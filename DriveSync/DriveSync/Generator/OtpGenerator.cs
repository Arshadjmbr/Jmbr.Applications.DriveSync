namespace DriveSync.Generator
{
    public class OtpGenerator
    {
        public static string GenerateOTP()
        {
            Random rand = new Random();
            int otpNumber = rand.Next(100000, 999999);
            return otpNumber.ToString();
        }
    }
}
