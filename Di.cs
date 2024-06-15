using ARS.Services;

namespace ARS; 

public static class Di {
    public static void Build(WebApplicationBuilder builder) {
        builder.Services.AddTransient<AuthService>();
    }
}