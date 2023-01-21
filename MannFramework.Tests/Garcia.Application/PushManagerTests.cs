using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MannFramework.Application.Manager;

namespace MannFramework.Tests.MannFramework.Application
{
    [TestClass]
    public class PushManagerTests
    {
        [TestMethod]
        public void SendSocketIONotificationTest()
        {
            PushManager.SendSocketIONotification("send message", "10", "100");
        }
    }
}
