using DomainServices.Interfaces.Infraestructure;
using System;
using System.Reflection;

namespace Infraestructure.Reflection
{
    public class Mapper : IMapper
    {
        public D Map<D>(object from) where D : new()
        {
            if (from == null) return default(D);

            var to = new D();
            var toProperties = to.GetType().GetProperties();

            foreach (var prop in from.GetType().GetProperties())
            {
                if (IsBaseTypeProperty(prop) && HasProperty(toProperties, prop))
                {
                    var toProperty = to.GetType().GetProperty(prop.Name);
                    var value = ResolveValue(prop, toProperty, from);
                    if (toProperty != null) toProperty.SetValue(to, value, null);
                }
            }

            return to;
        }

        private object ResolveValue(
            PropertyInfo propertyFrom,
            PropertyInfo propertyTo,
            object from)
        {
            var value = propertyFrom.GetValue(from);
            return value;
        }

        private bool IsBaseTypeProperty(PropertyInfo property)
        {
            if (property.PropertyType == typeof(string) ||
               property.PropertyType == typeof(float) ||
               property.PropertyType == typeof(float?) ||
               property.PropertyType == typeof(decimal) ||
               property.PropertyType == typeof(decimal?) ||
               property.PropertyType == typeof(int) ||
               property.PropertyType == typeof(int?) ||
               property.PropertyType == typeof(bool) ||
               property.PropertyType == typeof(bool?) ||
               property.PropertyType == typeof(DateTime) ||
               property.PropertyType == typeof(DateTime?))
                return true;

            return false;
        }

        private bool IsDateTypeProperty(PropertyInfo property)
        {
            if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                return true;

            return false;

        }

        private bool HasProperty(
            PropertyInfo[] toProperties,
            PropertyInfo property)
        {
            foreach (var prop in toProperties)
            {
                if (prop.Name == property.Name &&
                    (prop.PropertyType == property.PropertyType ||
                     prop.PropertyType == typeof(int?) && property.PropertyType == typeof(int) ||
                     prop.PropertyType == typeof(DateTime) && property.PropertyType == typeof(string) ||
                     prop.PropertyType == typeof(DateTime?) && property.PropertyType == typeof(string) ||
                     prop.PropertyType == typeof(string) && property.PropertyType == typeof(DateTime) ||
                     prop.PropertyType == typeof(string) && property.PropertyType == typeof(DateTime?)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}