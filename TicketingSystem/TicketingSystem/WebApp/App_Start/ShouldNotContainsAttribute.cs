using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApp.App_Start
{
    public class ShouldNotContainsAttribute : ValidationAttribute
    {
        private readonly string forbiddenContent;

        public ShouldNotContainsAttribute(string value)
        {
            this.forbiddenContent = value;
        }

        public override bool IsValid(object value)
        {
            string valueAsString = value as string;
            if (valueAsString == null)
            {
                return false;
            }

            if (valueAsString.Contains(forbiddenContent))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
