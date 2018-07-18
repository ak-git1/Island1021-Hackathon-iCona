using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using Elar.Framework.Core.Extensions;
using Icona.Logic.Entities;
using Icona.Usercontrols;

namespace Icona.Logic.Extensions
{
    /// <summary>
    /// Расширения для страницы ASP.NET
    /// </summary>
    public static class PageExtensions
    {
        #region Публичные методы

        /// <summary>
        /// Получение профиля текущего пользователя
        /// </summary>
        public static UserProfile GetCurrentUserProfile(this Page page)
        {
            return Membership.GetUser(page.User.Identity.Name).GetUserProfile();
        }

        /// <summary>
        /// Получение идентификатора профиля текущего пользователя
        /// </summary>
        public static int GetCurrentUserProfileId(this Page page)
        {
            return Membership.GetUser(page.User.Identity.Name).GetUserProfileId();
        }

        /// <summary>
        /// Выполнить клиентский скрипт.
        /// </summary>
        /// <param name="page">Текущая страница</param>
        /// <param name="script">JavaScript для выполнения на клиенте.</param>
        public static void Script(this Page page, string script)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        /// <summary>
        /// Добавить клиентский скрипт на страницу
        /// </summary>
        /// <param name="page">Текущая страница</param>
        /// <param name="scriptUrl">Путь к скрипту</param>
        public static void IncludeScript(this Page page, string scriptUrl)
        {
            ScriptManager.RegisterClientScriptInclude(page, page.GetType(), Guid.NewGuid().ToString(), scriptUrl);
        }

