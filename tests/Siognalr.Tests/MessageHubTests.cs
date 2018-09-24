using Microsoft.AspNet.SignalR;
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

        
    }
}
