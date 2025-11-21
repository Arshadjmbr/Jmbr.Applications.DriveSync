using System.Text;

namespace DriveSync.Utility
{
    public class DecryptionUtility
    {
        public static string Base64Decode(string encrptedText)
        {
            byte[] apiKeyBytes = Convert.FromBase64String(encrptedText);
            string decodedApiKey = Encoding.UTF8.GetString(apiKeyBytes);
            return decodedApiKey;
        }
    }
}
