using System;
using System.Web.UI;
using Icona.Logic.Enums;
using Icona.Logic.Extensions;

namespace Icona.Usercontrols
{
    /// <summary>
    /// Контрол для отображения сообщений и элементарных диалогов
    /// </summary>
    public partial class WebMessageBox : UserControl
    {
        #region Свойства

        /// <summary>
        /// Ширина окна
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Высота окна
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Свойство для хранения пользовательских данных
        /// </summary>
        public string Data
        {
            get { return (string)ViewState[$"{ID}_WebMessageBoxData"]; }
            set { ViewState[$"{ID}_WebMessageBoxData"] = value; }
        }

        /// <summary>
        /// Текст сообщения в диалоге
        /// </summary>
        public string Text
        {
            set
            {
                WebMessageBoxTextlbl.Text = value;
                WebMessageBoxUpdPnl.Update();
            }
            get
            {
                return WebMessageBoxTextlbl.Text;
            }
        }

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Показывать ли кнопку Отмена
        /// </summary>
        public bool ShowCancelButton
        {
            set
            {
                CancelBtn.Visible = value;
            }
            get
            {
                return CancelBtn.Visible;
            }
        }

        /// <summary>
        /// Текст кнопки Отмена
        /// </summary>
        public string CancelButtonText
        {
            set
            {
                CancelBtn.Text = value;
            }
            get
            {
                return CancelBtn.Text;
            }
        }

        /// <summary>
        /// Показывать ли кнопку Да
        /// </summary>
        public bool ShowYesButton
        {
            set
            {
                YesBtn.Visible = value;
            }
            get
            {
                return YesBtn.Visible;
            }
        }

        /// <summary>
        /// Показывать ли кнопку Нет
        /// </summary>
        public bool ShowNoButton
        {
            set
            {
                NoBtn.Visible = value;
            }
            get
            {
                return NoBtn.Visible;
            }
        }

        /// <summary>
        /// Текст всплывающей подсказки кнопки "Да"
        /// </summary>
        public string YesButtonToolTip
        {
            set
            {
                YesBtn.ToolTip = value;
            }
            get
            {
                return YesBtn.ToolTip;
            }
        }

        /// <summary>
        /// Текст кнопки Да
        /// </summary>
        public string YesButtonText
        {
            set
            {
                YesBtn.Text = value;
            }
            get
            {
                return YesBtn.Text;
            }
        }

        /// <summary>
        /// Текст кнопки Нет
        /// </summary>
        public string NoButtonText
        {
            set
            {
                NoBtn.Text = value;
            }
            get
            {
                return NoBtn.Text;
            }
        }

        /// <summary>
        /// Текст всплывающей подсказки кнопки "Нет"
        /// </summary>
        public string NoButtonToolTip
        {
            set
            {
                NoBtn.ToolTip = value;
            }
            get
            {
                return NoBtn.ToolTip;
            }
        }

        /// <summary>
        /// Иконка
        /// </summary>
        public WebMessageBoxIcons Icon
        {
            set
            {
                if (value == WebMessageBoxIcons.None)
                    WebMessageBoxIcon.Visible = false;
                else
                    WebMessageBoxIcon.ImageUrl = $"~/Images/WebMessageBox/{value}.png";
            }
        }

        #endregion

        #region События

        /// <summary>
        /// Нажата кнопка Да
        /// </summary>
        public event EventHandler YesButtonPressed;

        /// <summary>
        /// Нажата кнопка Нет
        /// </summary>
        public event EventHandler NoButtonPressed;

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор
        /// </summary>
        public WebMessageBox()
        {
            Height = 190;
            Width = 470;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Показать форму
        /// </summary>
        public void Show()
        {
            Page.OpenDialog(WebMessageBoxPanel.ClientID, Title, Width, Height, false);
        }

        /// <summary>
        /// Показать форму
        /// </summary>
        /// <param name="text">Текст</param>
        public void Show(string text)
        {
            Show(WebMessageBoxIcons.Info, text);
        }

        /// <summary>
        /// Показать форму
        /// </summary>
        /// <param name="icon">Иконка</param>
        /// <param name="text">Текст</param>
        /// <param name="title">Заголовок</param>
        /// <param name="height">Высота</param>
        /// <param name="width">Ширина</param>
        public void Show(WebMessageBoxIcons icon, string text, string title = "", int? height = null, int? width = null)
        {
            Icon = icon;
            Text = text;
            Title = title;
            Height = height ?? Height;
            Width = width ?? Width;
            WebMessageBoxUpdPnl.Update();
            Page.OpenDialog(WebMessageBoxPanel.ClientID, Title, Width, Height, false);
        }

        /// <summary>
        /// Показать информационное сообщение
        /// </summary>
        /// <param name="text">Текст</param>
        public void ShowInfo(string text)
        {
            Show(WebMessageBoxIcons.Info, text);
        }

        /// <summary>
        /// Показать сообщение об ошибке
        /// </summary>
        /// <param name="text">Текст</param>
        public void ShowError(string text)
        {
            Show(WebMessageBoxIcons.Error, text);
        }

        /// <summary>
        /// Показать предупреждение
        /// </summary>
        /// <param name="text">Текст</param>
        public void ShowWarning(string text)
        {
            Show(WebMessageBoxIcons.Warning, text);
        }

        #endregion

        #region Обработчики событий

        protected void YesBtn_Click(object sender, EventArgs e)
        {
            YesButtonPressed?.Invoke(sender, e);
        }

        protected void NoBtn_Click(object sender, EventArgs e)
        {
            NoButtonPressed?.Invoke(sender, e);
        }

        #endregion
    }

}