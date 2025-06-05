using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace Restaurant.Api.Services.Utils
{
    public static class CryptoUtils
    {
        public static string ToSha512String(this string @string)
        {
            var data = Encoding.UTF8.GetBytes(@string);
            var hash = SHA512.HashData(data);
            var result = string.Join("", hash.Select(x => x.ToString("x2")));
            return result;  
        }
        public static string ToSHA256String(this string @string)
        {
            var data = Encoding.UTF8.GetBytes(@string);
            var hash = SHA256.HashData(data);
            var result = string.Join("", hash.Select(x => x.ToString("x2")));
            return result;
        }
    }
}
