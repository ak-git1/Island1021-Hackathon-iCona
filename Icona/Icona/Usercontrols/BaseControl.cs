using System.Web.UI;
using Icona.Logic.Extensions;

namespace Icona.Usercontrols
{
    /// <summary>
    /// Базовый контрол
    /// </summary>
    public abstract class BaseControl : UserControl
    {
        #region Свойства

        /// <summary>
        /// Идентификатор текущего пользователя
        /// </summary>
        public int CurrentUserId => Page.GetCurrentUserProfileId();

        #endregion

        #region Методы

        /// <summary>
        /// Генерация названия ключа
        /// </summary>
        /// <param name="name">Название</param>
        /// <returns></returns>
        protected string KeyOf(string name)
        {
            return $"{ClientID}{ClientIDSeparator}{name}";
        }

        /// <summary>
        /// Генерация названия ключа сессии
        /// </summary>
        /// <param name="name">Название</param>
        /// <returns></returns>
        protected string SessionKeyOf(string name)
        {
            return $"{Request.Path}{IdSeparator}{KeyOf(name)}";
        }

        #endregion
    }
}
