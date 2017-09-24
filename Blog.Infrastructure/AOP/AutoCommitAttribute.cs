using AspectCore.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.AOP
{
    public class AutoCommitAttribute: InterceptorAttribute
    {
        public AutoCommitAttribute()
        {

        }
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            return base.Invoke(context, next);
        }
    }
}
