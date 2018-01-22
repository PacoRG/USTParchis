using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    sealed public class DataEntityAttribute : Attribute
    {
    }
}
