using Microsoft.EntityFrameworkCore;
using StockData.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Base.DbContexts
{
    public interface IStockDataScrapingDbContext
    {
        DbSet<Company> Company { get; set; }
    }
}
