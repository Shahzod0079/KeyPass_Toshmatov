using KeyPass_Toshmatov.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KeyPass_Toshmatov.Classes
{
    public class DatabaseManager : DbContext
    {
        public DbSet<Storage> Storages { get; set; }
        public DbSet<User> Users { get; set; }

        public DatabaseManager(DbContextOptions<DatabaseManager> options) : base(options)
        {
        }

        public DatabaseManager() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "server=127.0.0.1;port=3307;uid=root;pwd=;database=Storage;",
                    new MySqlServerVersion(new Version(8, 0, 11)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Storage>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class DatabaseManagerFactory : IDesignTimeDbContextFactory<DatabaseManager>
    {
        public DatabaseManager CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseManager>();
            optionsBuilder.UseMySql(
                "server=127.0.0.1;port=3307;uid=root;pwd=;database=Storage;",
                new MySqlServerVersion(new Version(8, 0, 11)));

            return new DatabaseManager(optionsBuilder.Options);
        }
    }
}