using System.Threading.Tasks;
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

        //[NUnit.Framework.Ignore("Test not verify method of mock object")]
        [Test]
        public void Start_WhenCalled_InvokeMockObjectMethod()
        {
           //Arrange
            _messageService = new MessageGetter(_mockReader.Object , _mockHubContext.Object);
            _mockReader.SetupSequence(x => x.Count())
                .Returns(1)
                .Returns(0);
            _mockReader.Setup(mock => mock.GetMessage());
            //act
            _messageService.Start();

            //Assert
            _mockReader.Verify(mock=>mock.GetMessage(), Times.AtLeastOnce());
            
        }

        [Test]
        public void Start_WhenCalled_TimerIsNotNull()
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
