using System.Collections.Generic;

namespace eSchalt.Backend.HelperClasses
{
    public class AiDetectionResult
    {
        public List<AiComponentDto> Components { get; set; } = new();
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public List<string> Warnings { get; set; } = new();
    }

    public class AiComponentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Confidence { get; set; }

        public double XPosTopLeft { get; set; }
        public double YPosTopLeft { get; set; }
        public double XPosBottomRight { get; set; }
        public double YPosBottomRight { get; set; }
    }
}


