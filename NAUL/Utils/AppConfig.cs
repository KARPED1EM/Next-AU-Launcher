using Microsoft.Win32;

namespace NAUL;

public static class AppConfig
{
    private static RegistryKey SoftwareKeys => Registry.CurrentUser.OpenSubKey("Software", true);
    private static RegistryKey _Keys;
    public static RegistryKey Keys
    {
        get
        {
            _Keys ??= SoftwareKeys.OpenSubKey("Next-AU-Launcher", true);
            return _Keys;
        }
        set { _Keys = value; }
    }

    public static T GetValue<T>(string key)
    {
        var value = Keys.GetValue(key);
        return (T)value ?? default;
    }

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