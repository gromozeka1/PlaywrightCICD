using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestFramework.Config
{
    public static class ConfigReader
    {
        public static TestSettings ReadConfig()
        {
            var configFile = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/appsettings.json");

            var jsonSerilizerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

            jsonSerilizerOptions.Converters.Add(new JsonStringEnumConverter());

            return JsonSerializer.Deserialize<TestSettings>(configFile, jsonSerilizerOptions) ?? throw new ArgumentNullException();
        }
    }
}
