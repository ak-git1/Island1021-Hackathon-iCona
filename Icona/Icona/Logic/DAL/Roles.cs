using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Icona.Logic.Entities.Security;
using Icona.Logic.Settings;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Icona.Logic.DAL
{
    /// <summary>
    /// Работа с провайдером ролей.
    /// </summary>
    internal static class Roles
    {
        /// <summary>
        /// Получение списка ролей.
        /// </summary>
        public static List<ExRole> GetList()
        {
            List<ExRole> result = new List<ExRole>();
            try
            {
                Database db = (new DatabaseProviderFactory()).CreateDefault();
                DbCommand command = db.GetStoredProcCommand("aspnet_Roles_GetAllRoles", System.Web.Security.Roles.ApplicationName);
                command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
                using (IDataReader reader = db.ExecuteReader(command))
                {
                    while (reader.Read())
                        result.Add(new ExRole(new Guid(reader["RoleId"].ToString()),
                            reader["RoleName"].ToString(),
                            reader["Description"].ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось получить список ролей: {ex.Message}");
            }

            return result;
        }



        /// <summary>
        /// Получение роли
        /// </summary>
        /// <param name="id">Идентификатор роли</param>
        /// <returns></returns>
        public static ExRole Get(Guid id)
        {
            ExRole result = new ExRole();
            try
            {
                Database db = (new DatabaseProviderFactory()).CreateDefault();
                DbCommand command = db.GetStoredProcCommand("aspnet_Roles_GetAllRoles", System.Web.Security.Roles.ApplicationName);
                command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
                using (IDataReader reader = db.ExecuteReader(command))
                {
                    while (reader.Read())
                        if (new Guid(reader["RoleId"].ToString()).Equals(id))
                        {
                            result.RoleId = id;
                            result.RoleName = reader["RoleName"].ToString();
                            result.Description = reader["Description"].ToString();
                        }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось получить роль: {ex.Message}");
            }

            return result;
        }
    }
}