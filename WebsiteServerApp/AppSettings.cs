namespace WebsiteServerApp;

/// <summary>
/// The current appsetting registered to be used in by the API.
/// </summary>
public enum AppSettingType
{
    MongoDbConnectionString,
    APIBaseURL,
    WebBaseURL,
}

/// <summary>
/// Class that implements methods for getting appsettings from the API client.
/// </summary>
public class AppSettings : IAppSettings
{
    private readonly IConfiguration _configuration;
    public bool IsInitialized => _configuration is not null;

    public AppSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <inheritdoc/>
    public string GetSetting(AppSettingType type)
    {
        return _configuration[string.Join(":", type.ToString().Split("_"))] ?? string.Empty;
    }

    /// <inheritdoc/>
    public bool TryGetSetting(AppSettingType type, out string value)
    {
        value = string.Empty;
        if (!IsInitialized)
            return false;

        value = _configuration[string.Join(":", type.ToString().Split("_"))] ?? string.Empty;

        return string.IsNullOrEmpty(value);
    }
}
