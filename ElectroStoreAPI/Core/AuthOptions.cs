using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ElectroStoreAPI.Core
{
    public class AuthOptions
    {
        //public AuthOptions(string login)
        //{
        //    AUDIENCE = login;
        //}
        public const string ISSUER = "ElectronicStoreAPI"; // издатель токена
        public const string AUDIENCE = "user"; // потребитель токена
        const string KEY = "electronicstoreapi228"; // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
