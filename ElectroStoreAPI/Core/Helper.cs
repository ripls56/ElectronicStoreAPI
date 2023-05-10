using System.Security.Cryptography;
using System.Text;

namespace ElectroStoreAPI.Core
{
    public class Helper
    {
        public static string ToSha256(string plainText)
        {
            using SHA256 sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));
            var stringbuilder = new StringBuilder();
            foreach (var t in bytes)
            {
                stringbuilder.Append(t.ToString("x2"));
            }
            return stringbuilder.ToString();
        }
    }
}
