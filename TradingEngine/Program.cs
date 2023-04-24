using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TradingEngine.Core;

using var engine = TradingEngineServerHostBuilder.BuildTradingEngineServer();
TradingEngineServerProvider.ServiceProvider = engine.Services;
{
    using var scope = TradingEngineServerProvider.ServiceProvider.CreateScope();
    await engine.RunAsync().ConfigureAwait(false); 
}
