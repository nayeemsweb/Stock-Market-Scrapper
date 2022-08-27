using StockData.Base.Entities;
using StockData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Base.Repositories
{
    public interface ICompanyRepository : IRepository<Company, int>
    {
    }
}
