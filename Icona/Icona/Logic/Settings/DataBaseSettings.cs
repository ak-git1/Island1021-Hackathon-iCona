using System.Configuration;
using Ak.Framework.Core.Extensions;

namespace Icona.Logic.Settings
{
    /// <summary>
    /// Настройки базы данных
    /// </summary>
    public static class DataBaseSettings
    {
        #region Переменные и константы

        /// <summary>
        /// Время исполнения SQL запроса (с)
        /// </summary>
        private static int? _sqlCommandTimeout;

        #endregion

        #region Свойства

        /// <summary>
        /// Время исполнения SQL запроса (с)
        /// </summary>
        public static int SqlCommandTimeout
        {
            get
            {
                if (_sqlCommandTimeout == null)
                    _sqlCommandTimeout = DAL.Settings.Get("SQLCommandTimeout").ToInt32(null);

                return _sqlCommandTimeout.Value;
            }
            set
            {
                _sqlCommandTimeout = value;
                DAL.Settings.Set("SQLCommandTimeout", _sqlCommandTimeout.ToString());
            }
        }

        /// <summary>
        /// Строка соединения с БД.
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["IconaDb"].ConnectionString;
            }
        }

        #endregion
    }

}
