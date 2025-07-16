using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BithdayAPP
{
    internal class BirthdayContext : DbContext
    {
        public DbSet<Birthday> Birthdays { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
