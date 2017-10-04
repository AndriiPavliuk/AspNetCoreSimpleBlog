using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Blog.AutoMapper
{
    public class AutoMapToAttribute : AutoMapperAttributeBase
    {
        public AutoMapToAttribute(params Type[] types):base(types)
        {

        }
        public override void CreateMap(IMapperConfigurationExpression configuration, Type type)
        {
            foreach (var item in TargetTypes)
            {
                AdditionConfig(configuration.CreateMap(type, item),type);
            }
        }
    }
}
