using eSchalt.Backend.Models;

namespace eSchalt.Backend.HelperClasses
{
    public class DynamicDataSeeder
    {
        public static void EnsureSeedData(ApplicationDbContext context)
        {
            EnsureSwitchBoxQRLinks(context);
            EnsureSwitchBoxes(context);

            context.SaveChanges();
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
    }
}