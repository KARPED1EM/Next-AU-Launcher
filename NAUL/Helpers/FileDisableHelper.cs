namespace NAUL;

internal static class File
{
    public const string DisabledSuffix = ".disabled";

    public static bool Exists(string path)
    {
        return System.IO.File.Exists(path) || System.IO.File.Exists(path + DisabledSuffix);
    }
    public static void Delete(string path)
    {
        System.IO.File.Delete(path);
        System.IO.File.Delete(path + DisabledSuffix);
        Utils.ClearMD5CahceFor(path);
        Utils.ClearMD5CahceFor(path + DisabledSuffix);
    }
    public static void Move(string sourceFileName, string destFileName, bool overwrite = true)
    {
        System.IO.File.Move(GetTruePath(sourceFileName), GetTruePath(destFileName), overwrite);
        Utils.ClearMD5CahceFor(GetTruePath(destFileName));
    }

    public static string GetTruePath(string path)
    {
        if (System.IO.File.Exists(path))
        {
            return path;
        }
        else
        {
            if (!path.EndsWith(DisabledSuffix)) path += DisabledSuffix;
            else path = path.Remove(path.IndexOf(DisabledSuffix));
            return path;
        }
    }
    public static bool IsEnabled(string path)
    {
        return !(GetTruePath(path)).EndsWith(DisabledSuffix);
    }
    public static void SetStatus(string path, bool enable)
    {
        path = GetTruePath(path);
        if (enable)
        {
            if (path.EndsWith(DisabledSuffix))
                System.IO.File.Move(path, path.Remove(path.IndexOf(DisabledSuffix)), true);
        }
        else
        {
            if (!path.EndsWith(DisabledSuffix))
                System.IO.File.Move(path, path + DisabledSuffix, true);
        }
    }
}
