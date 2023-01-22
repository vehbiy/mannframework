using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace MannFramework
{
    public class MannFrameworkHttpResponseMessage
    {
        public bool Success { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object Data { get; set; }

        public MannFrameworkHttpResponseMessage()
        {
            this.ErrorMessages = new List<string>();
        }

        public MannFrameworkHttpResponseMessage(HttpStatusCode httpStatusCode, bool? success = false) : this()
        {
            this.HttpStatusCode = httpStatusCode;

            if (success.HasValue)
            {
                this.Success = success.Value;
            }
        }

        public MannFrameworkHttpResponseMessage(HttpStatusCode httpStatusCode, bool? success, string errorMessage = "") : this(httpStatusCode, success)
        {
            if (this.Success == false)
            {
                if (string.IsNullOrEmpty(errorMessage))
                {
                    errorMessage = HttpStatusCodeHelper.GetStatusCodeMessage(httpStatusCode);
                }

                this.ErrorMessages.Add(errorMessage);
            }
        }

        public MannFrameworkHttpResponseMessage(HttpStatusCode httpStatusCode, Exception exception) : this(httpStatusCode, false)
        {
            this.ErrorMessages.Add(exception.Message);
        }

        public MannFrameworkHttpResponseMessage(ModelStateDictionary modelState) : this()
        {
            this.HttpStatusCode = HttpStatusCode.BadRequest;
            this.Success = false;

            foreach (ModelState value in modelState.Values)
            {
                foreach (ModelError error in value.Errors)
                {
                    this.ErrorMessages.Add(error.ErrorMessage);
                }
            }
        }

        public MannFrameworkHttpResponseMessage(OperationResult result) : this()
        {
            this.Success = result.Success;

            if (!result.Success)
            {
                if (result.ValidationResults.Count != 0)
                {
                    this.HttpStatusCode = HttpStatusCode.BadRequest;
                    this.ErrorMessages.AddRange(result.ValidationResults.SelectMany(x => x.Messages));
                }
            }
            else
            {
                this.HttpStatusCode = HttpStatusCode.OK;
            }
        }
    }

    public class MannFrameworkHttpResponseMessage<T> : MannFrameworkHttpResponseMessage
    {
        private T data;
        new public T Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
                base.Data = value;
            }
        }

        public MannFrameworkHttpResponseMessage() : base()
        {

        }

        public MannFrameworkHttpResponseMessage(T data)
        {
            if (data != null)
            {
                this.HttpStatusCode = HttpStatusCode.OK;
                this.Success = true;
                this.Data = data;
            }
            else
            {
                this.HttpStatusCode = HttpStatusCode.NotFound;
                this.Success = false;
            }
        }

        public MannFrameworkHttpResponseMessage(HttpStatusCode httpStatusCode, bool? success = false) : base(httpStatusCode, success)
        {

        }

        public MannFrameworkHttpResponseMessage(OperationResult result) : base(result)
        {
        }
    }

    public class HttpStatusCodeHelper
    {
        internal static string GetStatusCodeMessage(HttpStatusCode httpStatusCode)
        {
            string message = "";

            switch (httpStatusCode)
            {
                case HttpStatusCode.BadRequest:
                    message = "Eksik veri girildi";
                    break;
                case HttpStatusCode.NotFound:
                    message = "Kayıt bulunamadı";
                    break;
                case HttpStatusCode.NoContent:
                    message = "Veri bulunamadı.";
                    break;
                default:
                    break;
            }

            return message;
        }
    }
}
