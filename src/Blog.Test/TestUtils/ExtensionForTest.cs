using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Test
{
    public static class ExtensionForTest
    {
        /// <summary>
        /// 检查指定属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToInspect"></param>
        /// <param name="getters">要检查的属性</param>
        public static void CheckPropertiesAreNotNullOrDefault<T>(
          this T objectToInspect,
          params Func<T, object>[] getters)
        {
            foreach (var item in getters)
            {
                var propertyValue = item(objectToInspect);

                propertyValue.ShouldNotBe(GetDefaultValue(item(objectToInspect)));
            }
        }
    
        /// <summary>
        /// 检查所有属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToInspect"></param>
        /// <param name="excludePropertys">要排除的属性</param>
        public static void CheckAllPropertiesAreNotNullOrDefault<T>(this T objectToInspect, params string[] excludePropertys)
        {
            var props = objectToInspect.GetType().GetProperties();
            foreach (var item in props)
            {
                if (excludePropertys!=null&&excludePropertys.Contains(item.Name))
                {
                    continue;
                }
                var propertyValue = item.GetValue(objectToInspect, null);
                propertyValue.ShouldNotBe(GetDefaultValue(propertyValue),item.Name);
                if (item.PropertyType == typeof(string))
                {
                    ((string)propertyValue).ShouldNotBeNullOrWhiteSpace(item.Name);
                }
            }
        }

        public static object GetDefaultValue<T>(this T obj)
        {
            if (obj == null)
            {
                return null;
            }
            var type = obj.GetType();
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
