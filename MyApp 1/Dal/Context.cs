using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Dal
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            
            builder.UseSqlServer(@"Server=localhost;Database=DBTest;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<Employees> Employees { get; set; }
    }
}
