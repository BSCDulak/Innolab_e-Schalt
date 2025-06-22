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
        public DbSet<Eschalttabledemo> Eschalttabledemo { get; set; }
        public DbSet<HardcodingDbTest> HardcodingDbTest { get; set; }
        public DbSet<SwitchBox> SwitchBoxes { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentConnection> ComponentConnections { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // this can be changed later if we add a primary key for example a hash value base on
            // x and y coordinate of component and Schaltschrank location
            modelBuilder.Entity<Eschalttabledemo>().ToTable("eschaltdemo"). // Maps the entity to the correct table name
                HasNoKey(); // Indicates that the table has no primary key
            // Mapping and configuration for HardcodingTest
            modelBuilder.Entity<HardcodingDbTest>(entity =>
            {
                entity.ToTable("hardcodingdbtest"); // Maps to table name
                entity.HasKey(e => e.ComponentId); // Define ComponentId as the Primary Key
            });
            // Seed data for HardcodingTest
            modelBuilder.Entity<HardcodingDbTest>().HasData(
                new HardcodingDbTest { ComponentId = 1, Stockwerk = "First Floor", Raum = "101", Bemerkung = "Remark 1", Fi = "FI-A", Leiter = "L1", Gruppe = "G1", Sicherung = "S1", Relais = "R1", Dimmer = "D1", Ausgang = "A1", Eingang = "I1", KabelInfo = "Copper", Typ = "Type A", Info = "Info 1", Beschr = "Description 1", StockwerkKurz = "FF", SpsPositionImArray = "1" },
                new HardcodingDbTest { ComponentId = 2, Stockwerk = "First Floor", Raum = "102", Bemerkung = "Remark 2", Fi = "FI-B", Leiter = "L2", Gruppe = "G2", Sicherung = "S2", Relais = "R2", Dimmer = "D2", Ausgang = "A2", Eingang = "I2", KabelInfo = "Fiber", Typ = "Type B", Info = "Info 2", Beschr = "Description 2", StockwerkKurz = "FF", SpsPositionImArray = "2" },
                new HardcodingDbTest { ComponentId = 3, Stockwerk = "Second Floor", Raum = "201", Bemerkung = "Remark 3", Fi = "FI-C", Leiter = "L3", Gruppe = "G3", Sicherung = "S3", Relais = "R3", Dimmer = "D3", Ausgang = "A3", Eingang = "I3", KabelInfo = "Copper", Typ = "Type C", Info = "Info 3", Beschr = "Description 3", StockwerkKurz = "SF", SpsPositionImArray = "3" },
                new HardcodingDbTest { ComponentId = 4, Stockwerk = "Second Floor", Raum = "202", Bemerkung = "Remark 4", Fi = "FI-D", Leiter = "L4", Gruppe = "G4", Sicherung = "S4", Relais = "R4", Dimmer = "D4", Ausgang = "A4", Eingang = "I4", KabelInfo = "Fiber", Typ = "Type D", Info = "Info 4", Beschr = "Description 4", StockwerkKurz = "SF", SpsPositionImArray = "4" },
                new HardcodingDbTest { ComponentId = 5, Stockwerk = "Third Floor", Raum = "301", Bemerkung = "Remark 5", Fi = "FI-E", Leiter = "L5", Gruppe = "G5", Sicherung = "S5", Relais = "R5", Dimmer = "D5", Ausgang = "A5", Eingang = "I5", KabelInfo = "Copper", Typ = "Type E", Info = "Info 5", Beschr = "Description 5", StockwerkKurz = "TF", SpsPositionImArray = "5" },
                new HardcodingDbTest { ComponentId = 6, Stockwerk = "Third Floor", Raum = "302", Bemerkung = "Remark 6", Fi = "FI-F", Leiter = "L6", Gruppe = "G6", Sicherung = "S6", Relais = "R6", Dimmer = "D6", Ausgang = "A6", Eingang = "I6", KabelInfo = "Fiber", Typ = "Type F", Info = "Info 6", Beschr = "Description 6", StockwerkKurz = "TF", SpsPositionImArray = "6" },
                new HardcodingDbTest { ComponentId = 7, Stockwerk = "Fourth Floor", Raum = "401", Bemerkung = "Remark 7", Fi = "FI-G", Leiter = "L7", Gruppe = "G7", Sicherung = "S7", Relais = "R7", Dimmer = "D7", Ausgang = "A7", Eingang = "I7", KabelInfo = "Copper", Typ = "Type G", Info = "Info 7", Beschr = "Description 7", StockwerkKurz = "FoF", SpsPositionImArray = "7" },
                new HardcodingDbTest { ComponentId = 8, Stockwerk = "Fourth Floor", Raum = "402", Bemerkung = "Remark 8", Fi = "FI-H", Leiter = "L8", Gruppe = "G8", Sicherung = "S8", Relais = "R8", Dimmer = "D8", Ausgang = "A8", Eingang = "I8", KabelInfo = "Fiber", Typ = "Type H", Info = "Info 8", Beschr = "Description 8", StockwerkKurz = "FoF", SpsPositionImArray = "8" },
                new HardcodingDbTest { ComponentId = 9, Stockwerk = "Fifth Floor", Raum = "501", Bemerkung = "Remark 9", Fi = "FI-I", Leiter = "L9", Gruppe = "G9", Sicherung = "S9", Relais = "R9", Dimmer = "D9", Ausgang = "A9", Eingang = "I9", KabelInfo = "Copper", Typ = "Type I", Info = "Info 9", Beschr = "Description 9", StockwerkKurz = "FiF", SpsPositionImArray = "9" },
                new HardcodingDbTest { ComponentId = 10, Stockwerk = "Fifth Floor", Raum = "502", Bemerkung = "Remark 10", Fi = "FI-J", Leiter = "L10", Gruppe = "G10", Sicherung = "S10", Relais = "R10", Dimmer = "D10", Ausgang = "A10", Eingang = "I10", KabelInfo = "Fiber", Typ = "Type J", Info = "Info 10", Beschr = "Description 10", StockwerkKurz = "FiF", SpsPositionImArray = "10" }
            );

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

            base.OnModelCreating(modelBuilder);
        }
    }
}
