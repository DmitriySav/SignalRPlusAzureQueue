using NUnit.Framework;
using SignalRPlusAzureQueue.Hubs;

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
            
        }
    }
}
