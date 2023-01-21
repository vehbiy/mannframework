using Newtonsoft.Json.Linq;
using PushSharp.Apple;
using PushSharp.Google;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MannFramework.Application.Manager
{
    public static class PushManager
    {
        public static ApnsServiceBroker AppleBroker { get; set; }
        public static GcmServiceBroker AndroidBroker { get; set; }

        static PushManager()
        {
            CreateAppleBroker();
            CreateAndroidBroker();
        }

        private static void CreateAppleBroker()
        {
            string applePushCertificateLocation = GarciaApplicationConfiguration.ApplePushCertificateLocation;

            if (!string.IsNullOrEmpty(applePushCertificateLocation))
            {
                string applePushCertificatePassword = GarciaApplicationConfiguration.ApplePushCertificatePassword;
                bool applePushForProduction = GarciaApplicationConfiguration.ApplePushForProduction;
                byte[] appleCert = File.ReadAllBytes(applePushCertificateLocation);
                ApnsConfiguration appleConfig = new ApnsConfiguration(applePushForProduction ? ApnsConfiguration.ApnsServerEnvironment.Production : ApnsConfiguration.ApnsServerEnvironment.Sandbox, appleCert, applePushCertificatePassword);
                AppleBroker = new ApnsServiceBroker(appleConfig);
                AppleBroker.OnNotificationSucceeded += AppleBroker_OnNotificationSucceeded;
                AppleBroker.OnNotificationFailed += AppleBroker_OnNotificationFailed;
                AppleBroker.Start();
            }
        }

        private static void CreateAndroidBroker()
        {
            string androidPushToken = GarciaApplicationConfiguration.AndroidPushToken;

            if (!string.IsNullOrEmpty(androidPushToken))
            {
                GcmConfiguration gcmConfig = new GcmConfiguration(androidPushToken);
                AndroidBroker = new GcmServiceBroker(gcmConfig);
                AndroidBroker.OnNotificationSucceeded += AndroidBroker_OnNotificationSucceeded;
                AndroidBroker.OnNotificationFailed += AndroidBroker_OnNotificationFailed;
                AndroidBroker.Start();
            }
        }

        private static void AndroidBroker_OnNotificationFailed(GcmNotification notification, AggregateException exception)
        {
            //LoggingManager.Instance.SaveExceptionLog(LogTypesEnum.PushFail, exception);
        }

        private static void AndroidBroker_OnNotificationSucceeded(GcmNotification notification)
        {
            //LoggingManager.Instance.SaveLog(LogTypesEnum.PushSuccess, JsonConvert.SerializeObject(notification));
        }

        private static void AppleBroker_OnNotificationFailed(ApnsNotification notification, AggregateException exception)
        {
            //LoggingManager.Instance.SaveExceptionLog(LogTypesEnum.PushFail, exception);
        }

        private static void AppleBroker_OnNotificationSucceeded(ApnsNotification notification)
        {
            //LoggingManager.Instance.SaveLog(LogTypesEnum.PushSuccess, JsonConvert.SerializeObject(notification));
        }

        private static void QueueNotification(ApnsNotification appleNotification)
        {
            if (AppleBroker != null)
            {
                AppleBroker.QueueNotification(appleNotification);
            }
        }

        private static void QueueNotification(GcmNotification gcmNotification)
        {
            if (AndroidBroker != null)
            {
                AndroidBroker.QueueNotification(gcmNotification);
            }
        }

        public static void StopAllServices()
        {
            if (AppleBroker != null)
            {
                AppleBroker.Stop();
            }

            if (AndroidBroker != null)
            {
                AndroidBroker.Stop();
            }
        }

        private static void SendAppleNotification(string pushToken, int badgeCount, string message, string soundFile = null, int timeToLive = 24)
        {
            ApnsNotification appleNotification = new ApnsNotification(pushToken);
            JObject payload = new JObject();
            JObject aps = new JObject();
            aps.Add("badge", badgeCount);
            aps.Add("alert", message);
            aps.Add("sound", soundFile);
            payload.Add("aps", aps);
            payload.Add("custom", message);
            appleNotification.Payload = payload;
            appleNotification.Expiration = DateTime.Now.AddHours(timeToLive).ToUniversalTime();
            QueueNotification(appleNotification);
        }

        private static void SendAndroidNotification(string pushToken, int badgeCount, string message, int timeToLive = 24)
        {
            GcmNotification gcmNotification = new GcmNotification();
            JObject data = new JObject();
            int unreadNotificationCount = badgeCount;
            data.Add("badge", badgeCount);
            //data.Add("body", Message);
            data.Add("message", message);

            #region commented
            //JObject custom = new JObject();
            //string messageType = string.Empty;

            //switch (NotificationType)
            //{
            //    case NotificationTypesEnum.SendMessage:
            //    default:
            //        messageType = "new_message";
            //        break;
            //    case NotificationTypesEnum.NewMatch:
            //        messageType = "new_match";
            //        break;
            //    case NotificationTypesEnum.DeleteMatch:
            //        messageType = "delete_match";
            //        break;
            //}

            //custom.Add("message_type", messageType); 

            //JObject message = new JObject();

            //if (SenderId.HasValue)
            //{
            //    message.Add("sender_id", SenderId.Value);
            //}

            //custom.Add("message", message);
            //data.Add("custom", custom);
            #endregion

            gcmNotification.Data = data;
            //gcmNotification.Notification = data;
            gcmNotification.RegistrationIds = new List<string>() { pushToken };
            gcmNotification.TimeToLive = timeToLive * 3600;
            gcmNotification.DelayWhileIdle = true;
            QueueNotification(gcmNotification);
        }

        public static void SendNotification(MemberDevice device, string message, int badgeCount)
        {
            switch (device.DeviceType)
            {
                case DeviceType.IOS:
                    SendAppleNotification(device.PushToken, badgeCount, message);
                    break;
                case DeviceType.Android:
                    SendAndroidNotification(device.PushToken, badgeCount, message);
                    break;
                default:
                    break;
            }
        }

        public static void SendNotification(List<MemberDevice> devices, string message, int badgeCount)
        {
            foreach (MemberDevice device in devices)
            {
                switch (device.DeviceType)
                {
                    case DeviceType.IOS:
                        SendAppleNotification(device.PushToken, badgeCount, message);
                        break;
                    case DeviceType.Android:
                        SendAndroidNotification(device.PushToken, badgeCount, message);
                        break;
                    default:
                        break;
                }
            }
        }

        public static void SendSocketIONotification(string eventString, string from = null, string to = null, string id = null, object message = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = Guid.NewGuid().ToString();
            }

            string url = GarciaApplicationConfiguration.SocketIOUrl;
            string apiKey = GarciaApplicationConfiguration.SocketIOApiKey;
            IO.Options options = new IO.Options();
            options.Query = new Dictionary<string, string>();
            options.Query.Add("apikey", apiKey);
            options.Query.Add("id", id);
            Socket socket = IO.Socket(url, options);
            var parameters = new JObject();

            if (!string.IsNullOrEmpty(from))
            {
                parameters.Add("from", from);
            }

            if (!string.IsNullOrEmpty(to))
            {
                parameters.Add("to", to);
            }

            //if (message != null)
            //{
            //    parameters.Add(message);
            //}

            socket.On(Socket.EVENT_CONNECT, () =>
            {
                socket.Emit(eventString, message);
                socket.Disconnect();
            });

            //socket.Emit(Message, parameters);
            //socket.Close();
        }

        public static async void SendSocketIONotificationAsync(string eventString, string from = null, string to = null, string id = null, string message = null)
        {
            await Task.Run(() => SendSocketIONotification(eventString, from, to, id, message));
        }
    }
}
