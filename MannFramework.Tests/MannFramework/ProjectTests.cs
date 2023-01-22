using MannFramework.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Tests.MannFramework
{
    [TestClass]
    public class ProjectTests
    {
        [TestMethod]
        public void InsertProjectTest()
        {
            Project item = new Project()
            {
                IsActive = true,
                Name = "MannFramework"
            };

            OperationResult result = EntityManager.Instance.Save(item);
            Assert.IsTrue(result.Success);
        }
    }
}
