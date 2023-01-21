using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MannFramework.HttpResponseMessage;
using MannFramework.Interface;

namespace MannFramework.Factory
{
    public class HttpResponseFactory : IHttpResponseFactory
    {
        public GarciaHttpResponseMessage GetResponseMessage(HttpStatusCode httpStatusCode)
        {
            GarciaHttpResponseMessage response;

            switch (httpStatusCode)
            {
                case HttpStatusCode.BadRequest:
                    response = new BadRequest();
                    break;
                case HttpStatusCode.NotFound:
                    response = new NotFound();
                    break;
                case HttpStatusCode.NoContent:
                    response = new NoContent();
                    break;
                default:
                    response = new GarciaHttpResponseMessage();
                    // TODO: find a bettor one
                    break;
            }

            return response;
        }
    }
}
