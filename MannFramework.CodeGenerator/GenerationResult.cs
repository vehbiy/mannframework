using MannFramework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.CodeGenerator
{
    public class GenerationResult : OperationResult
    {
        public string Message { get; private set; }
        public string Code { get; private set; }
        public OperationResultType Status { get; private set; }
        public DateTime Timestamp { get; private set; }
        public Item Item { get; private set; }
        public Generator Generator { get; private set; }
        public List<string> Folders { get; set; }

        public string Summary
        {
            get
            {
                return new GarciaStringBuilder(this.Timestamp.ToString(), " ", this.Status.ToString(), " (", this.Message, ")", " ", this.Item?.Name).ToString();
            }
        }

        /// <summary>
        /// Status: success
        /// </summary>
        public GenerationResult(string code, Item item, Generator generator, List<string> folders) : this(code, "CodeGenerated", OperationResultType.Success, item, generator, folders)
        {

        }

        public GenerationResult(string code, string message, OperationResultType status, Item item, Generator generator, List<string> folders)
        {
            this.Code = code;
            this.Message = message;
            this.Status = status;
            this.Timestamp = DateTime.Now;
            this.Item = item;
            this.Generator = generator;
            this.Folders = folders;
        }

        public override string ToString()
        {
            return new GarciaStringBuilder(this.Timestamp.ToString(), " ", this.Status.ToString(), " (", this.Message, ")", " ", this.Item?.Name, " ", this.Generator?.GetType().Name, string.IsNullOrEmpty(this.Code) ? "" : ("\n" + this.Code)).ToString();
        }
    }
}
