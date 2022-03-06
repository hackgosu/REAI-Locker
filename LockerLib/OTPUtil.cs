using OtpNet;
namespace LockerLib
{

    public class OTPUtil
    {
        public static string GetBase32Secret()
        {
            var secret = KeyGeneration.GenerateRandomKey();
            return Base32Encoding.ToString(secret);
        }
        public static bool checkOTP(string base32Secret,string inputOTPCode)
        {
            var secret = Base32Encoding.ToBytes(base32Secret);
            var totp = new Totp(secret);
            return totp.VerifyTotp(inputOTPCode, out long timeStepMatched, VerificationWindow.RfcSpecifiedNetworkDelay);

        }
    }
}