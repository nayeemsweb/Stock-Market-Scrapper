using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Base.Services
{
    public interface IStockPriceService
    {
        int GetId(string name);
        void AddStockPrice(List<List<string>> stockPriceList);
    }
}
