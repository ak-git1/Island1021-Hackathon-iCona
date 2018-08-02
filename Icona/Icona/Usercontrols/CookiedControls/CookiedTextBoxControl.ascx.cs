using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ak.Framework.Core.Extensions;
using Icona.Logic.Extensions;

namespace Icona.Usercontrols.CookiedControls
{
    /// <summary>
    /// Контрол сохраняющий содержимое текстового окна в куках. Предназначен для использования в панелях фильтров.
    /// </summary>
    public partial class CookiedTextBoxControl : UserControl
    {
        #region Свойства 

        /// <summary>
        /// CSS
        /// </summary>
        public string CssClass
        {
            get
            {
                return TextBox.CssClass;
            }
            set
            {
                TextBox.CssClass = value;
            }
        }

        /// <summary>
        /// Отображать ли календарь
        /// </summary>
        public bool CalendarEnabled
        {
            set
            {
                TextBoxCalendarExtender.Enabled = value;
            }
            get
            {
                return TextBoxCalendarExtender.Enabled;
            }
        }

        /// <summary>
        /// Задействовать ли AutoCompleteExtender
        /// </summary>
        public bool AutoCompleteExtenderEnabled
        {
            set
            {
                AutoCompleteExtender.Enabled = value;
            }
            get
            {
                return AutoCompleteExtender.Enabled;
            }
        }

        /// <summary>
        /// Название метода для AutoCompleteExtender
        /// </summary>
        public string AutoCompleteExtenderServiceMethod
        {
            set
            {
                AutoCompleteExtender.ServiceMethod = value;
            }
            get
            {
                return AutoCompleteExtender.ServiceMethod;
            }
        }

        /// <summary>
        /// Сохранять ли содержимое текстового окна в куках
        /// </summary>
        public bool SaveValueToCookie { get; set; }

        /// <summary>
        /// Текст
        /// </summary>
        public string Text
        {
            set
            {
                TextBox.Text = Trim(value);
                if (SaveValueToCookie)
                    Page.Script($"add2Cookie('{ClientID}_TextBox_Text','');");
            }
            get
            {
                return Trim(TextBox.Text);
            }
        }

        /// <summary>
        /// Ширина
        /// </summary>
        public double Width
        {
            get
            {
                return TextBox.Width.Value;
            }
            set
            {
                TextBox.Width = new Unit(value);
            }
        }

        /// <summary>
        /// Разрешить удаление пробельных символов в начале и конце текста.
        /// </summary>
        public bool EnableTrimming { get; set; }

        /// <summary>
        /// Заменять несколько пробельных символов внутри текста на один пробел
        /// </summary>
        public bool DisablePreserveInsignificantWhitespace { get; set; }

        /// <summary>
        /// Заменить знак пробела на % (для поиска по частям нескольких слов)
        /// </summary>
        public bool ReplaceWhitespaceOnPercentSign { get; set; }

        /// <summary>
        /// Максимальная допустимая длина текста
        /// </summary>
        public int TextMaxLength
        {
            get { return TextBox.MaxLength; }
            set { TextBox.MaxLength = value; }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Убирает лишние пробельные символы в тексте
        /// </summary>
        /// <returns></returns>
        private string Trim(string text)
        {
            if (text.NotEmpty())
            {
                if (DisablePreserveInsignificantWhitespace && TextBox.TextMode == TextBoxMode.SingleLine)
                    text = Regex.Replace(text, "\\s+", " ");

                if (EnableTrimming)
                    text = text.Trim();

                if (ReplaceWhitespaceOnPercentSign && text.NotEmpty())
                    text = Regex.Replace(text, "(?<a>[^[])[ ](?<b>[^]])", "$1%$2");
            }

            return text;
        }

        #endregion

        #region Обработчики событий

        protected override void OnInit(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (SaveValueToCookie)
                {
                    TextBox.Attributes.Add("onchange", "cookiedTextBoxOnChange(this)");
                    TextBox.Text = Trim(this.GetFromCookie("TextBox_Text"));
                }
            }
            base.OnInit(e);
        }

        #endregion
    }

}