using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace EFSamurai
{
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<SecretIdentity> SecretIdentities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server = (localdb)\MSSQLLocalDB; " +
                @"Database = EFSamurai; " +
                @"Trusted_Connection = True; ");
        }
    }
}
