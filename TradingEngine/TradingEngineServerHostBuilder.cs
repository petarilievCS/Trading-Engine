using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TradingEngine.Configuration;
using TradingEngine.Logging;
using TradingEngine.Logging.Configuration;

namespace TradingEngine.Core
{
	public sealed class TradingEngineServerHostBuilder
	{
		public static IHost BuildTradingEngineServer()
			=> Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
			{
				// Configuration
				services.AddOptions();
				services.Configure<TradingEngineConfiguration>(context.Configuration.GetSection(nameof(TradingEngineConfiguration)));
                services.Configure<LoggingConfiguration>(context.Configuration.GetSection(nameof(LoggingConfiguration)));

                // Singleton
                services.AddSingleton<ITradingEngineServer, TradingEngineServer>();
				services.AddSingleton<ITextLogger, TextLogger>();

				// Hosted services
				services.AddHostedService<TradingEngineServer>();
			}).Build();
	}
}

