using ARS.Services;
using ARS.Services.Interfaces;

namespace ARS; 

public static class DI {
    public static void Build(WebApplicationBuilder builder) {
        builder.Services.AddSingleton<IClientTestService, ClientTestService>();
        builder.Services.AddSingleton<IRandomGeneratorService, RandomGeneratorService>();
        builder.Services.AddScoped<IRangeService, RangeService>();
		
		builder.Services.AddSingleton<IGreetingsService, GreetingsService>();
    }
}