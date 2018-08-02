using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Ak.Framework.Core.Extensions;
using Icona.Logic.Entities.Security;
using Icona.Logic.Settings;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Icona.Logic.DAL
{
    /// <summary>
    /// Класс для получения данных о пользователях и ролях из БД
    /// </summary>
    internal static class Security
    {
        /// <summary>
        /// Получает массив названий ролей пользователя
        /// </summary>
        /// <param name="userName">Логин пользователя</param>
        /// <returns>Массив названий ролей пользователя</returns>
        public static string[] GetRolesForUser(string userName)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_Security_GetRolesForUser", userName);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            using (IDataReader r = db.ExecuteReader(command))
            {
                List<string> roleNames = new List<string>();
                while (r.Read())
                    roleNames.Add(r["RoleName"].ToStr());
                return roleNames.ToArray();
            }
        }

        /// <summary>
        /// Удаление роли
        /// </summary>
        /// <param name="roleName">Название роли</param>
        /// <param name="throwOnPopulatedRole">Вызвать исключение, если роль принадлежит хотя бы одному пользователю</param>
        /// <returns>Удаление прошло успешно</returns>
        public static bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получить массив логинов пользователей имеющих роль
        /// </summary>
        /// <param name="roleName">Название роли</param>
        /// <returns>Массив логинов пользователей</returns>
        public static string[] GetUsersInRole(string roleName)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_Security_GetUsersInRole", roleName);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            using (IDataReader r = db.ExecuteReader(command))
            {
                List<string> userNames = new List<string>();
                while (r.Read())
                    userNames.Add(r["UserName"].ToStr());
                return userNames.ToArray();
            }
        }

        /// <summary>
        /// Узнать имеет ли пользователь роль
        /// </summary>
        /// <param name="userName">Название роли</param>
        /// <param name="roleName">Логин пользователя</param>
        /// <returns>Имеет ли пользователь роль</returns>
        public static bool IsUserInRole(string userName, string roleName)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_Security_IsUserInRole", userName, roleName);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            return db.ExecuteScalar(command).ToBoolean();            
        }

        /// <summary>
        /// Получить непосредственно связанные роли пользователя
        /// </summary>
        /// <param name="userName">Логин</param>
        /// <returns>Список ролей</returns>
        public static List<ExRole> GetOwnRolesForUser(string userName)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_Security_GetOwnRolesForUser", userName);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            using (IDataReader r = db.ExecuteReader(command))
            {
                List<ExRole> roles = new List<ExRole>();
                while (r.Read())
                    roles.Add(new ExRole(new Guid(r["RoleId"].ToStr()), r["RoleName"].ToStr(), r["Description"].ToStr()));
                return roles;
            }
        }
    }
}