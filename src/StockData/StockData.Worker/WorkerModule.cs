using Autofac;
using StockData.Worker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Worker
{
    public class WorkerModule : Module
    {
        #region Dependency Injection
        protected readonly string _connectionString;
        protected readonly string _migrationAssemblyName;
        public static ILifetimeScope? AutofacContainer { get; set; }
        public WorkerModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }
        #endregion

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StockDataScraperModel>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
