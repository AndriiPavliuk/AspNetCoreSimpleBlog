using AutoMapper;
using Blog.AutoMapper.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public IMappingExpression AdditionConfig(IMappingExpression mappingExpression, Type sourceType)
        {
            //忽略默认值
            if (sourceType.IsDefined(typeof(MapIgnoreNullMemberAttribute), false))
            {
                //TODO DateTime,Bool 的默认值无法忽略
                mappingExpression.ForAllMembers(opt =>
                    opt.Condition((srcType, desType, srcMember, disMember) =>
                     srcMember != null && !srcMember.Equals(GetDefaultValue(sourceType))));
            }
            return mappingExpression;
        }
        #region 都是用于SrcType到DesType映射配置时,对成员属性的配置
        protected void SrcToDesMemberMap(IMappingExpression mappingExpression, Type srcType, Type desType)
        {
            //配置对Src->Des成员的映射,要先检查desType的属性srcType是否有

            var desPropertiesSet = desType.GetProperties().Select(o => o.Name).ToHashSet();
            foreach (var item in srcType.GetProperties())
            {
                if (!desPropertiesSet.Contains(item.Name))
                {
                    continue;
                }
                if (item.IsDefined(typeof(OnlyMapFromAttribute)))
                {
                    mappingExpression.ForMember(item.Name, opt => opt.Ignore());
                }
                if (item.IsDefined(typeof(DontMapToAttribute)))
                {
                    var arribute = item.GetCustomAttribute<DontMapToAttribute>();
                    if (arribute.TargetTypes.Contains(desType))
                    {
                        mappingExpression.ForMember(item.Name, opt => opt.Ignore());
                    }
                }
            }
        }
        protected void DesToSrcMemberMap(IMappingExpression mappingExpression, Type desType)
        {
            foreach (var item in desType.GetProperties())
            {
                if (item.IsDefined(typeof(OnlyMapToAttribute)))
                {
                    mappingExpression.ForMember(item.Name, opt => opt.Ignore());
                }
            }
        }

        #endregion

        /// <summary>
        /// 如果type的属性成员定义了特性TAttribute,那么该属性映射被忽略
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="mappingExpression"></param>
        /// <param name="type"></param>
        protected void OnlyMap<TAttribute>(IMappingExpression mappingExpression, Type type, bool isSource = true)
            where TAttribute : Attribute
        {
            //AutoMap 的ForMember意思是,对于目标对象的XX成员,该如何映射
            //假如要让sourceType的XX属性不映射,那么应该是ForMember(XX,opt=>opt.Ignore)
            //然而这里的XX是目标对象的XX
            //那么如果目标对象没有XX,其实没必要调用ForMember

            //官方说道,ForSourceMember主要用于Validate https://github.com/AutoMapper/AutoMapper/issues/1556
            //真正的要ignore映射,还是应当用ForMember
            foreach (var item in type.GetProperties())
            {
                if (item.IsDefined(typeof(TAttribute)))
                {
                    if (isSource)
                    {
                        mappingExpression.ForSourceMember(item.Name, opt => opt.Ignore()).ReverseMap();
                    }
                    else
                    {
                        mappingExpression.ForMember(item.Name, opt => opt.Ignore());
                    }
                }
            }
        }
        private object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
