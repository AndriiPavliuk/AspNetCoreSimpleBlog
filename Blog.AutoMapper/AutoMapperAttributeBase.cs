using AutoMapper;
using Blog.AutoMapper.Attributes;
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

        public abstract void CreateMap(IMapperConfigurationExpression configuration, Type type);

        public void AdditionConfig(IMappingExpression mappingExpression, Type sourceType)
        {
            //忽略默认值
            if (sourceType.IsDefined(typeof(MapIgnoreNullMemberAttribute), false))
            {
                //TODO DateTime,Bool 的默认值无法忽略
                mappingExpression.ForAllMembers(opt =>
                    opt.Condition((srcType, desType, srcMember, disMember) =>
                     srcMember!=null&&!srcMember.Equals(GetDefaultValue(sourceType))));
            }

        }
        private object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
