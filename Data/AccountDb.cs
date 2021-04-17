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
        public virtual DbSet<ClientReservations> ClientReservations{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientReservations>().HasKey(sc => new { sc.ReservationId, sc.ClientId });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HotelReservationsDb;Integrated Security=True");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
