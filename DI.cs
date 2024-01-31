using ARS.Services;

namespace ARS; 

public static class DI {
    public static void Build(WebApplicationBuilder builder) {
        builder.Services.AddSingleton<IClientTestService, ClientTestService>();
    }
}