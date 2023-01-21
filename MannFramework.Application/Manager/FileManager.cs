using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;

namespace MannFramework.Application
{
    // TODO: Webapi controllerbase'e almak daha mantikli olabilir
    public static class FileManager
    {
        public static FileUploadResult UploadVideo(MultipartFormDataStreamProvider StreamProvider, string Prefix = "")
        {
            string uploadPath = GarciaApplicationConfiguration.FileUploadPath;
            var data = StreamProvider.FileData;
            string video = string.Empty;
            string image = string.Empty;
            string guid = Guid.NewGuid().ToString().Replace("-", "");

            foreach (MultipartFileData entry in data)
            {
                List<string> values = entry.Headers.GetValues("content-type").ToList();
                string newFileName = string.Empty;

                switch (values[0])
                {
                    case "video/mp4":
                        newFileName = uploadPath + Prefix + guid + ".mp4";
                        video = newFileName;
                        break;
                    case "image/jpeg":
                        newFileName = uploadPath + Prefix + guid + ".jpg";
                        image = newFileName;
                        break;
                }

                if (!string.IsNullOrEmpty(newFileName))
                {
                    File.Move(entry.LocalFileName, newFileName);
                }

                if (!string.IsNullOrEmpty(video) && !string.IsNullOrEmpty(image))
                {
                    break;
                }
            }

            string awsBucketName = GarciaApplicationConfiguration.AWSBucketName;
            bool uploadFilesToAWS = GarciaApplicationConfiguration.UploadFilesToAWS;
            bool uploadFilesToAzure = GarciaApplicationConfiguration.UploadFilesToAzure;

            if (uploadFilesToAWS)
            {
                string awsKey = GarciaApplicationConfiguration.AWSAccessKey;
                string awsSecret = GarciaApplicationConfiguration.AWSSecret;
                AmazonS3Client client = new AmazonS3Client(awsKey, awsSecret);
                TransferUtility transferUtility = new TransferUtility(client);

                if (!string.IsNullOrEmpty(video))
                {
                    transferUtility.Upload(video, awsBucketName, video.Replace(uploadPath, ""));
                }

                if (!string.IsNullOrEmpty(image))
                {
                    transferUtility.Upload(image, awsBucketName, image.Replace(uploadPath, ""));
                }
            }

            if (uploadFilesToAzure)
            {
                //string storageConnectionString = CloudConfigurationManager.GetSetting("AzureConnectionString");
                string storageConnectionString = GarciaApplicationConfiguration.AzureConnectionString;
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                string azureBlobContainer = GarciaApplicationConfiguration.AzureBlobContainer;
                CloudBlobContainer container = blobClient.GetContainerReference(azureBlobContainer);
                //container.CreateIfNotExists();

                if (!string.IsNullOrEmpty(video))
                {
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(video.Replace(uploadPath, ""));
                    blockBlob.UploadFromFile(video);
                }

                if (!string.IsNullOrEmpty(image))
                {
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(video.Replace(uploadPath, ""));
                    blockBlob.UploadFromFile(image.Replace(uploadPath, ""));
                }
            }

            image = image.Replace(uploadPath, "");
            video = video.Replace(uploadPath, "");
            return new FileUploadResult() { FileName = video, Thumbnail = image };
        }

        public static string ReadWithoutLock(string path)
        {
            string text = string.Empty;

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    text = sr.ReadToEnd();
                }
            }

            return text;
        }
    }
}
