using eSchalt.Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace eSchalt.Backend
{
    public class ApplicationDbContext : IdentityDbContext
    {
        // Constructor to pass options to the base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // DbSet properties for each table
        public DbSet<SwitchBox> SwitchBoxes { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentConnection> ComponentConnections { get; set; }
        public DbSet<SwitchBoxQRLink> SwitchBoxQRLinks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // SwitchBox-Component one-to-many
            modelBuilder.Entity<SwitchBox>()
                .HasMany(s => s.Components)
                .WithOne(c => c.SwitchBox)
                .HasForeignKey(c => c.SwitchBoxId)
                .OnDelete(DeleteBehavior.Cascade);

            // Component-Component many-to-many via ComponentConnection
            modelBuilder.Entity<ComponentConnection>()
                .HasOne(cc => cc.FromComponent)
                .WithMany(c => c.Connections)
                .HasForeignKey(cc => cc.FromComponentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComponentConnection>()
                .HasOne(cc => cc.ToComponent)
                .WithMany(c => c.ConnectedTo)
                .HasForeignKey(cc => cc.ToComponentId)
                .OnDelete(DeleteBehavior.Restrict);

            // SwitchBox-SwitchBoxQRLink one-to-many, if we want to only have one QR link per switchbox, we can change this to .WithOne() instead of .WithMany()
            modelBuilder.Entity<SwitchBoxQRLink>()
                .HasOne(q => q.SwitchBox)
                .WithMany()
                .HasForeignKey(q => q.SwitchBoxId)
                .OnDelete(DeleteBehavior.Cascade);


            // Seed SwitchBox
            modelBuilder.Entity<SwitchBox>().HasData(
                new SwitchBox { Id = 1, Floor = "EG", Group = "E-DO15", Room = "", Type = "" }
            );

            // Seed Components
            modelBuilder.Entity<Component>().HasData(
                new Component { Id = 1, Name = "S1", XPosTopLeft = 101, YPosTopLeft = 105, XPosBottomRight = 163, YPosBottomRight = 211, SwitchBoxId = 1 },
                new Component { Id = 2, Name = "S2", XPosTopLeft = 166, YPosTopLeft = 111, XPosBottomRight = 213, YPosBottomRight = 215, SwitchBoxId = 1 },
                new Component { Id = 3, Name = "S3", XPosTopLeft = 215, YPosTopLeft = 105, XPosBottomRight = 252, YPosBottomRight = 215, SwitchBoxId = 1 },
                new Component { Id = 4, Name = "S4", XPosTopLeft = 329, YPosTopLeft = 111, XPosBottomRight = 351, YPosBottomRight = 212, SwitchBoxId = 1 },
                new Component { Id = 5, Name = "S5", XPosTopLeft = 353, YPosTopLeft = 111, XPosBottomRight = 372, YPosBottomRight = 211, SwitchBoxId = 1 },
                new Component { Id = 6, Name = "S6", XPosTopLeft = 374, YPosTopLeft = 111, XPosBottomRight = 393, YPosBottomRight = 209, SwitchBoxId = 1 },
                new Component { Id = 7, Name = "S7", XPosTopLeft = 394, YPosTopLeft = 112, XPosBottomRight = 413, YPosBottomRight = 212, SwitchBoxId = 1 },
                new Component { Id = 8, Name = "S8", XPosTopLeft = 415, YPosTopLeft = 112, XPosBottomRight = 434, YPosBottomRight = 210, SwitchBoxId = 1 },
                new Component { Id = 9, Name = "S9", XPosTopLeft = 435, YPosTopLeft = 104, XPosBottomRight = 455, YPosBottomRight = 210, SwitchBoxId = 1 },
                new Component { Id = 10, Name = "S10", XPosTopLeft = 456, YPosTopLeft = 109, XPosBottomRight = 474, YPosBottomRight = 208, SwitchBoxId = 1 },
                new Component { Id = 11, Name = "S11", XPosTopLeft = 476, YPosTopLeft = 107, XPosBottomRight = 497, YPosBottomRight = 211, SwitchBoxId = 1 },
                new Component { Id = 12, Name = "S12", XPosTopLeft = 548, YPosTopLeft = 109, XPosBottomRight = 583, YPosBottomRight = 208, SwitchBoxId = 1 },
                new Component { Id = 13, Name = "S13", XPosTopLeft = 584, YPosTopLeft = 108, XPosBottomRight = 603, YPosBottomRight = 209, SwitchBoxId = 1 },
                new Component { Id = 14, Name = "S14", XPosTopLeft = 604, YPosTopLeft = 109, XPosBottomRight = 624, YPosBottomRight = 209, SwitchBoxId = 1 },
                new Component { Id = 15, Name = "S15", XPosTopLeft = 626, YPosTopLeft = 110, XPosBottomRight = 644, YPosBottomRight = 208, SwitchBoxId = 1 },
                new Component { Id = 16, Name = "S16", XPosTopLeft = 645, YPosTopLeft = 107, XPosBottomRight = 664, YPosBottomRight = 205, SwitchBoxId = 1 },
                new Component { Id = 17, Name = "S17", XPosTopLeft = 665, YPosTopLeft = 104, XPosBottomRight = 694, YPosBottomRight = 207, SwitchBoxId = 1 },
                new Component { Id = 18, Name = "R1", XPosTopLeft = 126, YPosTopLeft = 278, XPosBottomRight = 168, YPosBottomRight = 371, SwitchBoxId = 1 },
                new Component { Id = 19, Name = "R2", XPosTopLeft = 169, YPosTopLeft = 277, XPosBottomRight = 207, YPosBottomRight = 370, SwitchBoxId = 1 },
                new Component { Id = 20, Name = "R3", XPosTopLeft = 207, YPosTopLeft = 275, XPosBottomRight = 249, YPosBottomRight = 370, SwitchBoxId = 1 },
                new Component { Id = 21, Name = "R4", XPosTopLeft = 249, YPosTopLeft = 269, XPosBottomRight = 300, YPosBottomRight = 367, SwitchBoxId = 1 },
                new Component { Id = 22, Name = "R5", XPosTopLeft = 425, YPosTopLeft = 274, XPosBottomRight = 503, YPosBottomRight = 367, SwitchBoxId = 1 },
                new Component { Id = 23, Name = "R6", XPosTopLeft = 504, YPosTopLeft = 272, XPosBottomRight = 579, YPosBottomRight = 363, SwitchBoxId = 1 },
                new Component { Id = 24, Name = "R7", XPosTopLeft = 580, YPosTopLeft = 272, XPosBottomRight = 655, YPosBottomRight = 361, SwitchBoxId = 1 },
                new Component { Id = 25, Name = "R8", XPosTopLeft = 656, YPosTopLeft = 272, XPosBottomRight = 735, YPosBottomRight = 362, SwitchBoxId = 1 },
                new Component { Id = 26, Name = "R9", XPosTopLeft = 561, YPosTopLeft = 411, XPosBottomRight = 652, YPosBottomRight = 508, SwitchBoxId = 1 },
                new Component { Id = 27, Name = "R0", XPosTopLeft = 652, YPosTopLeft = 412, XPosBottomRight = 729, YPosBottomRight = 506, SwitchBoxId = 1 }
            );

            // Seed ComponentConnections (s1->r1, s1->r2, s2->r2)
            modelBuilder.Entity<ComponentConnection>().HasData(
                new ComponentConnection { Id = 1, FromComponentId = 1, ToComponentId = 18 },
                new ComponentConnection { Id = 2, FromComponentId = 1, ToComponentId = 19 },
                new ComponentConnection { Id = 3, FromComponentId = 2, ToComponentId = 19 }
            );
            // This will cause problems with cascade delete if we delete a component, it will not be able to delete all connections to that component
            // TODO: Add a cascade delete to the ComponentConnection table
            modelBuilder.Entity<SwitchBoxQRLink>().HasData(
                new SwitchBoxQRLink
                {
                    Id = 1,
                    SwitchBoxId = 1,
                    QRLink = "32fe4380-615b-4ed7-8622-a981303264dc.png"
                }
            );


            base.OnModelCreating(modelBuilder);
        }
    }
}
