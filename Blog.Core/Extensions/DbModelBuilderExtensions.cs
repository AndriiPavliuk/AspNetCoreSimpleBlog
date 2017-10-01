using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Extensions
{
    /// <summary>
	/// Extension methods for <see cref="T:System.Data.Entity.DbModelBuilder" />.
	/// </summary>
	public static class DbModelBuilderExtensions
    {
        public static void ChangeTablePrefix<TRole, TUser>(this ModelBuilder modelBuilder, string prefix, string schemaName = null)
        {
            prefix = (prefix ?? "");
            //SetTableName<IdentityRoleClaim<TKey>>(modelBuilder, prefix + "RoleClaims", schemaName);
            SetTableName<IdentityUserRole<string>>(modelBuilder, prefix + "UserRoles", schemaName);
            SetTableName<IdentityUser>(modelBuilder, prefix + "Users", schemaName);
            SetTableName<IdentityUserLogin<string>>(modelBuilder, prefix + "UserLogins", schemaName);
            SetTableName<IdentityRole>(modelBuilder, prefix + "Roles", schemaName);
            SetTableName<IdentityUserClaim<string>>(modelBuilder, prefix + "UserClaim", schemaName);
        }

        private static void SetTableName<TEntity>(ModelBuilder modelBuilder, string tableName, string schemaName) where TEntity : class
        {
            if (schemaName == null)
            {
                modelBuilder.Entity<TEntity>().ToTable(tableName);
                return;
            }
            modelBuilder.Entity<TEntity>().ToTable(tableName, schemaName);
        }
    }

}
