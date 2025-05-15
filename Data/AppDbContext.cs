using DiplomProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DiplomProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
    }
}