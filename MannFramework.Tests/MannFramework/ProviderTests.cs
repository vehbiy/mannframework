using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MannFramework;

namespace MannFramework.Tests.MannFramework
{
    [TestClass]
    public class ProviderTests
    {
        //[TestMethod]
        //public void GetItemsTest()
        //{
        //    //List<Person> items = Provider<Person>.Instance.GetItems();
        //    //List<Person> items = Provider3<Person>.Instance.GetItems();
        //}

        [TestMethod]
        public void ExecuteSqlJoinTest()
        {
            DatabaseResponse response = SqlDatabaseConnection.Instance.ExecuteItems("select p.Id as [p.Id], a.Id as [a.Id] from person p join address a on p.Id = a.personId", null);
        }
    }

    [TestClass]
    public class Provider2Tests
    {
        [TestMethod]
        public void GetItemsTest()
        {
            MannFrameworkConfigurationManager.SetConfigurationValues(typeof(MannFrameworkConfiguration));
            //List<Person> items = Provider3<Person>.Instance.GetItems();
            //Assert.IsNotNull(items);
            //List<Entity<int>> items2 = EntityManager.Instance.GetItems(typeof(Person));
            //Assert.IsNotNull(items2);
            List<Person> items3 = EntityManager.Instance.GetItems<Person>();
            Assert.IsNotNull(items3);
            List<Person> items4 = PersonProvider.Instance.GetItems();
            Assert.IsNotNull(items4);

            List<Person> items5 = EmptyPersonProvider.Instance.GetItems();
            Assert.IsNotNull(items5);

            Person item = EntityManager.Instance.GetItem<Person>(1);
        }

        [TestMethod]
        public void SaveTest()
        {
            Person item = TestClassFactory.CreatePerson();

            //OperationResult result = PersonProvider.Instance.Save(item);
            //Assert.IsTrue(result.Success);

            OperationResult result = result = EntityManager.Instance.Save(item);
            Assert.IsTrue(result.Success);
        }

        public void TransactionTest()
        {
            //Guid transactionId = EntityManager.Instance.BeginTransaction();
            //EntityManager.Instance.Save<Person>(null);
        }
    }
}
