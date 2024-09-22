using Cardoso.Dynamic.Configuration;
using Microsoft.Extensions.Configuration;

namespace Cardoso.Configuration
{
    public class LocalEnvConfigurationService : IConfigurationService
    {
        private readonly IConfigurationRoot _configuration;

        public LocalEnvConfigurationService(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddConfiguration(configuration)
                .AddJsonFile("additionalappsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public override string GetProperty(string key)
        {
            return _configuration[key] ?? "";
        }

        public override string GetProperty(string key, string fallback)
        {
            return _configuration[key] ?? fallback;
        }

        public override void UpdateAll()
        {
            _configuration.Reload();
        }
    }
}