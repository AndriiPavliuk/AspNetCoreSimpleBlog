using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.AutoMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DontMapToAttribute:Attribute
    {
        public DontMapToAttribute(params Type[] targetTypes)
        {
            this.TargetTypes = targetTypes;
        }

        public Type[] TargetTypes { get; }
    }
}
