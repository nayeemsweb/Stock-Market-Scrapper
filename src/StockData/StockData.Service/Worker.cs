using StockData.Service.Models;

namespace StockData.Service
{
    public class Worker : BackgroundService
    {
        #region Dependency Injection
        private readonly ILogger<Worker> _logger;
        private readonly ScraperModel _scraperModel;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, ScraperModel scraperModel, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _scraperModel = scraperModel;
            _serviceProvider = serviceProvider;
        }
        #endregion

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                if (_scraperModel.GetMarketStatus() == "Open")
                {
                    _scraperModel.GetScrapedData();
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