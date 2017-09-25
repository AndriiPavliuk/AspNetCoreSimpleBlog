using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.AutoMapper
{

    public abstract class AutoMapperAttributeBase : Attribute
    {
        /// <summary>
        /// 通过构造函数指定当前类与哪个类映射
        /// </summary>
        /// <param name="targetTypes"></param>
        protected AutoMapperAttributeBase(params Type[] targetTypes)
        {
            this.TargetTypes = targetTypes;
        }

        protected Type[] TargetTypes { get; private set; }

        public abstract void CreateMap(IMapperConfigurationExpression configuration,Type type);

    }
}
