using Microsoft.Extensions.ServiceDiscovery;
using Microsoft.Extensions.ServiceDiscovery.Http;

namespace ServicediscoveryDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddServiceDiscovery();

            builder.Services.ConfigureHttpClientDefaults(static http =>
            {
                http.AddServiceDiscovery();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.MapGet("/report", async (IHttpClientFactory factory) =>
            {
                const string serviceName = "weatherReport";
                var client = factory.CreateClient();
                var response = await client.GetAsync($"http://{serviceName}/weatherforecast");
                var content = await response.Content.ReadAsStringAsync();

                return content;
            });

            app.Run();
        }
    }
}
