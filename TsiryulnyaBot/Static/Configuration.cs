using Microsoft.Extensions.Configuration;

namespace TsiryulnyaBot.Static
{
    public static class Configuration
    {
        private static IConfigurationRoot _configurationBuilder;

        public static string Get(string key)
        {
            if (_configurationBuilder !=  null) 
            {
                return _configurationBuilder[key];
            }

            _configurationBuilder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true).Build();

            return _configurationBuilder[key];
        }
    }
}
