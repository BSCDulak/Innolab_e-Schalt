using eSchalt.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace eSchalt.Backend
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor to pass options to the base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // DbSet properties for each table
        public DbSet<Eschalttabledemo> Eschalttabledemo { get; set; }
        public DbSet<HardcodingDbTest> HardcodingDbTest { get; set; }
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


            base.OnModelCreating(modelBuilder);
        }
    }
}
