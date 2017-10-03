using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Blog.AutoMapper.Attributes;

namespace Blog.AutoMapper
{
    public class AutoMapAttribute : AutoMapperAttributeBase
    {
        public AutoMapAttribute(params Type[] targetTypes) : base(targetTypes)
        {

        }

        public override void CreateMap(IMapperConfigurationExpression configuration, Type sourceType)
        {
            foreach (var item in TargetTypes)
            {

                var tmp1 = AdditionConfig(configuration.CreateMap(sourceType, item), sourceType);
                SrcToDesMemberMap(tmp1, sourceType, item);
                DesToSrcMemberMap(tmp1, item);

                var tmp2 = AdditionConfig(configuration.CreateMap(item, sourceType), item);
                SrcToDesMemberMap(tmp2, item, sourceType);
                DesToSrcMemberMap(tmp2, sourceType);
            }
        }
    }
}
