using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Services.Tests.Utils
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class CustomAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string && value.ToString() == "Paco")
                return true;

            return false; ;
        }
    }
}
