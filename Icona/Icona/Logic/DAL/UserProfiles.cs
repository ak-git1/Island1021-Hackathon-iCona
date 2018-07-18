using System;
using System.Data;
using System.Data.Common;
using Icona.Logic.Entities;
using Icona.Logic.Filters;
using Icona.Logic.Settings;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Icona.Logic.DAL
{
    /// <summary>
    /// Работа с профилями пользователей
    /// </summary>
    internal static class UserProfiles
    {
        /// <summary>
        /// Добавление новой анкеты пользователя
        /// </summary>
        /// <param name="profile">Анкета</param>
        public static int Add(UserProfile profile)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_UserProfiles_Add",
                                                                                profile.UserId,
                                                                                profile.FirstName,
                                                                                profile.LastName,
                                                                                profile.MiddleName,
                                                                                profile.GenderId);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            return Convert.ToInt32(db.ExecuteScalar(command));
        }

        /// <summary>
        /// Обновление анкеты пользователя
        /// </summary>
        /// <param name="profile">Анкета</param>
        public static void Update(UserProfile profile)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_UserProfiles_Update", profile.Id,
                                                                                    profile.FirstName,
                                                                                    profile.LastName,
                                                                                    profile.MiddleName,
                                                                                    profile.GenderId,
                                                                                    profile.Blocked);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Получение анкеты пользователя
        /// </summary>
        /// <param name="id">Идентификатор</param>        
        public static UserProfile Get(int id)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_UserProfiles_Get", id);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;

            using (IDataReader reader = db.ExecuteReader(command))
            {
                if (reader.Read())
                    return new UserProfile(reader);
            }

            return null;
        }

        /// <summary>
        /// Получение анкеты пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>        
        public static UserProfile GetByUserId(Guid userId)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_UserProfiles_GetByUserId", userId);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;

            using (IDataReader reader = db.ExecuteReader(command))
            {
                if (reader.Read())
                    return new UserProfile(reader);
            }

            return null;
        }

        /// <summary>
        /// Проверка существования пользователя по идентификатору
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>        
        public static bool Exists(Guid userId)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_UserProfiles_Exists", userId);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;

            return Convert.ToBoolean(db.ExecuteScalar(command));
        }

        /// <summary>
        /// Удалить запись по идентификатору.
        /// </summary>
        /// <param name="userProfileId">Идентификатор профиля пользователя</param>
        public static void Delete(int userProfileId)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_UserProfiles_Delete", userProfileId);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            db.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Получение списка анкет пользователей
        /// </summary>
        /// <param name="f">Фильтр</param>
        /// <returns></returns>
        public static ListEx<UserProfile> GetList(UsersRegistryFilter f)
        {
            Database db = (new DatabaseProviderFactory()).CreateDefault();
            DbCommand command = db.GetStoredProcCommand("p_UserProfiles_GetList",   f.FIO.Trim(),
                                                                                    f.Login.Trim(),
                                                                                    f.Email.Trim(),
                                                                                    f.RegistrationDateFrom,
                                                                                    f.RegistrationDateTill,
                                                                                    f.LastEnterDateFrom,
                                                                                    f.LastEnterDateTill,
                                                                                    f.IsBlocked,                                                                                    
                                                                                    f.Paging.CurrentPage,
                                                                                    f.Paging.RecordsPerPage);
            command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
            using (IDataReader r = db.ExecuteReader(command))
            {
                return new ListEx<UserProfile>(r, x => new UserProfile(x));
            }
        }
    }
}
