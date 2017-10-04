using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Blog.AutoMapper
{
    public class AutoMapFromAttribute : AutoMapperAttributeBase
    {
        public override void CreateMap(IMapperConfigurationExpression configuration, Type type)
        {
            foreach (var item in TargetTypes)
            {
                AdditionConfig(configuration.CreateMap(item, type),item);
            }
        }
    }
}
