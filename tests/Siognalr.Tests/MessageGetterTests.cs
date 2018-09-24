using System;
using Microsoft.AspNet.SignalR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
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
        private Mock<IQueueReader> _mockReader;
        private Mock<IHubContext> _mockHubContext;
        private  MessageGetter _messageService;
        public MessageGetterTests()
        {
             _mockReader = new Mock<IQueueReader>();
            _mockHubContext = new Mock<IHubContext>();
        }
        [Test]
        public void MessageGetter_GetMessageRun()
        {
            bool messageEvent = false;

            _messageService = new MessageGetter(_mockReader.Object , _mockHubContext.Object);
            _mockReader.Setup(x => x.Count()).Returns(1);
            _mockReader.Setup(mock => mock.GetMessage());
            //act
            _messageService.Start();


            
        }

        [Test]
        public void MessageGetter_Start_TimerIsNotNull()
        {
            //arrange
            _messageService = new MessageGetter(_mockReader.Object, _mockHubContext.Object);

            //act
            _messageService.Start();

            //assert
            Assert.IsNotNull(_messageService._timer);
        }


    }
}
