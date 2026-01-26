using System.Text.Json;

namespace eSchalt.Backend.HelperClasses
{
    public static class PatternCorrector
    {
        public static string Correct(string aiJson, string patternJsonPath)
        {
            //load the AI
            var det = JsonSerializer.Deserialize<AiDetectionResult>(aiJson) ?? new AiDetectionResult();

            //load the pattern so that we know which names are allowed
            var allowed = LoadAllowedNamesFromPattern(patternJsonPath);

            //we normalize the names since the names in the pattern vs. the names of the AI may differ
            foreach (var c in det.Components)
                c.Name = NormalizeName(c.Name);

            //we delete duplicate Boxes - we keep the one with the higher confidence
            det.Components = DeduplicateByIoU(det.Components, iouThreshold: 0.50);

            //we sort the components into rows based on their y-coordinate (if components are the
            //same height, they get sorted into the same row
            var rows = ClusterRows(det.Components, yThreshold: 60);

            foreach (var r in rows)
            {
                //we order the components based on their x-coordinate 
                r.Sort((a, b) => CenterX(a).CompareTo(CenterX(b)));
                // we prevent the boxes from overlapping
                SplitHorizontalOverlapsInRow(r, minOverlapPx: 2);
            }

            //collect all rows
            var ordered = rows
                .OrderBy(r => r.Average(CenterY))
                .SelectMany(r => r)
                .ToList();

            //components get a new id after we ordered them
            for (int i = 0; i < ordered.Count; i++)
                ordered[i].Id = i;

            det.Components = ordered;

            //this is a warning if the KI missed a component or recognized a wrong one
            det.Warnings ??= new List<string>();
            foreach (var c in det.Components)
            {
                if (!allowed.Contains(c.Name))
                    det.Warnings.Add($"Unexpected component name '{c.Name}' not in pattern.");
            }

            return JsonSerializer.Serialize(det, new JsonSerializerOptions { WriteIndented = true });
        }

        private static HashSet<string> LoadAllowedNamesFromPattern(string patternPath)
        {
            var json = File.ReadAllText(patternPath);
            using var doc = JsonDocument.Parse(json);

            var cabinetObj = doc.RootElement.EnumerateObject().First().Value;
            var rows = cabinetObj.GetProperty("rows").EnumerateArray();

            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var row in rows)
            {
                foreach (var slot in row.GetProperty("slots").EnumerateArray())
                {
                    var expectedEl = slot.GetProperty("expected");

                    if (expectedEl.ValueKind == JsonValueKind.String)
                    {
                        var expected = expectedEl.GetString() ?? "";
                        if (!expected.Equals("empty", StringComparison.OrdinalIgnoreCase))
                            set.Add(NormalizeName(expected));
                    }
                    else if (expectedEl.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var e in expectedEl.EnumerateArray())
                        {
                            var expected = e.GetString() ?? "";
                            if (!expected.Equals("empty", StringComparison.OrdinalIgnoreCase))
                                set.Add(NormalizeName(expected));
                        }
                    }
                }
            }

            return set;
        }

        private static string NormalizeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return name;

            var n = name.Trim();

            n = n.Replace("_", "-");

            if (n.Contains("SPS", StringComparison.OrdinalIgnoreCase)) return "SPS";

            if (n.Equals("Sicherungen-230V", StringComparison.OrdinalIgnoreCase)) return "Sicherung230V";
            if (n.Equals("Sicherungen-230Vs", StringComparison.OrdinalIgnoreCase)) return "Sicherung230V";
            if (n.Equals("Sicherung-230V", StringComparison.OrdinalIgnoreCase)) return "Sicherung230V";
            if (n.Equals("Sicherung-230Vs", StringComparison.OrdinalIgnoreCase)) return "Sicherung230V";

            if (n.Equals("FI-Sicherungskombi", StringComparison.OrdinalIgnoreCase)) return "FI-Sicherungskombi";
            if (n.Equals("FI-Sicherungskombi", StringComparison.OrdinalIgnoreCase)) return "FI-Sicherungskombi";

            if (n.Equals("Relaiss", StringComparison.OrdinalIgnoreCase)) return "Relais";
            if (n.Equals("Relai", StringComparison.OrdinalIgnoreCase)) return "Relais";

            if (n.Equals("Verbinders", StringComparison.OrdinalIgnoreCase)) return "Verbinder";

            return n;
        }

        private static double CenterX(AiComponentDto c) => (c.XPosTopLeft + c.XPosBottomRight) / 2.0;
        private static double CenterY(AiComponentDto c) => (c.YPosTopLeft + c.YPosBottomRight) / 2.0;

        private static List<List<AiComponentDto>> ClusterRows(List<AiComponentDto> comps, double yThreshold)
        {
            var sorted = comps.OrderBy(CenterY).ToList();
            var rows = new List<List<AiComponentDto>>();

            foreach (var c in sorted)
            {
                if (rows.Count == 0)
                {
                    rows.Add(new List<AiComponentDto> { c });
                    continue;
                }

                var last = rows[^1];
                var meanY = last.Average(CenterY);

                if (Math.Abs(CenterY(c) - meanY) <= yThreshold)
                    last.Add(c);
                else
                    rows.Add(new List<AiComponentDto> { c });
            }

            return rows;
        }


        private static void SplitHorizontalOverlapsInRow(List<AiComponentDto> row, double minOverlapPx)
        {
            for (int i = 0; i < row.Count - 1; i++)
            {
                var a = row[i];
                var b = row[i + 1];

                double overlap = a.XPosBottomRight - b.XPosTopLeft;

                if (overlap > minOverlapPx)
                {
                    double newLeft = a.XPosBottomRight;

                    if (newLeft < b.XPosBottomRight - 1)
                        b.XPosTopLeft = newLeft;
                }
            }
        }


        private static List<AiComponentDto> DeduplicateByIoU(List<AiComponentDto> comps, double iouThreshold)
        {
            var result = new List<AiComponentDto>();

            foreach (var grp in comps.GroupBy(c => c.Name, StringComparer.OrdinalIgnoreCase))
            {
                var list = grp.OrderByDescending(c => c.Confidence).ToList();

                while (list.Count > 0)
                {
                    var best = list[0];
                    result.Add(best);
                    list.RemoveAt(0);

                    list = list.Where(c => IoU(best, c) < iouThreshold).ToList();
                }
            }

            return result;
        }

        private static double IoU(AiComponentDto a, AiComponentDto b)
        {
            double xA = Math.Max(a.XPosTopLeft, b.XPosTopLeft);
            double yA = Math.Max(a.YPosTopLeft, b.YPosTopLeft);
            double xB = Math.Min(a.XPosBottomRight, b.XPosBottomRight);
            double yB = Math.Min(a.YPosBottomRight, b.YPosBottomRight);

            double interW = Math.Max(0, xB - xA);
            double interH = Math.Max(0, yB - yA);
            double interArea = interW * interH;

            double areaA = Math.Max(0, a.XPosBottomRight - a.XPosTopLeft) *
                           Math.Max(0, a.YPosBottomRight - a.YPosTopLeft);

            double areaB = Math.Max(0, b.XPosBottomRight - b.XPosTopLeft) *
                           Math.Max(0, b.YPosBottomRight - b.YPosTopLeft);

            double denom = areaA + areaB - interArea;
            if (denom <= 0) return 0;

            return interArea / denom;
        }
    }
}
