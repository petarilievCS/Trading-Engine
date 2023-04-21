using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TradingEngine.Configuration;

namespace TradingEngine
{
	public sealed class TradingEngineServerHostBuilder
	{
		public static IHost BuildTradingEngineServer()
			=> Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
			{
				// Configuration
				services.AddOptions();
				services.Configure<TradingEngineConfiguration>(context.Configuration.GetSection(nameof(TradingEngineConfiguration)));

				// Singleton
				services.AddSingleton<ITradingEngineServer, TradingEngineServer>();

				// Hosted services
				services.AddHostedService<TradingEngineServer>();
			}).Build();
	}
}

