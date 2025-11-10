using System.Security.Cryptography;
using System.Text;

namespace DriveSync.Hashing
{
    public class PasswordHashing
    {
        public static string HashingPassword(string password)
        {
            string hashedPassword;
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = md5.ComputeHash(inputBytes);
                hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
            return hashedPassword;
        }
    }
}
