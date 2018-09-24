using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using SignalRPlusAzureQueue.Hubs;
using SignalRPlusAzureQueue.Interfaces;
using SignalRPlusAzureQueue.Sevices;
using Assert = NUnit.Framework.Assert;

namespace Siognalr.Tests
{
    /// <summary>
    /// Summary description for MessageGetterTests
    /// </summary>
    [TestClass]
    public class MessageGetterTests
    {
        private IQueueReader _mockReader;
        private IHubContext _mockHubContext;
        private static MessageGetter _messageService;
        public MessageGetterTests()
        {
            var _mockReader = Mock.Of<IQueueReader>();
            var _mockHubContext = Mock.Of<IHubContext>();
            _messageService = MessageGetter.GetInstance(_mockReader, _mockHubContext);
        }
        [Test]
        public void MessageGetter_GetInstance_InstanceIsNotNull()
        {
            Assert.IsNotNull(MessageGetter.Instance);
        }

        [Test]
        public void MessageGetter_Start_TimerIsNotNull()
        {
            _messageService.Start();
            Assert.IsNotNull(_messageService._timer);
        }


    }
}
