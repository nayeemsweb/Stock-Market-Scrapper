using Autofac;
using Microsoft.Extensions.Configuration;
using StockData.Base.DbContexts;
using StockData.Base.Repositories;
using StockData.Base.Services;
using StockData.Base.Services.Scraper;
using StockData.Base.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Base
{
    public class BaseModule : Module
    {
        #region Dependency Injection
        protected readonly string _connectionString;
        protected readonly string _migrationAssemblyName;        
        public BaseModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }
        #endregion

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StockDataScrapingDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<StockDataScrapingDbContext>().As<IStockDataScrapingDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<StockPriceRepository>().As<IStockPriceRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StockDataUnitOfWork>().As<IStockDataUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CompanyService>().As<ICompanyService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StockPriceService>().As<IStockPriceService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StockDataScraperService>().As<IStockDataScraperService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
