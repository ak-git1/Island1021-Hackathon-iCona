using System;
using System.Web.UI;
using Icona.Logic.Extensions;

namespace Icona.Logic.UI
{
    /// <summary>
    /// Базовая страница
    /// </summary>
    public abstract class BasePage : Page
    {
        #region Свойства

        /// <summary>
        /// Идентификатор текущего пользователя
        /// </summary>
        protected int CurrentUserId
        {
            get
            {
                if (ViewState["82A7244F-419E-4FEF-AEAA-6928BC9479BE"] == null)
                    ViewState["82A7244F-419E-4FEF-AEAA-6928BC9479BE"] = this.GetCurrentUserProfileId();
                return Convert.ToInt32(ViewState["82A7244F-419E-4FEF-AEAA-6928BC9479BE"]);
            }
        }

        /// <summary>
        /// Путь к странице, на которую нужно вернуться с текущей страницы
        /// (при установке значения сразу проводится UrlDecode).
        /// Стандартный ценарий использования - url для ссылки "Вернуться на предыдущую страницу".
        /// </summary>
        public string ReturnUrl
        {
            get { return ViewState["9112DE94-29D8-496D-A757-76E2CC757E1D"].ToString(); }
            set { ViewState["9112DE94-29D8-496D-A757-76E2CC757E1D"] = Server.UrlDecode(value); }
        }

        /// <summary>
        /// Строка параметров url
        /// </summary>
        public string UrlParameters
        {
            get
            {
                if (Page.Request.UrlReferrer == null)
                    return string.Empty;
                return Page.Request.QueryString.ToString();
            }
        }

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
        protected string SessionKeyOf(string name)
        {
            return $"{Request.Path}{IdSeparator}{KeyOf(name)}";
        }

        #endregion
    }
}