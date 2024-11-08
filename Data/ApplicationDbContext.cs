using ClientPortalWeb.Models;
using ClientPortalWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientPortalWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<LinkedClient> LinkedClient { get; set; } // new table for linked clients
        public DbSet<LinkedContacts> LinkedContacts { get; set; } // new table for linked clients

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mark ClientContactViewModel as keyless
            modelBuilder.Entity<LinkedClient>().HasNoKey();
            modelBuilder.Entity<LinkedClient>().ToTable("LinkedClient");

            modelBuilder.Entity<LinkedContacts>().HasNoKey();
            modelBuilder.Entity<LinkedContacts>().ToTable("LinkedContacts");


            // Configure the relationships for LinkedClient

            modelBuilder.Entity<LinkedClient>()
                .HasOne(lc => lc.Contact)
                .WithMany()
                .HasForeignKey(lc => lc.ClientId);

            modelBuilder.Entity<LinkedContacts>()
                .HasOne(lc => lc.Client)
                .WithMany()
                .HasForeignKey(lc => lc.ClientId);
        }

    }
}
