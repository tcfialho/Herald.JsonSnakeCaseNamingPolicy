using Microsoft.Extensions.DependencyInjection;

using System.Text.Json;

namespace Herald.JsonSnakeCaseNamingPolicy
{
    public static class Configurations
    {
        public static IServiceCollection AddSnakeCaseNamingPolicy(this IServiceCollection services)
        {
            var defaultJsonOptions = ((JsonSerializerOptions)typeof(JsonSerializerOptions)
                .GetField("s_defaultOptions", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                .GetValue(null));

            defaultJsonOptions.PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy();

            return services;
        }
    }
}
