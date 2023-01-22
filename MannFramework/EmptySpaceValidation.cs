using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class WhitespaceValidation : RegularExpressionAttribute
    {
        public WhitespaceValidation() : base(@"^\S*$")
        {
            ErrorMessage = MannFrameworkLocalizationManager.Localize("Whitespacenotallowed");
        }
    }
}
