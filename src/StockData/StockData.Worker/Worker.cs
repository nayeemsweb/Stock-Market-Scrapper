using Autofac;
using StockData.Base.DbContexts;
using StockData.Worker.Models;

namespace StockData.Worker
{
    public class Worker : BackgroundService
    {
        #region Dependency Injection
        private readonly ILogger<Worker> _logger;
        private readonly StockDataScraperModel _stockDataScraperModel;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, StockDataScraperModel stockDataScraperModel, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _stockDataScraperModel = stockDataScraperModel;
            _serviceProvider = serviceProvider;
        }        
        #endregion

        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var scope = _serviceProvider.CreateScope().ServiceProvider;
                var context = scope.GetService<StockDataScrapingDbContext>();

                if (_stockDataScraperModel.GetMarketStatus() == "Open")
                {
                    _stockDataScraperModel.GetScrapedData();
                }
                else
                {
                    _logger.LogInformation("Market Status: CLOSED at: {time}", DateTimeOffset.Now);
                }
                
                //We know, 1min = 60000ms
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}