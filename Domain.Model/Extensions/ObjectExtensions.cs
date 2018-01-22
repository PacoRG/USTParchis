using System;
using System.Collections;
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

        public static bool IsCollection(this object obj)
        {
            if (obj == null) return false;
            return (obj is IList &&
                   obj.GetType().IsGenericType &&
                   obj.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>)))
                   ||
                   (obj is ICollection &&
                   obj.GetType().IsGenericType &&
                   obj.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(ICollection<>)));
        }      
    }
}
