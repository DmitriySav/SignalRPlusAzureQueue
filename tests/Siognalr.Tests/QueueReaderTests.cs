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
        private QueueReader _queueReader;
        public QueueReaderTests()
        {
            _queueService = new Mock<IAzureQueueStorageService>();
        }

        [Test]
        public void GetMessage_WhenCalled_IvokeEventCall()
        {
            //arrange
            _queueService.Setup(x => x.GetMessage()).Returns("Message");
            _queueReader = new QueueReader(_queueService.Object);
            bool messageEvent = false;
            _queueReader.OnGetMessage += (e) => messageEvent = true;

            //act
            _queueReader.GetMessage();

            //assert
            Assert.IsTrue(messageEvent);
        }

        [Test]
        public void GetMessage_WhenCalled_InvokeEventCall_MessageIsHello()
        {
            //arrange
            _queueService.Setup(x => x.GetMessage()).Returns("Hello");
            _queueReader = new QueueReader(_queueService.Object);
            string message = string.Empty;

            _queueReader.OnGetMessage += (s) => message = s;
            
            //act
            _queueReader.GetMessage();

            //assert
            Assert.AreEqual("Hello", message);
        }

        [Test]
        public void Count_WhenCalled_ReturnOne()
        {
            //arrange
            _queueService.Setup(x => x.QueueCount()).Returns(1);
            _queueReader = new QueueReader(_queueService.Object);

            //act
            int num = _queueReader.Count();

            //assert
            Assert.AreEqual(num,1);
        }
    }
}
