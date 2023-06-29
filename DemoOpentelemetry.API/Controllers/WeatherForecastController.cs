using Microsoft.AspNetCore.Mvc;
using LaunchDarkly.Sdk;
using LaunchDarkly.Sdk.Server;
using DemoOpentelemetry.API.LaunchDarkly;

namespace DemoOpentelemetry.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly IFeatureFlag featureFlag;
        private readonly IConfiguration configuration;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IFeatureFlag featureFlag, IConfiguration configuration, ILogger<WeatherForecastController> logger)
        {
            this.featureFlag = featureFlag;
            this.configuration = configuration;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var featureFlagName = configuration.GetSection("FeatureFlagName").Value;
            var defaultValue = false;
            var tenantId = $"test-tenant-1";

            var userObject = new FeatureFlagUser() { TenantId = tenantId };
            var ffValue = featureFlag.IsEnabled(featureFlagName, userObject, defaultValue);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}