using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public class OperationResult
    {
        public virtual bool Success { get; set; }
        public virtual ValidationResults ValidationResults { get; set; }
        /// <summary>
        /// Returns localized text for the first validation result if exists. Returns empty string otherwise.
        /// </summary>
        public string LocalizedValidationMessage
        {
            get
            {
                return GarciaLocalizationManager.Localize(this.ValidationMessage);
            }
        }

        /// <summary>
        /// Returns text for the first validation result if exists. Returns empty string otherwise.
        /// </summary>
        public string ValidationMessage
        {
            get
            {
                return this.ValidationResults?[0].Messages?[0];
            }
        }

        protected OperationResult(bool Success, string ValidationError)
        {
            this.ValidationResults = new ValidationResults();
            this.Success = Success;

            if (!string.IsNullOrEmpty(ValidationError))
            {
                this.ValidationResults.Add(new ValidationResult(ValidationError));
            }
        }

        protected OperationResult(bool Success, ValidationResults validationResults)
        {
            this.ValidationResults = validationResults;
            this.Success = Success;
        }

        /// <summary>
        /// Success: false
        /// </summary>
        /// <param name="ValidationError"></param>
        public OperationResult(string ValidationError)
            : this(false, ValidationError)
        {
        }

        /// <summary>
        /// Success: true
        /// </summary>
        /// <param name="ValidationError"></param>
        public OperationResult()
            : this(true, string.Empty)
        {
        }

        public ValidationResult AddValidationResult(string Message)
        {
            ValidationResult validationResult = new ValidationResult(Message);
            this.ValidationResults.Add(validationResult);
            return validationResult;
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Item { get; set; }

        protected OperationResult(T Item, bool Success, string ValidationError)
            : base(Success, ValidationError)
        {
            this.Item = Item;
        }

        protected OperationResult(T Item, bool Success, ValidationResults validationResults)
           : base(Success, validationResults)
        {
            this.Item = Item;
        }

        public OperationResult(T Item, string ValidationError)
            : this(Item, Item != null, ValidationError)
        {
        }

        public OperationResult(T Item)
            : this(Item, ValidationError: null)
        {
        }

        /// <summary>
        /// Success: false
        /// </summary>
        public OperationResult()
            : this(default(T), false, ValidationError: null)
        {
        }

        public OperationResult(OperationResult result, T item)
            : this(item, result.Success, result.ValidationResults)
        {

        }
    }
}
