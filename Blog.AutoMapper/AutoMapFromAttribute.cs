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
                configuration.CreateMap(item, type);
            }
        }
    }
}
