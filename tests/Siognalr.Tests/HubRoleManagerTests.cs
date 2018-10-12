using System;
using System.Linq;
using System.Threading;
using MessageConsumer.Infrastructure.Business;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MessageConsumer.Tests
{
    [TestFixture]
    public class HubRoleManagerTests
    {
        [Test]
        public void Add_WhenCalled_CountUsersWithThreeConnectionId()
        {
            var roleManager = new HubGroupManager<string>();

            roleManager.AddToGroup("User","User1","firstConnection");
            roleManager.AddToGroup("User", "User1", "secondConnection");
            roleManager.AddToGroup("User", "User1", "thirdConnection");

            Assert.AreEqual(1, roleManager.GetUsersFromGroup("User").Count());
            Assert.AreEqual(3, roleManager
                .GetUsersFromGroup("User")
                .FirstOrDefault().ConnectionIds.Count);
        }

        [Test]
        public void Add_whenCalled_addTwoUsers()
        {
            var roleManager = new HubGroupManager<string>();

            roleManager.AddToGroup("User", "User1", "thirdConnection");
            roleManager.AddToGroup("User", "User1", "firstConnection");
            roleManager.AddToGroup("User", "User2", "secondConnection");

            Assert.AreEqual(2, roleManager.GetUsersFromGroup("User").Count());
        }

        [Test]
        public void Add_addUsersToDifferentGroup_ReturnNumberOfGroup_3()
        {
            var roleManager = new HubGroupManager<string>();

            roleManager.AddToGroup("User", "User1", "thirdConnection");
            roleManager.AddToGroup("Admin", "User1", "firstConnection");
            roleManager.AddToGroup("Coach", "User2", "secondConnection");
            roleManager.AddToGroup("User", "User2", "secondConnection");

            Assert.AreEqual(3, roleManager.RolesGroup.Count);
        }

        [Test]
        public void Remove_RemoveAllUsersFromGroup_GroupIsDeleted_AssertFalse()
        {
            var roleManager = new HubGroupManager<string>();

            roleManager.AddToGroup("User", "User1", "thirdConnection");
            roleManager.AddToGroup("Admin", "User1", "firstConnection");
            roleManager.AddToGroup("Coach", "User2", "secondConnection");
            roleManager.AddToGroup("User", "User2", "secondConnection");

            roleManager.RemoveFromGroup("User1", "thirdConnection");
            roleManager.RemoveFromGroup("User2", "secondConnection");

            var result = roleManager.RolesGroup.ContainsKey("User");

            Assert.IsFalse(result);
        }

        [Test]
        public void Remove_RemoveUserConnection_UserIsNotDeleteIfConnectionExsist()
        {
            var roleManager = new HubGroupManager<string>();

            roleManager.AddToGroup("User", "User1", "thirdConnection");
            roleManager.AddToGroup("User", "User1", "firstConnection");
            roleManager.AddToGroup("User", "User2", "secondConnection");

            roleManager.RemoveFromGroup("User1", "thirdConnection");

            var user = roleManager.GetUsersFromGroup("User")
                .FirstOrDefault(x => x.UserId == "User1");

            bool result = user != null;

            Assert.IsTrue(result);
        }

        [Test]
        public void Add_addUsersInThread_addTwoUsers()
        {
            var roleManager = new HubGroupManager<string>();
            Thread t1 = new Thread(()=>AddInThread(roleManager, 0, 1000));
            Thread t2 = new Thread(() => AddInThread(roleManager,1000, 1500 ));
            t1.Start();
            t2.Start();
            t2.Join();
            t1.Join();

            var users = roleManager.GetUsersFromGroup("User");
            Assert.AreEqual(1500, users.Count());
            
        }

        [Test]
        public void AddandRemove_addUsersInThread_addTwoUsers()
        {
            var roleManager = new HubGroupManager<string>();
            Thread t1 = new Thread(() => AddInThread(roleManager, 0, 1000));
            Thread t2 = new Thread(() => RemoveInThread(roleManager, 500, 1000));
            t1.Start();
            t2.Start();
            t2.Join();
            t1.Join();

            var users = roleManager.GetUsersFromGroup("User");
            Assert.AreEqual(500, users.Count());

        }


        private void AddInThread(HubGroupManager<string> roleManager, int from, int to)
        {
            for (int i = from; i < to; i++)
            {
                roleManager.AddToGroup("User", $"User{i}", "Connection");
            }
        }

        private void RemoveInThread(HubGroupManager<string> roleManager, int from, int to)
        {
            for (int i = from; i < to; i++)
            {
                roleManager.RemoveFromGroup($"User{i}", "Connection");
            }
        }

    }
}
