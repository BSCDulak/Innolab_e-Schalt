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
    }
}
