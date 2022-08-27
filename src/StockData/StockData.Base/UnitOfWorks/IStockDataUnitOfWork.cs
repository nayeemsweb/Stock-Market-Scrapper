using StockData.Base.Repositories;
using StockData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Base.UnitOfWorks
{
    public interface IStockDataUnitOfWork : IUnitOfWork
    {
        ICompanyRepository Companies { get; }
        IStockPriceRepository StockPrices { get; }
    }
}
