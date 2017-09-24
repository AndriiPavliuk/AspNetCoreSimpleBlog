using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Entity
{
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }

        /// <summary>
        /// Checks if this entity is transient (it has not an Id).
        /// </summary>
        /// <returns>True, if this entity is transient</returns>
        public virtual bool IsTransient()
        {
            if (EqualityComparer<TPrimaryKey>.Default.Equals(this.Id, default(TPrimaryKey)))
            {
                return true;
            }
            if (typeof(TPrimaryKey) == typeof(int))
            {
                return Convert.ToInt32(this.Id) <= 0;
            }
            return typeof(TPrimaryKey) == typeof(long) && Convert.ToInt64(this.Id) <= 0L;
        }
    }
    public abstract class Entity : Entity<int>
    {

    }
}
