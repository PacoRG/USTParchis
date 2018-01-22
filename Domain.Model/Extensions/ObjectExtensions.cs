using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Domain.Model.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsDataEntity(this object obj)
        {
            return obj != null && obj.GetType().GetCustomAttribute(typeof(DataEntityAttribute)) != null;
        }
    }
}
