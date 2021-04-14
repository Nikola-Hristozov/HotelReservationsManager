using Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AccountDb : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AccountDb;Integrated Security=True");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
