using Microsoft.EntityFrameworkCore;
using StockData.Base.Entities;
using StockData.Foundation.DbContexts.DataScraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Base.DbContexts.DataScraper
{
    public class DataScraperDbContext : DbContext, IDataScraperDbContext
    {
        protected readonly string _connectionString;
        protected readonly string _migrationAssemblyName;
        public DataScraperDbContext(string connectionString, string migrationAssemblyName)
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

        public DbSet<Company> Companies { get; set; }
        public DbSet<StockPrice> StockPrices { get; set; }
    }
}
