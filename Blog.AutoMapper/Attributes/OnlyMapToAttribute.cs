using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.AutoMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class OnlyMapToAttribute:Attribute
    {
    }
}
