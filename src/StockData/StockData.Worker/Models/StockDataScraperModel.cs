using StockData.Base.Services.Scraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Worker.Models
{
    public class StockDataScraperModel
    {
        #region Dependency Injection
        protected IStockDataScraperService _stockDataScraperService;
        public StockDataScraperModel(IStockDataScraperService stockDataScraperService)
        {
            _stockDataScraperService = stockDataScraperService;
        }
        #endregion

        public string GetMarketStatus()
        {
            return _stockDataScraperService.MarketStatus();
        }

        public void GetScrapedData()
        {
            _stockDataScraperService.DataScraper();
        }
    }
}
