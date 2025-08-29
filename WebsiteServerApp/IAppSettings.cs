namespace WebsiteServerApp;

/// <summary>
/// Interface that contains basic method to get the desired setting from the API client.
/// </summary>
public interface IAppSettings
{
    /// <summary>
    /// Whether the appsettings has been initialized.
    /// </summary>
    bool IsInitialized { get; }
    /// <summary>
    /// Get the setting given the appsetting type.
    /// </summary>
    /// <param name="type">The type of the appsetting to get.</param>
    /// <returns>The value of the setting.</returns>
    public string GetSetting(AppSettingType type);
    /// <summary>
    /// Try to get a setting given the appsetting type.
    /// </summary>
    /// <param name="type">The type of the appsetting to get.</param>
    /// <param name="value">The value of the setting.</param>
    /// <returns>Whether the setting was found or not.</returns>
    public bool TryGetSetting(AppSettingType type, out string value);
}
