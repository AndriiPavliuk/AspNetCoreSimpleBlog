using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Reflection
{
    public static class ReflectionHelper
    {
        public static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }
            Type[] interfaces = givenType.GetInterfaces();
            for (int i = 0; i < interfaces.Length; i++)
            {
                Type interfaceType = interfaces[i];
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }
            return !(givenType.BaseType == null) && ReflectionHelper.IsAssignableToGenericType(givenType.BaseType, genericType);
        }
    }
}
