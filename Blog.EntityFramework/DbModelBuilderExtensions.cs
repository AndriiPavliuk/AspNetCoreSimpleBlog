using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.EntityFramework
{
    /// <summary>
	/// Extension methods for <see cref="T:System.Data.Entity.DbModelBuilder" />.
	/// </summary>
	public static class DbModelBuilderExtensions
    {
        /// <summary>
        /// Changes prefix for ABP tables (which is "Abp" by default).
        /// Can be null/empty string to clear the prefix.
        /// </summary>
        /// <typeparam name="TTenant">The type of the tenant entity.</typeparam>
        /// <typeparam name="TRole">The type of the role entity.</typeparam>
        /// <typeparam name="TUser">The type of the user entity.</typeparam>
        /// <param name="modelBuilder">Model builder.</param>
        /// <param name="prefix">Table prefix, or null to clear prefix.</param>
        public static void ChangeAbpTablePrefix<TTenant, TRole, TUser>(this DbModelBuilder modelBuilder, string prefix, string schemaName = null) where TTenant : AbpTenant<TUser> where TRole : AbpRole<TUser> where TUser : AbpUser<TUser>
        {
            prefix = (prefix ?? "");
            DbModelBuilderExtensions.SetTableName<AuditLog>(modelBuilder, prefix + "AuditLogs", schemaName);
            DbModelBuilderExtensions.SetTableName<BackgroundJobInfo>(modelBuilder, prefix + "BackgroundJobs", schemaName);
            DbModelBuilderExtensions.SetTableName<Edition>(modelBuilder, prefix + "Editions", schemaName);
            DbModelBuilderExtensions.SetTableName<FeatureSetting>(modelBuilder, prefix + "Features", schemaName);
            DbModelBuilderExtensions.SetTableName<TenantFeatureSetting>(modelBuilder, prefix + "Features", schemaName);
            DbModelBuilderExtensions.SetTableName<EditionFeatureSetting>(modelBuilder, prefix + "Features", schemaName);
            DbModelBuilderExtensions.SetTableName<ApplicationLanguage>(modelBuilder, prefix + "Languages", schemaName);
            DbModelBuilderExtensions.SetTableName<ApplicationLanguageText>(modelBuilder, prefix + "LanguageTexts", schemaName);
            DbModelBuilderExtensions.SetTableName<NotificationInfo>(modelBuilder, prefix + "Notifications", schemaName);
            DbModelBuilderExtensions.SetTableName<NotificationSubscriptionInfo>(modelBuilder, prefix + "NotificationSubscriptions", schemaName);
            DbModelBuilderExtensions.SetTableName<OrganizationUnit>(modelBuilder, prefix + "OrganizationUnits", schemaName);
            DbModelBuilderExtensions.SetTableName<PermissionSetting>(modelBuilder, prefix + "Permissions", schemaName);
            DbModelBuilderExtensions.SetTableName<RolePermissionSetting>(modelBuilder, prefix + "Permissions", schemaName);
            DbModelBuilderExtensions.SetTableName<UserPermissionSetting>(modelBuilder, prefix + "Permissions", schemaName);
            DbModelBuilderExtensions.SetTableName<TRole>(modelBuilder, prefix + "Roles", schemaName);
            DbModelBuilderExtensions.SetTableName<Setting>(modelBuilder, prefix + "Settings", schemaName);
            DbModelBuilderExtensions.SetTableName<TTenant>(modelBuilder, prefix + "Tenants", schemaName);
            DbModelBuilderExtensions.SetTableName<UserLogin>(modelBuilder, prefix + "UserLogins", schemaName);
            DbModelBuilderExtensions.SetTableName<UserLoginAttempt>(modelBuilder, prefix + "UserLoginAttempts", schemaName);
            DbModelBuilderExtensions.SetTableName<TenantNotificationInfo>(modelBuilder, prefix + "TenantNotifications", schemaName);
            DbModelBuilderExtensions.SetTableName<UserNotificationInfo>(modelBuilder, prefix + "UserNotifications", schemaName);
            DbModelBuilderExtensions.SetTableName<UserOrganizationUnit>(modelBuilder, prefix + "UserOrganizationUnits", schemaName);
            DbModelBuilderExtensions.SetTableName<UserRole>(modelBuilder, prefix + "UserRoles", schemaName);
            DbModelBuilderExtensions.SetTableName<TUser>(modelBuilder, prefix + "Users", schemaName);
            DbModelBuilderExtensions.SetTableName<UserAccount>(modelBuilder, prefix + "UserAccounts", schemaName);
        }

        private static void SetTableName<TEntity>(DbModelBuilder modelBuilder, string tableName, string schemaName) where TEntity : class
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
