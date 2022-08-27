using StockData.Base.Services.Scraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Service.Models
{
    public class ScraperModel
    {
        protected IStockDataScraperService _stockDataScraperService;
        public ScraperModel(IStockDataScraperService stockDataScraperService)
        {
            _stockDataScraperService = stockDataScraperService;
        }

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
