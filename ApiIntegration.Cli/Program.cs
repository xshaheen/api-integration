using System;
using System.Threading.Tasks;
using ApiIntegration.Cli.Api;
using ApiIntegration.Cli.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;

namespace ApiIntegration.Cli {
    public class Program {
        public static async Task Main(string[] args) {
            var config = _BuildConfiguration();
            var serviceProvider = _BuildServiceProvider(config);

            var app = serviceProvider.GetRequiredService<RestaurantsSearchApplication>();

            await app.RunAsync(args);
        }

        private static IConfigurationRoot _BuildConfiguration() {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables("CLI_")
                .Build();
        }

        private static ServiceProvider _BuildServiceProvider(IConfiguration config) {
            var services = new ServiceCollection();
            _ConfigureServices(services, config);
            return services.BuildServiceProvider();
        }

        private static void _ConfigureServices(IServiceCollection services, IConfiguration config) {
            services.AddSingleton<RestaurantsSearchApplication>();
            services.AddSingleton<IConsoleWriter, ConsoleWriter>();
            services.AddSingleton<IRestaurantsSearchService, RestaurantsSearchService>();

            services.AddValidatorsFromAssemblyContaining<Program>();

            services.AddRefitClient<IRestaurantApi>()
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[] {
                    TimeSpan.FromMilliseconds(100),
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                }))
                .ConfigureHttpClient(client => client.BaseAddress = new Uri(config["RestaurantApi:BaseAddress"]));
        }
    }
}
