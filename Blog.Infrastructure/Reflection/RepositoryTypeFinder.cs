using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Blog.Reflection
{
    public class RepositoryTypeFinder : ITypeFinder
    {
        public ICollection<Type> GetTypes()
        {
            var ass = AssemblyHelper.LoadCompileAssemblies();
            return this.GetTypes(ass);
        }

        public ICollection<Type> GetTypes(ICollection<Assembly> assembly)
        {
            List<Type> types = new List<Type>();
            foreach (var item in assembly)
            {
                types.AddRange(FindRepository(item));
            }
            return types;
        }

        public IEnumerable<Type> FindRepository(Assembly assembly)
        {
            IEnumerable<Type> allTypes = assembly.GetTypes();

            //allTypes = allTypes.Where(a =>
            //{
            //    var b = a.IsAbstract == false
            //            && a.IsClass
            //            && typeof(IDomainService).IsAssignableFrom(a);
            //    return b;
            //});

            List<Type> ret = allTypes.ToList();
            return ret;
        }
    }
}
