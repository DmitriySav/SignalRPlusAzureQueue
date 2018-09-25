using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using NUnit.Framework;
using Moq;
using SignalRPlusAzureQueue.Hubs;
using SignalRPlusAzureQueue.Interfaces;
using SignalRPlusAzureQueue.Sevices;

namespace Siognalr.Tests
{
    [TestFixture]
    public class MessageHubTests
    {
        private Mock<IMessageService> _messageServiceMock;
        private MessageHub _messageHub;

        public MessageHubTests()
        {
            _messageServiceMock = new Mock<IMessageService>();
        }

        public interface IClientContract
        {
            void broadcastMessage(string name);
        }

        [Test]
        public void HubMockingTest()
        {
            //arrange 
            _messageHub = new MessageHub(_messageServiceMock.Object);
            var mockClient = new Mock<IHubCallerConnectionContext<dynamic>>();
            var all = new Mock<IClientContract>();
            _messageHub.Clients = mockClient.Object;
            all.Setup(m => m.broadcastMessage(It.IsAny<string>())).Verifiable();
            mockClient.Setup(m => m.All).Returns(all.Object);

            //act
            _messageHub.OnConnection();

            //Assert
            all.VerifyAll();

        }

        [Test]
        public void Start_whanCalled_RunMessageGetterStarMethod()
        {
            //arrange 
            _messageHub = new MessageHub(_messageServiceMock.Object);

            //act
            _messageHub.Start();

            //assert
            _messageServiceMock.Verify(m=>m.Start());

        }


    }
}
