using NUnit.Framework;
using Moq;
using SignalRPlusAzureQueue.Hubs;
using SignalRPlusAzureQueue.Interfaces;

namespace Siognalr.Tests
{
    [TestFixture]
    public class MessageHubTests
    {
        private MessageHub _messageHub;

        [SetUp]
        public void SetUp()
        {
            _messageHub = new MessageHub();
        }

        [Test]
        public void InstantiationTest()
        {
            _messageHub.OnConnection();
        }

        [Test]
        public void MessageGetterTest()
        {
            IQueueReader FakeReader = Mock.Of<IQueueReader>();
        }
    }
}
