using Framework.Models;
using Microsoft.EntityFrameworkCore;

namespace Framework.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {                
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<CambioPassword> CambiosPasswords { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Client> Clients { get; set; }

    }
}
