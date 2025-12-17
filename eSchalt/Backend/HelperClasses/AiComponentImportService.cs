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

            // 1. Deserialize JSON (throws on invalid)
            AiDetectionResult detection;
            try
            {
                detection = JsonSerializer.Deserialize<AiDetectionResult>(json)
                             ?? throw new InvalidOperationException("AI JSON contained no data.");
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to parse AI JSON.", ex);
            }

            // 2. Ensure SwitchBox exists (auto-create if needed)
            var switchBox = await _context.SwitchBoxes.FindAsync(switchBoxId);
            if (switchBox == null)
            {
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

            // 3. Delete existing components for this SwitchBox
            var oldComponents = _context.Components
                .Where(c => c.SwitchBoxId == switchBoxId);

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
            });

            await _context.Components.AddRangeAsync(newComponents);

            // 5. Commit
            await _context.SaveChangesAsync();
        }
    }
}


