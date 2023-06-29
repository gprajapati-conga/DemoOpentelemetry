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
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IFeatureFlag featureFlag, ILogger<WeatherForecastController> logger)
        {
            this.featureFlag = featureFlag;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var featureFlagName = "platform-enable-datachange-stream-notifications";
            var defaultValue = false;
            var organizationFriendlyId = $"test-tenant-1";

            var userObject = new FeatureFlagUser() { TenantId = organizationFriendlyId };
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