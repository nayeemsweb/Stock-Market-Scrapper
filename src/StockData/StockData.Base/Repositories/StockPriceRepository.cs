using Microsoft.EntityFrameworkCore;
using StockData.Base.DbContexts;
using StockData.Base.Entities;
using StockData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Base.Repositories
{
    public class StockPriceRepository : Repository<StockPrice, int>, IStockPriceRepository
    {
        public StockPriceRepository(IStockDataScrapingDbContext context) 
            : base((DbContext)context)
        {
        }
    }
}
