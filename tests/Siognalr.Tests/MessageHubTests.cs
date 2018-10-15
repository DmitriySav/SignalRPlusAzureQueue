using MessageConsumer.Hubs;
using MessageConsumer.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace MessageConsumer.Tests
{
    [TestFixture]
    public class MessageHubTests
    {
        private readonly Mock<IMessageService> _messageServiceMock;
       // private MessageHub _messageHub;

        public MessageHubTests()
        {
            _messageServiceMock = new Mock<IMessageService>();
        }

        //[Test]
        //public void HubMockingTest()
        //{
        //    //arrange 
        //    _messageHub = new MessageHub(_messageServiceMock.Object);
        //    var mockClient = new Mock<IHubCallerConnectionContext<dynamic>>();
        //    var clientContractMock = new Mock<IClientContract>();
        //    _messageHub.Clients = mockClient.Object;
        //    clientContractMock.Setup(m => m.Message(It.IsAny<string>())).Verifiable();
        //    mockClient.Setup(m => m.Caller).Returns(clientContractMock.Object);

        //    //act
        //    _messageHub.OnConnected();

        //    //Assert
        //    clientContractMock.VerifyAll();

        //}

        //[Test]
        //public void Start_whanCalled_RunMessageGetterStarMethod()
        //{
        //    //arrange 
        //    _messageHub = new MessageHub(_messageServiceMock.Object);

        //    //act
        //    _messageHub.Start();

        //    //assert
        //    _messageServiceMock.Verify(m => m.Start());

        //}



    }
}
