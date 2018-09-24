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
        public void QueueReader_GetMessage_eventCall()
        {
            _queueService.Setup(x => x.GetMessage()).Returns("Message");
            queueReader = new QueueReader(_queueService.Object);
            bool messageEvent = false;


            queueReader.OnGetMessage += (e) => messageEvent = true;
            //act
            queueReader.GetMessage();

            Assert.IsTrue(messageEvent);
        }
    }
}
