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
                return GarciaConfiguration.CurrentMode == GarciaModeType.Development;
            }
        }

        public static string GetSecureValue(string PlainValue)
        {
            if (GarciaConfiguration.UseModeSettings && IsDevelopment)
            {
                return PlainValue;
            }

            return Encryption.Encrypt(PlainValue, passPhrase);
        }

        public static string GetPlainValue(string SecureValue)
        {
            if (GarciaConfiguration.UseModeSettings && IsDevelopment)
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
