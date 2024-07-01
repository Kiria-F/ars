using ARS.Gateways;
using ARS.Services;

namespace ARS; 

public static class Di {
    public static void Build(WebApplicationBuilder builder) {
        // Services
        builder.Services.AddTransient<AuthService>();
        builder.Services.AddTransient<SessionService>();
        
        // Gateways
        builder.Services.AddTransient<UserGateway>();
        builder.Services.AddSingleton<SessionGateway>();
    }
}