        /// <summary>
        /// Выполнить клиентский скрипт.
        /// </summary>
        /// <param name="page">Текущая страница <see cref="Page"/>.</param>
        /// <param name="script">JavaScript для выполнения на клиенте.</param>
        public static void ScriptBlock(this Page page, string script)
        {
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        /// <summary>
        /// Открытие диалога
        /// </summary>
        /// <param name="page">Текущая страница</param>
        /// <param name="dialogClientId">Клиентский идентификатор диалоговой формы</param>
        /// <param name="dialogTitle">Заголовок</param>
        /// <param name="dialogWidth">Ширина</param>
        /// <param name="dialogHeigth">Высота</param>
        /// <param name="disableEnterKey">Отключение клавиши Enter на тектовых полях</param>
        /// <param name="closeDialogButtonId">Идентификатор кнопки закрытия диалогового окна</param>
        /// <param name="modal">Модальное окно</param>
        /// <param name="titleBarVisible">Видимость заголовка окна</param>
        /// <param name="closeButtonVisible">Видимость кнопки "крестик"</param>
        public static void OpenDialog(this Page page, 
                                      string dialogClientId, 
                                      string dialogTitle,
                                      int dialogWidth, 
                                      int dialogHeigth,
                                      bool disableEnterKey = true,
                                      string closeDialogButtonId = null,
                                      bool modal = true,
                                      bool titleBarVisible = true,
                                      bool closeButtonVisible = true)
        {
            Script(page, string.Format("openDialog('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');",
                                       dialogClientId,
                                       dialogTitle,
                                       dialogWidth,
                                       dialogHeigth,
                                       disableEnterKey,
                                       closeDialogButtonId,
                                       modal,
                                       titleBarVisible,
                                       closeButtonVisible));
        }

        /// <summary>
        /// Открытие диалога
        /// </summary>
        /// <param name="page">Текущая страница</param>
        /// <param name="dialogClientId">Клиентский идентификатор диалоговой формы</param>
        /// <param name="dialogTitle">Заголовок</param>
        /// <param name="dialogWidth">Ширина</param>
        /// <param name="dialogHeigth">Высота</param>
        /// <param name="disableEnterKey">Отключение клавиши Enter на тектовых полях</param>
        /// <param name="closeDialogButtonId">Идентификатор кнопки закрытия диалогового окна</param>
        /// <param name="modal">Модальное окно</param>
        /// <param name="titleBarVisible">Видимость заголовка окна</param>
        /// <param name="closeButtonVisible">Видимость кнопки "крестик"</param>
        /// <remarks>
        /// Если не заданы значения параметров dialogWidth и(или) dialogHeigth,
        /// то ширина и(или) высота диалогового окна будут определяться автоматически по его содержимому
        /// </remarks>
        public static void OpenDialog(  this Page page,
                                        string dialogClientId,
                                        string dialogTitle,
                                        int? dialogWidth = null,
                                        int? dialogHeigth = null,
                                        bool disableEnterKey = true,
                                        string closeDialogButtonId = null,
                                        bool modal = true,
                                        bool titleBarVisible = true,
                                        bool closeButtonVisible = true)
        {
            Script(page, string.Format("openDialog('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');",
                                       dialogClientId,
                                       dialogTitle,
                                       dialogWidth,
                                       dialogHeigth,
                                       disableEnterKey,
                                       closeDialogButtonId,
                                       modal,
                                       titleBarVisible,
                                       closeButtonVisible));
        }

        /// <summary>
        /// Закрытие диалога
        /// </summary>
        /// <param name="page">Текущая страница</param>
        /// <param name="dialogClientId">Клиентский идентификатор диалоговой формы</param>
        public static void CloseDialog(this Page page, string dialogClientId)
        {
            Script(page, string.Format("closeDialog('{0}');", dialogClientId));
        }

        /// <summary>
        /// Изменение заголовка окна
        /// </summary>
        /// <param name="page">Текущая страница</param>
        /// <param name="dialogClientId">Клиентский идентификатор диалоговой формы</param>
        /// <param name="title">Заголовок</param>
        public static void ChangeDialogTitle(this Page page, string dialogClientId, string title)
        {
            Script(page, $"$('#{dialogClientId}').dialog('option', 'title', '{title}');");
        }

        /// <summary>
        /// Выполнить клиентский скрипт со стандартным сообщением об ошибке
        /// </summary>
        /// <param name="page">Текущая страница</param>
        /// <param name="message">Текст сообщения</param>
        public static void StandardErrorScript(this Page page, string message = null)
        {
            Control wmb = FindControlRecursiveById(page, "MasterPageWebMessageBox");
            if (wmb == null)
                Script(page, "alertError();");
            else
            {
                Script(page,
                    $@" if (window.showMessageBox)
                                                  {
                        $"showMessageBox('{wmb.ClientID}','{(string.IsNullOrEmpty(message) ? "К сожалению произошла ошибка. Попробуйте еще раз..." : message)}','Внимание!','error');"
                        }
                                              else 
                                                  alertError();");
            }
        }

        /// <summary>
        /// Отобразить информационное сообщение
        /// </summary>
        /// <param name="page">Страницы</param>
        /// <param name="format">Форматированная строка</param>
        /// <param name="args">Параметры</param>
        /// <param name="asynch">Вызвать сообщение асинхронно</param>
        /// <param name="goToUri">Переход на страницу</param>
        public static void Alert(this Page page, string format, bool asynch = true, string goToUri = null, params object[] args)
        {
            ShowMessageBox(page, String.Format(format, args), "Внимание!", MessageBoxTypesCodes[MessageBoxTypes.Info], asynch, goToUri);
        }

        /// <summary>
        /// Отобразить сообщение об ошибке
        /// </summary>
        /// <param name="page">Страницы</param>
        /// <param name="format">Форматированная строка</param>
        /// <param name="args">Параметры</param>
        public static void ErrorAlert(this Page page, string format, params object[] args)
        {
            ShowMessageBox(page, String.Format(format, args), "Внимание!", MessageBoxTypesCodes[MessageBoxTypes.Error]);
        }

        /// <summary>
        /// Отобразить предупреждение об ошибке
        /// </summary>
        /// <param name="page">Страницы</param>
        /// <param name="format">Форматированная строка</param>
        /// <param name="args">Параметры</param>
        public static void WarningAlert(this Page page, string format, params object[] args)
        {
            ShowMessageBox(page, String.Format(format, args), "Внимание!", MessageBoxTypesCodes[MessageBoxTypes.Warning]);
        }

        /// <summary>
        /// Получение даты в строком представлении
        /// </summary>
        /// <param name="dt">Дата и время</param>
        /// <param name="format">Формат строки</param>
        /// <returns></returns>
        public static string GetDate(object dt, string format = "dd.MM.yyyy")
        {
            DateTime? dateTime = dt.ToDateTime(null);
            return (dateTime.HasValue) ? dateTime.Value.ToString(format) : "Не указано";
        }

        /// <summary>
        /// Получение всех контролов определенного типа
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="page">Страница</param>
        /// <param name="control">Контрол</param>
        /// <returns></returns>
        public static List<T> FindControls<T>(this Page page, Control control) where T : Control
        {
            List<T> foundControls = new List<T>();
            FindControlsReqursive(control, foundControls);
            return foundControls;
        }

        /// <summary>
        /// Получение первого контрола с определенным идентификатором
        /// </summary>
        /// <param name="page">Страница</param>
        /// <param name="controlId">Идентификатор контрола</param>
        /// <returns></returns>
        public static Control FindControlRecursiveById(this Page page, string controlId)
        {
            Control foundControl = null;

            foreach (Control control in page.Controls)
            {
                foundControl = FindControlReqursive(control, controlId);
                if (foundControl != null)
                    break;
            }

            return foundControl;
        }

        /// <summary>
        /// Получение первого контрола с определенным идентификатором
        /// </summary>
        /// <param name="page">Страница</param>
        /// <param name="controlClientId">Идентификатор контрола</param>
        /// <returns></returns>
        public static Control FindControlRecursiveByClientId(this Page page, string controlClientId)
        {
            Control foundControl = null;

            foreach (Control control in page.Controls)
            {
                foundControl = FindControlReqursiveByClientId(control, controlClientId);
                if (foundControl != null)
                    break;
            }

            return foundControl;
        }

        /// <summary>
        /// Получения контрола с определенным идентификаторм
        /// в родителях заданного контрола (рекурсия вверх)
        /// </summary>
        /// <param name="owner">Контрол</param>
        /// <param name="controlId">Идентификатор контрола</param>
        /// <returns></returns>
        public static Control FindControlOnParentById(this Control owner, string controlId)
        {
            return FindControlOnParentReqursive(owner.Parent, controlId);
        }

        /// <summary>
        /// Получения контрола с определенным идентификаторм
        /// в родителях заданного контрола (рекурсия вверх)
        /// </summary>
        /// <param name="owner">Контрол</param>
        /// <param name="controlId">Идентификатор контрола</param>
        /// <returns></returns>
        public static Control FindControlOnParentReqursive(Control owner, string controlId)
        {
            Control result = owner.Controls.Cast<Control>().FirstOrDefault(control => control.Visible && control.ID == controlId);

            if (result == null && owner.Parent != null)
                result = FindControlOnParentReqursive(owner.Parent, controlId);

            return result;
        }

        /// <summary>
        /// Получение всех контрола с определенным идентификатором
        /// </summary>
        /// <typeparam name="T">Тип контрола</typeparam>
        /// <param name="page">Страница</param>
        /// <param name="controlId">Идентификатор контрола</param>
        /// <returns></returns>
        public static T FindControlRecursiveById<T>(this Page page, string controlId)
            where T : Control
        {
            return (T) FindControlRecursiveById(page, controlId);
        }

        /// <summary>
        /// Получение всех контролов с определенным идентификатором
        /// </summary>
        /// <param name="page">Страница</param>
        /// <param name="controlId">Идентификатор контрола</param>
        /// <returns></returns>
        public static List<Control> FindControlsRecursiveByIdSubString(this Page page, string controlId)
        {
            List<Control> foundControls = new List<Control>();
            
            foreach (Control control in page.Controls)
                FindControlsRecursiveByIdSubString(control, controlId, foundControls);

            return foundControls;
        }

        /// <summary>
        /// Возвращает случайную строку для задания уникальной группы валидации контрола
        /// </summary>
        /// <returns></returns>
        public static string GenerateValidationGroup(this Page page)
        {
            return Path.GetRandomFileName().Replace(".", string.Empty).Substring(0, 11);
        }

        /// <summary>
        /// Получение абсолютного пути к странице
        /// (путь строится от корневой директории)
        /// </summary>
        /// <param name="page">Страница</param>
        /// <param name="attachQuery">Прикрепить запрос к пути</param>
        /// <returns></returns>
        public static string GetAbsoluteUrl(this Page page, bool attachQuery = true)
        {
            return (attachQuery)? page.Request.Url.PathAndQuery : page.Request.Url.AbsolutePath;
        }

        /// <summary>
        /// Получение адреса страницы для возврата
        /// </summary>
        /// <param name="page">Страница</param>
        /// <param name="defaultUrl">URL по умолчанию</param>
        /// <returns></returns>
        public static string GetReturnUrl(this Page page, string defaultUrl = null)
        {
            string returnUrl = page.Request["ReturnUrl"];
            if (!returnUrl.NotEmpty())
                returnUrl = page.Request.UrlReferrer != null ? page.Request.UrlReferrer.AbsoluteUri : string.Empty;
            if (!returnUrl.NotEmpty())
                returnUrl = defaultUrl;
            return returnUrl ?? string.Empty;
        }

        /// <summary>
        /// Зарегистрировать элемент управления, который инициирует загрузку файлов.
        /// </summary>
        public static void RegisterDownloadControl(this Page page, params Control[] controls)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(page);

            if (scriptManager != null)
            {
                foreach (Control control in controls.Where(control => control != null))
                {
                    scriptManager.RegisterPostBackControl(control);
                    BindClientEvents(page, control);
                }
            }
        }

        #endregion

        #region Приватные методы

        /// <summary>
        /// Рекурсивное получение всех контролов определенного типа
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="parent">Родительский контрол</param>
        /// <param name="foundControls">Список найденных контролоы</param>
        /// <returns></returns>
        private static void FindControlsReqursive<T>(Control parent, List<T> foundControls) where T : Control
        {
            foreach (Control control in parent.Controls)
            {
                if (control is T && control.Visible)
                    foundControls.Add((T) control);

                if (control.HasControls())
                    FindControlsReqursive(control, foundControls);
            }
        }

        /// <summary>
        /// Рекурсивное получение первого контрола с определенным идентификатором
        /// </summary>
        /// <param name="parent">Родительский контрол</param>
        /// <param name="controlId">Идентификатор контрола</param>
        /// <returns></returns>
        private static Control FindControlReqursive(Control parent, string controlId)
        {
            Control result = null;

            foreach (Control control in parent.Controls)
            {
                if (control.Visible && control.ID == controlId)
                {
                    result = control;
                    break;
                }

                if (control.HasControls())
                {
                    result = FindControlReqursive(control, controlId);
                    if (result != null)
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Рекурсивное получение первого контрола с определенным идентификатором
        /// </summary>
        /// <param name="parent">Родительский контрол</param>
        /// <param name="controlClientId">Идентификатор контрола</param>
        /// <returns></returns>
        private static Control FindControlReqursiveByClientId(Control parent, string controlClientId)
        {
            Control result = null;

            foreach (Control control in parent.Controls)
            {
                if (control.Visible && control.ClientID == controlClientId)
                {
                    result = control;
                    break;
                }

                if (control.HasControls())
                {
                    result = FindControlReqursiveByClientId(control, controlClientId);
                    if (result != null)
                        break;
                }
            }

            return result;
        }        

        /// <summary>
        /// Получение всех контролов с начальной подстрокой идентификатора
        /// </summary>
        /// <param name="parent">Родительский контрол</param>
        /// <param name="controlIdSubstring">Начальная подстрока идентификатора контрола</param>
        /// <param name="foundControls">Список контролов, соответсвующих controlIdSubstring</param>
        /// <returns></returns>
        private static void FindControlsRecursiveByIdSubString(Control parent, string controlIdSubstring, List<Control> foundControls)
        {
            foreach (Control control in parent.Controls)
            {
                if (control.Visible && !string.IsNullOrEmpty(control.ID) && control.ID.StartsWith(controlIdSubstring))
                    foundControls.Add(control);

                if (control.HasControls())
                    FindControlsRecursiveByIdSubString(control, controlIdSubstring, foundControls);
            }
        }

        /// <summary>
        /// Отображение сообщение в виде алерта или messagebox
        /// </summary>
        /// <param name="page">Страница</param>
        /// <param name="text">Текст сообщения</param>
        /// <param name="title">Заголовок сообщения</param>
        /// <param name="type">Тип сообщения</param>
        /// <param name="asynch">Отобразить сообщение асинхронно</param>
        /// <param name="goToUri">Переход на страницу</param>
        private static void ShowMessageBox(Page page, string text, string title, string type, bool asynch = true, string goToUri = null)
        {
            Control wmb = FindControlRecursiveById(page, "MasterPageWebMessageBox");

            
            if (wmb == null)
            {
                Script(page, $"alert{(asynch ? "Async" : string.Empty)}('{text}'); {((!string.IsNullOrWhiteSpace(goToUri)) ? $"window.location = '{goToUri}';" : string.Empty)}" );
            }
            else
            {
                Script(page,
                    $@" if (window.showMessageBox)
                        {{
                                    {$"showMessageBox('{wmb.ClientID}','{text}','{title}','{type}');"}
                                    {((!string.IsNullOrWhiteSpace(goToUri)) ? $"window.location = '{goToUri}';" : string.Empty)}
                        }}  else  {{ 
                                    alert{(asynch ? "Async" : string.Empty)}('{text}');
                                    {((!string.IsNullOrWhiteSpace(goToUri)) ? $"window.location = '{goToUri}';" : string.Empty)}
                        }}");
            }
        }

        /// <summary>
        /// Получить ClientId контрола WebMessageBox
        /// </summary>
        /// <param name="page">Страница</param>
        /// <returns>ClientId контрола WebMessageBox</returns>
        public static string GetMasterPageWebMessageBoxClientId(this Page page)
        {
            Control wmb = FindControlRecursiveById(page, "MasterPageWebMessageBox");
            if (wmb == null) throw new Exception("Элемент управления WebMessageBox не найден на странице!");
            return wmb.ClientID;
        }

        /// <summary>
        /// Типы сообщений
        /// </summary>
        internal enum MessageBoxTypes
        {
            /// <summary>
            /// Информация
            /// </summary>
            Info = 0,

            /// <summary>
            /// Ошибка
            /// </summary>
            Error = 1,

            /// <summary>
            /// Требует внимания
            /// </summary>
            Warning = 2
        }

        /// <summary>
        /// Коды типов сообщений
        /// </summary>
        internal static Dictionary<MessageBoxTypes, string> MessageBoxTypesCodes =
            new Dictionary<MessageBoxTypes, string>
            {
                { MessageBoxTypes.Info, "info" },
                { MessageBoxTypes.Error, "error" },
                { MessageBoxTypes.Warning, "warning" },

            };

        /// <summary>
        /// Возвращает код типа сообщения "информация"
        /// </summary>
        /// <param name="page">Страница</param>
        /// <returns>Код типа сообщения "информация"</returns>
        public static string GetInfoMessageBoxTypesCode(this Page page)
        {
            return MessageBoxTypesCodes[MessageBoxTypes.Info];
        }

        /// <summary>
        /// Привязать обработчики клиентских событий
        /// </summary>
        /// <param name="page">Страница</param>
        /// <param name="control">Контрол</param>
        private static void BindClientEvents(Page page, Control control)
        {
            page.Script(" $(\"#" + control.ClientID + "\").unbind(\"click\", " + BaseWaitControl.OnClientClickEventHandler + ").bind(\"click\", " + BaseWaitControl.OnClientClickEventHandler + "); ");
        }

        #endregion
    }
}
