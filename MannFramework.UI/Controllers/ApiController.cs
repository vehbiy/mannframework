using MannFramework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MannFramework;
using MannFramework.Macondo.Models;
using System.Web.Http;

namespace MannFramework.Macondo.Controllers
{
    public class ApiController : GarciaApiController
    {
        [Route("api/Project/Items/{id}")]
        public List<ItemModel> GetItems(int id)
        {
            Project project = EntityManager.Instance.GetItem<Project>(id);

            if (project != null)
            {
                List<ItemModel> models = Model.GetModels<Item, ItemModel>(project.Items);
                return models;
            }

            return null;
        }

        [Route("api/Icon/{id}")]
        public Icon GetIcon(int id)
        {
            Icon icon = EntityManager.Instance.GetItem<Icon>(id);
            return icon;
        }

        [Route("api/Icon")]
        public List<Icon> GetIcons()
        {
            List<Icon> icons = EntityManager.Instance.GetItems<Icon>();
            return icons;
        }

        [Route("api/GenerateKey")]
        public string GetGenerateKey()
        {
            string key = Helpers.CreateKey(8);
            return key;
        }
    }
}