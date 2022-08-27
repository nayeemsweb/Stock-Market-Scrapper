using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Base.Services.Scraper
{
    public class StockDataScraperService : IStockDataScraperService
    {
        #region Dependency Injection
        protected ICompanyService _companyService;
        protected IStockPriceService _stockPriceService;

        public StockDataScraperService(ICompanyService companyService, IStockPriceService stockPriceService)
        {
            _companyService = companyService;
            _stockPriceService = stockPriceService;
        }
        #endregion
        public void DataScraper()
        {
            var html = ("https://www.dse.com.bd/latest_share_price_scroll_l.php");

            var web = new HtmlWeb();
            var doc = web.Load(html);

            var tableNodes = doc
                .DocumentNode.Descendants("table")
                .Where(node => node.GetAttributeValue("class", "")
                .Contains("shares-table")).ToList();

            var allTexts = new List<List<string>>();
            foreach (var item in tableNodes)
            {
                //HtmlNodeCollection 
                var tableRowNodes = item.ChildNodes;
                
                foreach (var tableRowNode in tableRowNodes)
                {
                    if (tableRowNode.NodeType == HtmlNodeType.Element)
                    {
                        var tableDataNodes = tableRowNode.ChildNodes;
                        var text = new List<string>();
                        
                        foreach (var tableDataNode in tableDataNodes)
                        {
                            if (tableDataNode.NodeType == HtmlNodeType.Element)
                            {
                                var tempText = tableDataNode.InnerText;
                                var lineParts = tempText.Split('\t', '\r', '\n');

                                foreach (var part in lineParts)
                                {
                                    if (part != "")
                                    {
                                        if (part == "--")
                                        {
                                            text.Add("0");
                                        }
                                        else
                                        {
                                            text.Add(part);
                                        }
                                    }
                                }
                            }
                        }
                        allTexts.Add(text);
                    }
                }
            }
            DataSeparator(allTexts);
        }

        public void DataSeparator(List<List<string>> allTexts)
        {
            var companies = new List<string>();
            var stockData = new List<List<string>>();

            for (var i = 0; i < allTexts.Count; i++)
            {
                var text = new List<string>();

                if (i != 0)
                {
                    for (var j = 0; j < allTexts[i].Count; j++)
                    {
                        if (j == 0)
                        {
                            continue;
                        }
                        else if (j == 1)
                        {
                            companies.Add(allTexts[i][j]);
                            text.Add(allTexts[i][j]);
                        }
                        else
                        {
                            text.Add(allTexts[i][j]);
                        }
                    }
                }
                stockData.Add(text);
            }

            InsertDataToCompany(companies);
            InsertDataToStockPrice(stockData);
        }

        public string MarketStatus()
        {
            var allTexts = new List<List<string>>();
            var html = ("https://www.dse.com.bd/latest_share_price_scroll_l.php");

            var web = new HtmlWeb();
            var htmlDoc = web.Load(html);

            var marketStatus = htmlDoc
                .DocumentNode
                .SelectSingleNode("//html//body//div//div//div//header/div//span//span//b")
                .InnerText;

            return marketStatus;
        }

        public void InsertDataToCompany(List<string> companies)
        {
            _companyService.AddCompany(companies);
        }

        public void InsertDataToStockPrice(List<List<string>> stockData)
        {
            _stockPriceService.AddStockPrice(stockData);
        }
    }
}