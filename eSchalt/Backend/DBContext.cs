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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // this can be changed later if we add a primary key for example a hash value base on
            // x and y coordinate of component and Schaltschrank location
            modelBuilder.Entity<Eschalttabledemo>().ToTable("eschaltdemo"). // Maps the entity to the correct table name
                HasNoKey(); // Indicates that the table has no primary key

            base.OnModelCreating(modelBuilder);
        }
    }
}
