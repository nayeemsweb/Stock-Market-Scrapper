using Autofac;
using StockData.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Service
{
    public class WorkerModule : Module
    {
        #region Dependency Injection
        protected readonly string _connectionString;
        protected readonly string _migrationAssemblyName;
        public WorkerModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }
        #endregion

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ScraperModel>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
