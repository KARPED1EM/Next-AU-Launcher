using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace NAUL;

public static class Utils
{
    private static Dictionary<string, string> MD5Cache = new();

    public static void ClearCache() => MD5Cache.Clear();

    public static string GetMD5HashFromFile(string fileName, bool useCache = true)
    {
        if (useCache && MD5Cache.TryGetValue(fileName, out var cacheMd5))
            return cacheMd5;
        try
        {
            using var md5 = MD5.Create();
            using var stream = System.IO.File.OpenRead(fileName);
            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
        catch
        {
            return "";
        }
    }
}
