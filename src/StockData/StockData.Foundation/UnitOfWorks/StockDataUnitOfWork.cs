using Microsoft.EntityFrameworkCore;
using StockData.Base.DbContexts;
using StockData.Base.Repositories;
using StockData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Base.UnitOfWorks
{
    public class StockDataUnitOfWork : UnitOfWork, IStockDataUnitOfWork
    {
        public ICompanyRepository Companies { get; private set; }
        public IStockPriceRepository StockPrices { get; private set; }

        public StockDataUnitOfWork(IStockDataScrapingDbContext context, 
            ICompanyRepository companies, 
            IStockPriceRepository stockPrices)
            : base((DbContext)context)
        {
            Companies = companies;
            StockPrices = stockPrices;
        }
    }
}
