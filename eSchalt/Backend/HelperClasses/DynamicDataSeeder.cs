using eSchalt.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace eSchalt.Backend.HelperClasses
{
    public class DynamicDataSeeder
    {
        public static void EnsureSeedData(ApplicationDbContext context)
        {
            EnsureSwitchBoxQRLinks(context);
            EnsureSwitchBoxes(context);

            context.SaveChanges();

            // Reset PostgreSQL sequences to prevent duplicate key errors due to the seeded switchboxes ID being the same as newly created switchboxes ID
            // This ensures the sequence is higher than any existing ID
            // this is only an issue at the start when we try to iterate over the seeded table entries and try to add new ones with the same ID
            ResetSequences(context);
        }

        private static void EnsureSwitchBoxQRLinks(ApplicationDbContext context)
        {
            var seedLinks = new List<SwitchBoxQRLink>
        {
            new SwitchBoxQRLink { SwitchBoxId = 2, QRLink = "theemptycabinet.png" }
        };

            foreach (var link in seedLinks)
            {
                if (!context.SwitchBoxQRLinks.Any(x => x.QRLink == link.QRLink && x.SwitchBoxId == link.SwitchBoxId))
                {
                    context.SwitchBoxQRLinks.Add(link);
                }
            }
        }

        private static void EnsureSwitchBoxes(ApplicationDbContext context)
        {
            var seedBoxes = new List<SwitchBox>
        {
            new SwitchBox { Id = 2, Room = "Room A", Floor = "2", Type = "Type Unused", Group = "Group 2" }
        };

            foreach (var box in seedBoxes)
            {
                if (!context.SwitchBoxes.Any(x => x.Id == box.Id))
                {
                    context.SwitchBoxes.Add(box);
                }
            }
        }

        private static void ResetSequences(ApplicationDbContext context)
        {
            // Reset sequences for all tables that use auto-incrementing IDs
            // This ensures the sequence is set to the maximum existing ID + 1
            
            // Reset SwitchBoxes sequence
            var maxSwitchBoxId = context.SwitchBoxes.Any() 
                ? context.SwitchBoxes.Max(s => s.Id) 
                : 0;
            context.Database.ExecuteSqlRaw($"SELECT setval('\"SwitchBoxes_Id_seq\"', {maxSwitchBoxId}, true);");

            // Reset Components sequence
            var maxComponentId = context.Components.Any() 
                ? context.Components.Max(c => c.Id) 
                : 0;
            context.Database.ExecuteSqlRaw($"SELECT setval('\"Components_Id_seq\"', {maxComponentId}, true);");

            // Reset ComponentConnections sequence
            var maxConnectionId = context.ComponentConnections.Any() 
                ? context.ComponentConnections.Max(cc => cc.Id) 
                : 0;
            context.Database.ExecuteSqlRaw($"SELECT setval('\"ComponentConnections_Id_seq\"', {maxConnectionId}, true);");

            // Reset SwitchBoxQRLinks sequence
            var maxQRLinkId = context.SwitchBoxQRLinks.Any() 
                ? context.SwitchBoxQRLinks.Max(q => q.Id) 
                : 0;
            context.Database.ExecuteSqlRaw($"SELECT setval('\"SwitchBoxQRLinks_Id_seq\"', {maxQRLinkId}, true);");
        }
    }
}