﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Reflection
{
    public static class Types
    {
        public static bool IsPrimitive(Type type)
        {
            return type.IsPrimitive ||
                    type == typeof(String) ||
                    type == typeof(Decimal) ||
                    type == typeof(Int16) ||
                    type == typeof(Int32) ||
                    type == typeof(Int64) ||
                    type == typeof(Boolean);
        }

        public static bool IsNumeric(Type type)
        {
            return type.IsPrimitive ||
                    type == typeof(int) ||
                    type == typeof(int?) ||
                    type == typeof(decimal) ||
                    type == typeof(decimal?) ||
                    type == typeof(float) ||
                    type == typeof(float?) ||
                    type == typeof(double) ||
                    type == typeof(double?);
        }
    }
}
