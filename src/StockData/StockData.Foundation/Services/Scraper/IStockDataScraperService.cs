using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Base.Services.Scraper
{
    public interface IStockDataScraperService
    {
        void DataScraper();
        void DataSeparator(List<List<string>> allTexts);
        string MarketStatus();
        void InsertDataToCompany(List<string> companies);
        void InsertDataToStockPrice(List<List<string>> stockData);
        
    }
}
