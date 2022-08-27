using StockData.Base.Entities;
using StockData.Base.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockData.Data;

using StockData.Base.Repositories;


namespace StockData.Base.Services
{
    public class StockPriceService : IStockPriceService
    {
        #region Dependency Injection
        protected readonly IStockDataUnitOfWork _stockDataUnitOfWork;
        public StockPriceService(IStockDataUnitOfWork stockDataUnitOfWork)
        {
            _stockDataUnitOfWork = stockDataUnitOfWork;
        }
        #endregion

        public int GetId(string name)
        {
            var getEntity = _stockDataUnitOfWork.Companies.Get(c => c.TradeCode == name, "").FirstOrDefault();

            var entity = new Company
            {
                Id = getEntity.Id,
                TradeCode = getEntity.TradeCode
            };

            return entity.Id;
        }

        public void AddStockPrice(List<List<string>> stockPriceList)
        {
            for (var i = 1; i < stockPriceList.Count; i++)
            {
                var companyId = GetId(stockPriceList[i][0]);

                _stockDataUnitOfWork.StockPrices.Add(
                    new StockPrice
                    {
                        CompanyId = companyId,
                        LastTradingPrice = Convert.ToDouble(stockPriceList[i][1]),
                        High = Convert.ToDouble(stockPriceList[i][2]),
                        Low = Convert.ToDouble(stockPriceList[i][3]),
                        ClosePrice = Convert.ToDouble(stockPriceList[i][4]),
                        YesterdayClosePrice = Convert.ToDouble(stockPriceList[i][5]),
                        Change = Convert.ToDouble(stockPriceList[i][6]),
                        Trade = Convert.ToDouble(stockPriceList[i][7]),
                        Value = Convert.ToDouble(stockPriceList[i][8]),
                        Volume = Convert.ToDouble(stockPriceList[i][9])
                    });
            }
            _stockDataUnitOfWork.Save();
        }
    }
}
