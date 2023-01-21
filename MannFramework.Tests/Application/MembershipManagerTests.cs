using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MannFramework.Application;

namespace MannFramework.Tests.Application
{
    [TestClass]
    public class MembershipManagerTests
    {
        [TestMethod]
        public void TestRegister()
        {
            Member member = new Member()
            {
                BirthDate = DateTime.Today.AddYears(-20),
                Name = "Name",
                Surname = "Surname",
                Email = Helpers.CreateKey(10) + "garciaframework.com"
            };

            OperationResult<Member> result = MembershipManager.Instance.Register(member);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Item);
        }
    }
}
