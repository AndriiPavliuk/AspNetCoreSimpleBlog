using AutoMapper;
using Blog.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Blog.AutoMapper
{
    public static class BlogAutoMapper
    {
        public static void Initialization()
        {
            var types = FindNeedMapType(AssemblyHelper.LoadCompileAssemblies());
            Mapper.Initialize(config =>
            {
                foreach (var item in types)
                {
                    item.GetCustomAttribute<AutoMapperAttributeBase>().CreateMap(config, item);
                }
            });
        }

        public static List<Type> FindNeedMapType(List<Assembly> assembly)
        {
            ITypeFinder finder = new TypeFinder();
            List<Type> markedMapTargetTypes = finder.Find(
                 o => o.IsClass
              && o.IsPublic
              && (o.IsDefined(typeof(AutoMapToAttribute))
                    | o.IsDefined(typeof(AutoMapFromAttribute))
                    | o.IsDefined(typeof(AutoMapAttribute)))).ToList();
            return markedMapTargetTypes;
        }
    }
}
