using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using eSchalt.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace eSchalt.Backend.HelperClasses
{
    public class AiComponentImportService
    {
        private readonly ApplicationDbContext _context;

        public AiComponentImportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ImportComponentsAsync(int switchBoxId, string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("AI JSON must not be empty.", nameof(json));

            Console.WriteLine($"[AiComponentImportService] Starting import for SwitchBoxId: {switchBoxId}");
            Console.WriteLine($"[AiComponentImportService] JSON length: {json.Length}");

            // 1. Deserialize JSON (throws on invalid)
            AiDetectionResult detection;
            try
            {
                detection = JsonSerializer.Deserialize<AiDetectionResult>(json)
                             ?? throw new InvalidOperationException("AI JSON contained no data.");
                Console.WriteLine($"[AiComponentImportService] Deserialized JSON. Found {detection.Components.Count} components");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"[AiComponentImportService] JSON deserialization failed: {ex.Message}");
                throw new InvalidOperationException("Failed to parse AI JSON.", ex);
            }

            // 2. Ensure SwitchBox exists (auto-create if needed)
            var switchBox = await _context.SwitchBoxes.FindAsync(switchBoxId);
            if (switchBox == null)
            {
                Console.WriteLine($"[AiComponentImportService] SwitchBox {switchBoxId} not found, creating new one");
                switchBox = new SwitchBox
                {
                    Id = switchBoxId,
                    Floor = string.Empty,
                    Room = string.Empty,
                    Group = string.Empty,
                    Type = string.Empty
                };

                _context.SwitchBoxes.Add(switchBox);
            }
            else
            {
                Console.WriteLine($"[AiComponentImportService] SwitchBox {switchBoxId} exists");
            }

            // 3. Delete existing components for this SwitchBox
            var oldComponents = _context.Components
                .Where(c => c.SwitchBoxId == switchBoxId)
                .ToList(); // Materialize the query

            Console.WriteLine($"[AiComponentImportService] Deleting {oldComponents.Count} existing components");
            _context.Components.RemoveRange(oldComponents);

            // 4. Insert new components (preserve doubles)
            var newComponents = detection.Components.Select(c => new Component
            {
                Name = c.Name,
                XPosTopLeft = c.XPosTopLeft,
                YPosTopLeft = c.YPosTopLeft,
                XPosBottomRight = c.XPosBottomRight,
                YPosBottomRight = c.YPosBottomRight,
                SwitchBoxId = switchBoxId
            }).ToList(); // Materialize the list

            Console.WriteLine($"[AiComponentImportService] Adding {newComponents.Count} new components");
            _context.Components.AddRange(newComponents);

            // 5. Commit
            Console.WriteLine($"[AiComponentImportService] Saving changes...");
            var saved = await _context.SaveChangesAsync();
            Console.WriteLine($"[AiComponentImportService] SaveChangesAsync returned: {saved} entities saved");
        }
    }
}


