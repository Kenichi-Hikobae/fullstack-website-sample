namespace WebsiteServerApp;

public interface IAppSettings
{
    bool IsInitialized { get; }
    public string GetSetting(AppSettingType type);
    public bool TryGetSetting(AppSettingType type, out string value);
}
