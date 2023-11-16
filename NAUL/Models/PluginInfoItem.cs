namespace NAUL.Models;

public class PluginInfoItem
{
    public string PluginName { get; set; }

    public PluginTypes PluginType { get; set; }

    public string IconUrl { get; set; }

    public string Author { get; set; }

    public string URL { get; set; }

    public string License { get; set; }

    public string Description { get; set; }
}

public enum PluginTypes
{
    Single,
    Additional,
}