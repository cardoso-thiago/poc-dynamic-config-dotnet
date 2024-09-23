using Core.DynamicConfig.Dynamic.Configuration;
using Microsoft.Extensions.Configuration;

namespace Core.LocalEnvConfig.Configuration
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


        public string GetProperty(string key)
        {
            return _configuration[key] ?? "";
        }

        public string GetProperty(string key, string fallback)
        {
            return _configuration[key] ?? fallback;
        }
    }
}