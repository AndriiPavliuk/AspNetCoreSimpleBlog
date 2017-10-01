using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Blog.AutoMapper
{
    public class AutoMapAttribute:AutoMapperAttributeBase
    {
        public AutoMapAttribute(params Type[] targetTypes):base(targetTypes)
        {

        }

        public override void CreateMap(IMapperConfigurationExpression configuration, Type type)
        {
            foreach (var item in TargetTypes)
            {
                AdditionConfig(configuration.CreateMap(type, item, MemberList.Destination), type);
                AdditionConfig(configuration.CreateMap(item, type), item);
            }
        }
    }
}
