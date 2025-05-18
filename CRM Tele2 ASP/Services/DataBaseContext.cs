namespace CRM_Tele2_ASP.Services
{
    using CRM_Tele2_ASP.Models;
    using Microsoft.EntityFrameworkCore;
    public class DataBaseContext : DbContext
    {
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Call> Calls { get; set; } = null!;

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.EnsureCreated();        
        }
    }
}
