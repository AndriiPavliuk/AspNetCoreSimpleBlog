using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Entity
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }

        bool IsTransient();
    }
    public interface IEntity:IEntity<int>
    {

    }
}
