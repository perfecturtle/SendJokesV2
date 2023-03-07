using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.DependencyInjection;
using SendJokesV2.Models;
using SendJokesV2.Services;

[assembly: FunctionsStartup(typeof(SendJokesV2.Startup))]
namespace SendJokesV2
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            RegisterInjectedServices(builder);
        }
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var builtConfig = builder.ConfigurationBuilder.Build();
        }
        private void RegisterInjectedServices(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IAPIService, APIService>();
            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddHttpClient("MyJokesAPI", (provider, client) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                client.BaseAddress = new Uri(configuration["AppSettings:BaseURLJokes"]);
            });

            builder.Services.AddHttpClient("RandomFactsAPI", (provider, client) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                client.BaseAddress = new Uri(configuration["AppSettings:BaseURLRandomFacts"]);
            });


            builder.Services.AddHttpClient("ComicsAPI", (provider, client) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                client.BaseAddress = new Uri(configuration["AppSettings:BaseURLComics"]);
            });

            builder.Services.AddOptions<AppSettings>().Configure<IConfiguration>((settings, configuration) => configuration.GetSection(nameof(AppSettings)).Bind(settings));
        }
    }

}
