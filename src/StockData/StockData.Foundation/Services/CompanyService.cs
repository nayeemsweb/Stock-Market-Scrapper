using StockData.Base.Entities;
using StockData.Base.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Base.Services
{
    public class CompanyService : ICompanyService
    {
        #region Dependency Injection
        protected readonly IStockDataUnitOfWork _stockDataUnitOfWork;
        public CompanyService(IStockDataUnitOfWork stockDataUnitOfWork)
        {
            _stockDataUnitOfWork = stockDataUnitOfWork;
        }
        #endregion

        public void AddCompany(List<string> companies)
        {
            foreach (var company in companies)
            {
                var getEntity = _stockDataUnitOfWork.Companies.Get(c => c.TradeCode == company, "").FirstOrDefault();

                if (getEntity == null)
                {
                    _stockDataUnitOfWork.Companies.Add(
                        new Company
                        {
                            TradeCode = company.ToString()
                        });
                }
            }
            _stockDataUnitOfWork.Save();
        }
    }
}
