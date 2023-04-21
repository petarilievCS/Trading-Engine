using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TradingEngine.Configuration;

namespace TradingEngine
{
	public sealed class TradingEngineServer : BackgroundService, ITradingEngineServer
	{
        private readonly ILogger<TradingEngineServer> _logger;
        private readonly IOptions<TradingEngineConfiguration> _config;

		public TradingEngineServer(ILogger<TradingEngineServer> logger, IOptions<TradingEngineConfiguration> config)
		{
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config ?? throw new ArgumentNullException(nameof(config));
		}

        // Method that provies access to ExecuteAsync
        public Task Run(CancellationToken token) => ExecuteAsync(token);

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Starting {nameof(TradingEngineServer)}");
            while (!stoppingToken.IsCancellationRequested)
            {
          
            }
            _logger.LogInformation($"Stopped {nameof(TradingEngineServer)}");
            return Task.CompletedTask;
        }
    }
}

