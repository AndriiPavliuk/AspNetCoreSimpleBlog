using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Repository
{
    /// <summary>
    /// This exception is thrown if an entity excepted to be found but not found.
    /// </summary>
    [Serializable]
    public class EntityNotFoundException
    {
        /// <summary>
        /// Type of the entity.
        /// </summary>
        public Type EntityType
        {
            get;
            set;
        }

        /// <summary>
        /// Id of the Entity.
        /// </summary>
        public object Id
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new <see cref="T:Abp.Domain.Entities.EntityNotFoundException" /> object.
        /// </summary>
        public EntityNotFoundException()
        {
        }

        /// <summary>
        /// Creates a new <see cref="T:Abp.Domain.Entities.EntityNotFoundException" /> object.
        /// </summary>
        public EntityNotFoundException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {
        }

        /// <summary>
        /// Creates a new <see cref="T:Abp.Domain.Entities.EntityNotFoundException" /> object.
        /// </summary>
        public EntityNotFoundException(Type entityType, object id) : this(entityType, id, null)
        {
        }

        /// <summary>
        /// Creates a new <see cref="T:Abp.Domain.Entities.EntityNotFoundException" /> object.
        /// </summary>
        public EntityNotFoundException(Type entityType, object id, Exception innerException) : base(string.Format("There is no such an entity. Entity type: {0}, id: {1}", entityType.FullName, id), innerException)
        {
            this.EntityType = entityType;
            this.Id = id;
        }

        /// <summary>
        /// Creates a new <see cref="T:Abp.Domain.Entities.EntityNotFoundException" /> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public EntityNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new <see cref="T:Abp.Domain.Entities.EntityNotFoundException" /> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
