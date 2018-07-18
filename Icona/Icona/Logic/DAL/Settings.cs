using System;
using System.Data.Common;
using Icona.Logic.Settings;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Icona.Logic.DAL
{
    /// <summary>
    /// Работа с настройками
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Получение значения параметра настроек
        /// </summary>
        /// <param name="name">Название поля</param>
        /// <returns></returns>
        public static string Get(string name)
        {
            try
            {
                Database db = (new DatabaseProviderFactory()).CreateDefault();
                object result = db.ExecuteScalar("p_Settings_Get", name);
                return (result != null && result != DBNull.Value) ? result.ToString() : String.Empty;
            }
            catch (Exception exc)
            {
                throw new Exception($"Не удалось получить настройки: {exc.Message}");
            }
        }

        /// <summary>
        /// Сохранение параметра настроек
        /// </summary>
        /// <param name="name">Название поля</param>
        /// <param name="value">Значение</param>
        public static void Set(string name, string value)
        {
            try
            {
                Database db = (new DatabaseProviderFactory()).CreateDefault();
                DbCommand command = db.GetStoredProcCommand("p_Settings_Set", name, value);
                command.CommandTimeout = DataBaseSettings.SqlCommandTimeout;
                db.ExecuteNonQuery(command);
            }
            catch (Exception exc)
            {
                throw new Exception($"Не удалось сохранить настройки: {exc.Message}");
            }
        }
    }

}