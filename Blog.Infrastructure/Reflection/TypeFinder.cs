using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Reflection
{
    public class TypeFinder : ITypeFinder
    {
        public ICollection<Type> Find(Func<Type, bool> predicate)
        {
            return this.FindAll().Where(predicate).ToList();
        }

        public ICollection<Type> FindAll()
        {
            var assembly = AssemblyHelper.LoadCompileAssemblies();
            var types = new List<Type>();
            foreach (var item in assembly)
            {
                types.AddRange(item.GetTypes());
            }
            return types;
        }
    }
}
