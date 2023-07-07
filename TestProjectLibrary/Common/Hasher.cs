using System.Security.Cryptography;
using System.Text;

namespace TestProjectLibrary.Common;

public static class Hasher
{
    public static string Hash(string toHash)
    {
        using var hash = SHA256.Create();
        return BitConverter.ToString(hash.ComputeHash(
            Encoding.UTF8.GetBytes(toHash))
        ).Replace("-", string.Empty);
    }
}