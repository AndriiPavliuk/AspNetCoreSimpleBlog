using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Blog.Reflection
{
    public interface ITypeFinder
    {
        ICollection<Type> FindAll();
        ICollection<Type> Find(Func<Type, bool> predicate);
    }
}
