using Microsoft.Extensions.Configuration;
using Cardoso.Dynamic.Configuration;

namespace Cardoso.Configuration
{
    public class LocalEnvConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public LocalEnvConfigurationService()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

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
    }
}

