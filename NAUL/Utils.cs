using System;
using System.IO;
using System.Security.Cryptography;

namespace NAUL;

internal static class Utils
{
    public static string GetMD5HashFromFile(string fileName)
    {
        try
        {
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(fileName);
            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
        catch
        {
            return "";
        }
    }
}
