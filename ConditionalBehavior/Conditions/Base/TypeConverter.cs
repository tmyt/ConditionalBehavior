using System;
using System.Reflection;
using Microsoft.Xaml.Interactions.Core;

namespace Gears.ConditionalBehavior.Conditions.Base
{
    public class TypeConverter
    {
        public static object ChangeType(Type targetType, object from)
        {
            var typeInfo = targetType.GetTypeInfo();
            if (from == null)
                return typeInfo.IsValueType ? Activator.CreateInstance(targetType) : null;
            if (typeInfo.IsAssignableFrom(from.GetType().GetTypeInfo()))
                return from;
            // Convert
            var str = from.ToString();
            return typeInfo.IsEnum ? Enum.Parse(targetType, str) : ConvertType(str, targetType.FullName);
        }

        private static object ConvertType(string from, string type)
        {
            try
            {
                // Get Internal method
                var typeName = typeof(EventTriggerBehavior).AssemblyQualifiedName
                    .Replace(".EventTriggerBehavior,", ".TypeConverterHelper,");
                var typeConverterHelperType = Type.GetType(typeName);
                var typeConverterHelperConvert = typeConverterHelperType
                    .GetRuntimeMethod("Convert", new[] { typeof(string), typeof(string) });
                return typeConverterHelperConvert.Invoke(null, new object[] { from, type });
            }
            catch
            {
                return null;
            }
        }
    }
}
