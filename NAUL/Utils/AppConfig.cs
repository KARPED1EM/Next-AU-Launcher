using Microsoft.Win32;

namespace NAUL;

public static class AppConfig
{
    private const string AppKeyName = "Next-AU-Launcher";
    private static RegistryKey SoftwareKeys => Registry.CurrentUser.OpenSubKey("Software", true);
    private static RegistryKey _Keys;
    public static RegistryKey Keys
    {
        get
        {
            _Keys ??= SoftwareKeys.OpenSubKey(AppKeyName, true);
            _Keys ??= SoftwareKeys.CreateSubKey(AppKeyName, true);
            return _Keys;
        }
        set { _Keys = value; }
    }

    public static T GetValue<T>(AppConfigs key) => GetValue<T>(key.ToString());
    public static T GetValue<T>(string key)
    {
        var value = Keys.GetValue(key) ?? default;
        return (T)value;
    }
    
    public static void SetValue(AppConfigs key, object value) => SetValue(key.ToString(), value);
    public static void SetValue(string key, object value)
    {
        Keys.SetValue(key, value);
    }
}

public enum AppConfigs
{
    DataPath,
    SelectedVersion,
}