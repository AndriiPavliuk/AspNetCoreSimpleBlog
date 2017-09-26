using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Exceptions
{

    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string name, string id) : base($"There is no such an entity." +
                    $" type: {name}, id: {id}")
        {

        }
    }
}
