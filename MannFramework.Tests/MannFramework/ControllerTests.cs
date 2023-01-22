using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Tests.MannFramework
{
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void CreateModelTest()
        {
            List<Person> items = EntityManager.Instance.GetItems<Person>();
            Person item = items.FirstOrDefault();
            MannFrameworkApiController controller = new MannFrameworkApiController();
            dynamic model = controller.DynamicModelGenerator.GenerateModel<int>(item);
            string serializedModel = JsonConvert.SerializeObject(model);

            PersonModel personModel = new PersonModel(item);
            string serializedModel2 = JsonConvert.SerializeObject(personModel);
        }

        [TestMethod]
        public void CreateModelsTest()
        {
        }
    }
}
