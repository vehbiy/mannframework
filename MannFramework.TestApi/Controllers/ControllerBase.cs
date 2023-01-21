using Garcia.Tests;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Garcia.TestApi.Controllers
{
    public class ControllerBase : ApiController
    {
        private GarciaApiController controller = new GarciaApiController();

        //public virtual dynamic Get(int Id)
        //{
        //    Person person = EntityManager.Instance.GetItem<Person>(Id);
        //    dynamic model = this.controller.GenerateModel<int>(person);
        //    return model;
        //}

        public virtual dynamic Post([FromBody]dynamic Model)
        {
            return Model;
        }

        //// GET api/<controller>
        //public virtual IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public virtual string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public virtual void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public virtual void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public virtual void Delete(int id)
        //{
        //}
    }
}