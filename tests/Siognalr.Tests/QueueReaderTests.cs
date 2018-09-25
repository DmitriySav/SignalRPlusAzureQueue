using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using SignalRPlusAzureQueue.Interfaces;
using SignalRPlusAzureQueue.Readers;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Siognalr.Tests
{
    /// <summary>
    /// Summary description for QueueReaderTests
    /// </summary>
    [TestClass]
    public class QueueReaderTests
    {
        private Mock<IAzureQueueStorageService> _queueService;
        private QueueReader queueReader;
        public QueueReaderTests()
        {
            _queueService = new Mock<IAzureQueueStorageService>();
        }

        [Test]
        public void GetMessage_WhenCalled_IvokeEventCall()
        {
            //arrange
            _queueService.Setup(x => x.GetMessage()).Returns("Message");
            queueReader = new QueueReader(_queueService.Object);
            bool messageEvent = false;
            queueReader.OnGetMessage += (e) => messageEvent = true;

            //act
            queueReader.GetMessage();

            //assert
            Assert.IsTrue(messageEvent);
        }

        [Test]
        public void GetMessage_WhenCalled_IvokeEventCall_MessageIsHello()
        {
            //arrange
            _queueService.Setup(x => x.GetMessage()).Returns("Hello");
            queueReader = new QueueReader(_queueService.Object);
            string message = string.Empty;

            queueReader.OnGetMessage += (s) => message = s;
            
            //act
            queueReader.GetMessage();

            //assert
            Assert.AreEqual("Hello", message);
        }

        [Test]
        public void Count_WhenCalled_ReturnOne()
        {
            //arrange
            _queueService.Setup(x => x.QueueCount()).Returns(1);
            queueReader = new QueueReader(_queueService.Object);

            //act
            int num = queueReader.Count();

            //assert
            Assert.AreEqual(num,1);
        }
    }
}
