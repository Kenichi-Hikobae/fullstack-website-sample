using System.Linq;

namespace WebsiteServerApp;

public enum AppSettingType
{
    MongoDbConnectionString,
    APIBaseURL,
    WebBaseURL,
}

public class AppSettings : IAppSettings
{
    public IConfiguration Configuration { get; private set; }
    public bool IsInitialized => Configuration is not null;

    public AppSettings(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public string GetSetting(AppSettingType type)
    {
        return Configuration[string.Join(":", type.ToString().Split("_"))] ?? string.Empty;
    }

    public bool TryGetSetting(AppSettingType type, out string value)
    {
        value = string.Empty;
        if (!IsInitialized)
            return false;

        value = Configuration[string.Join(":", type.ToString().Split("_"))] ?? string.Empty;

        return string.IsNullOrEmpty(value);
    }
}
