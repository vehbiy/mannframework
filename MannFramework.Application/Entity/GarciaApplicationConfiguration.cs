using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    public static class GarciaApplicationConfiguration
    {
        public static string ApplePushCertificateLocation { get; set; }
        public static string ApplePushCertificatePassword { get; set; }
        public static bool ApplePushForProduction { get; set; }
        public static string AndroidPushToken { get; set; }
        public static string FileUploadPath { get; set; }
        public static string AWSAccessKey { get; set; }
        public static string AWSSecret { get; set; }
        public static string AWSBucketName { get; set; }
        public static bool UploadFilesToAWS { get; set; }
        public static bool UploadFilesToAzure { get; set; }
        public static string AzureConnectionString { get; set; }
        public static string AzureBlobContainer { get; set; }
        public static string SocketIOUrl { get; set; }
        public static string SocketIOApiKey { get; set; }

        static GarciaApplicationConfiguration()
        {
            GarciaConfigurationManager.SetConfigurationValues(typeof(GarciaApplicationConfiguration));
        }
    }
}
