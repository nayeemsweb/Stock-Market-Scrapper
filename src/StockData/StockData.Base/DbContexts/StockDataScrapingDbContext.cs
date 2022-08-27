using Microsoft.EntityFrameworkCore;
using StockData.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Base.DbContexts
{
    public class StockDataScrapingDbContext : DbContext, IStockDataScrapingDbContext
    {
        protected readonly string _connectionString;
        protected readonly string _migrationAssemblyName;
        public StockDataScrapingDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Company>()
                .HasMany(s => s.StockPrice)
                .WithOne(c => c.Company)
                .HasForeignKey(f => f.CompanyId);

            base.OnModelCreating(model);
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<StockPrice> StockPrices { get; set; }
    }
}
