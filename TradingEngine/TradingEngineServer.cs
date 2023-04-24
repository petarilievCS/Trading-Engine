using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using TradingEngine.Configuration;
using TradingEngine.Logging;

namespace TradingEngine.Core
{
	public sealed class TradingEngineServer : BackgroundService, ITradingEngineServer
	{
        private readonly ITextLogger _logger;
        private readonly IOptions<TradingEngineConfiguration> _config;

		public TradingEngineServer(ITextLogger logger, IOptions<TradingEngineConfiguration> config)
		{
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config ?? throw new ArgumentNullException(nameof(config));
		}

        // Method that provies access to ExecuteAsync
        public Task Run(CancellationToken token) => ExecuteAsync(token);

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Info(nameof(TradingEngine), "Server Starting...");
            while (!stoppingToken.IsCancellationRequested)
            {
          
            }
            _logger.Info(nameof(TradingEngine), "Server Stopped.");
            return Task.CompletedTask;
        }
    }
}

