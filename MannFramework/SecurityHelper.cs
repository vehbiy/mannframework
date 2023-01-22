using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public static class SecurityHelper
    {
        private static string passPhrase;

        public static string PassPhrase
        {
            set
            {
                passPhrase = value;
            }
        }

        private static bool IsDevelopment
        {
            get
            {
                return MannFrameworkConfiguration.CurrentMode == MannFrameworkModeType.Development;
            }
        }

        public static string GetSecureValue(string PlainValue)
        {
            if (MannFrameworkConfiguration.UseModeSettings && IsDevelopment)
            {
                return PlainValue;
            }

            return Encryption.Encrypt(PlainValue, passPhrase);
        }

        public static string GetPlainValue(string SecureValue)
        {
            if (MannFrameworkConfiguration.UseModeSettings && IsDevelopment)
            {
                return SecureValue;
            }

            if (string.IsNullOrEmpty(SecureValue))
            {
                return SecureValue;
            }

            return Encryption.Decrypt(SecureValue, passPhrase);
        }
    }
}
