using System.Linq;
using System.Threading;
using MessageConsumer.Services;
using NUnit.Framework;


namespace MessageConsumer.Tests
{
    [TestFixture]
    public class HubGroupManagerTests
    {
        [Test]
        public void Add_WhenCalled_CountUsersWithThreeConnectionId()
        {
            var roleManager = new HubGroupManager<string>();

            roleManager.AddToGroup("User","User1","firstConnection");
            roleManager.AddToGroup("User", "User1", "secondConnection");
            roleManager.AddToGroup("User", "User1", "thirdConnection");

            Assert.AreEqual(1, roleManager?.GetUsersFromGroup("User").Count());
            Assert.AreEqual(3, roleManager.GetUsersFromGroup("User")
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

            var result = roleManager.IsGroupExist("User");

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
            Thread t2 = new Thread(() => RemoveInThread(roleManager, 0, 500));
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();

            var users = roleManager.GetUsersFromGroup("User");
            Assert.That(users.Count(), Is.LessThan(1000));
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
        private void GetUserGroupsByIdInThread(HubGroupManager<string> roleManager, int from, int to)
        {
            for (int i = from; i < to; i++)
            {
                roleManager.GetUserGroupsById($"User{i}");
            }
        }
        private void GetUserGroupsByConnectionIdInThread(HubGroupManager<string> roleManager, int from, int to)
        {
            for (int i = from; i < to; i++)
            {
                roleManager.GetUserGroupByConnectionId("Connection");
            }
        }
        private void GetUsersFromGroupInThread(HubGroupManager<string> roleManager, int from, int to)
        {
            for (int i = from; i < to; i++)
            {
                roleManager.GetUsersFromGroup("User");
            }
        }


        [Test]
        public void MultiThreadTest_AllAction_RunAllMethods()
        {
            var roleManager = new HubGroupManager<string>();
            Thread t1 = new Thread(() => AddInThread(roleManager, 0, 1000));
            Thread t2 = new Thread(() => RemoveInThread(roleManager, 0, 500));
            Thread t3 = new Thread(()=> GetUserGroupsByIdInThread(roleManager,0,722));
            Thread t4 = new Thread(()=> GetUserGroupsByConnectionIdInThread(roleManager,0,500));
            Thread t5 = new Thread(()=> GetUsersFromGroupInThread(roleManager, 0 , 432));
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();
            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();
            t5.Join();
        }

        [Test]
        public void GetUserGroupsByConnectionId_WhenCalled_ReturnEnumerableOfUsersGroup()
        {
            var roleManager = new HubGroupManager<string>();

            roleManager.AddToGroup("User", "User1", "firstConnection");
            roleManager.AddToGroup("Admin", "User1", "firstConnection");
            roleManager.AddToGroup("Coach", "User2", "secondConnection");
            roleManager.AddToGroup("User", "User2", "secondConnection");

            var groups = roleManager.GetUserGroupByConnectionId("firstConnection").ToList();

            Assert.IsTrue(groups.Contains("Admin")&&groups.Contains("User"));
        }

        [Test]
        public void GetUserGroupById_WhenCalled_ReturnEnumerableOfUsersGroup()
        {
            var roleManager = new HubGroupManager<string>();

            roleManager.AddToGroup("User", "User1", "firstConnection");
            roleManager.AddToGroup("Admin", "User1", "firstConnection");
            roleManager.AddToGroup("Coach", "User2", "secondConnection");
            roleManager.AddToGroup("User", "User2", "secondConnection");

            var groups = roleManager.GetUserGroupsById("User2").ToList();

            Assert.IsTrue(groups.Contains("Coach") && groups.Contains("User"));
        }

        [Test]
        public void GetUserFromGroup_WhenCalled_ReturnEnumerableOfUsers()
        {
            var roleManager = new HubGroupManager<string>();

            roleManager.AddToGroup("User", "User1", "firstConnection");
            roleManager.AddToGroup("Admin", "User1", "firstConnection");
            roleManager.AddToGroup("Coach", "User2", "secondConnection");
            roleManager.AddToGroup("User", "User2", "secondConnection");

            var groups = roleManager.GetUsersFromGroup("User");
            var test = roleManager.GetUsersFromGroup("NotExist");

            Assert.IsEmpty(test);
            Assert.IsTrue(groups.Count()==2);
        }

        [Test]
        public void GetConnectionIdFromGroup_WhenCalled_ReturnEnumerableOfConnectionId()
        {
            var roleManager = new HubGroupManager<string>();

            roleManager.AddToGroup("User", "User1", "firstConnection");
            roleManager.AddToGroup("Admin", "User1", "firstConnection");
            roleManager.AddToGroup("Coach", "User2", "secondConnection");
            roleManager.AddToGroup("User", "User2", "secondConnection");
            roleManager.AddToGroup("User", "User3", "NewConnection");

            var groups = roleManager.GetConnectionIdFromGroup("User").ToList();

            Assert.IsTrue(groups.Contains("NewConnection")&&groups.Contains("firstConnection"));
        }


    }
}
