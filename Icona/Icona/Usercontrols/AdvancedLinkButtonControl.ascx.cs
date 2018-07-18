/***********************************************************************************
* 
* Ограничения: 
* если на OnLoad родительского контрола есть проверка !IsPostBack,
* то OnCommand и OnClick работать не будут, т.к в этом случае событию не будет назначен обработчик,
* т.к. не будет вызван Initialize
* 
***********************************************************************************/

using System;
using System.Web.UI.WebControls;
using Elar.Framework.Core.Extensions;
using Icona.Logic.Extensions;
using UserControl = System.Web.UI.UserControl;

namespace Icona.Usercontrols
{
    public partial class AdvancedLinkButtonControl : UserControl
    {
        #region Переменные и константы

        /// <summary>
        /// Инициализация закончена
        /// </summary>
        private bool _initialized;

        #endregion

        #region Свойства

        /// <summary>
        /// Признак того, что контрол отображается
        /// </summary>
        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
                Image.Visible = LinkBtn.Visible = value;
            }
        }

        /// <summary>
        /// Всплывающая подсказка
        /// </summary>
        public string ToolTip
        {
            get
            {
                return LinkBtn.ToolTip;
            }
            set
            {
                LinkBtn.ToolTip = value;
                Image.Attributes.Add("title", value);
            }
        }

        /// <summary>
        /// Включено
        /// </summary>
        public bool Enabled
        {
            get
            {
                return LinkBtn.Enabled;
            }
            set
            {
                LinkBtn.Enabled = value;
                Image.Attributes.Add("disabled", value.ToString());
            }
        }

        /// <summary>
        /// Путь к изображению
        /// (рекомендуемый размер изображения 17x17)
        /// </summary>
        public string ImageUrl
        {
            get
            {
                return Image.Src;
            }
            set
            {
                Image.Src = value;
            }
        }

        /// <summary>
        /// Обработка клика на клиенсткой стороне с помощью javascript
        /// </summary>
        public string OnClientClick
        {
            get
            {
                return LinkBtn.OnClientClick;
            }
            set
            {
                LinkBtn.OnClientClick = value;
            }
        }

        /// <summary>
        /// Аргумент выполнения команды
        /// </summary>
        public string CommandArgument
        {
            get
            {
                return LinkBtn.CommandArgument;
            }
            set
            {
                LinkBtn.CommandArgument = value;
            }
        }

        /// <summary>
        /// Название команды
        /// </summary>
        public string CommandName
        {
            get
            {
                return LinkBtn.CommandName;
            }
            set
            {
                LinkBtn.CommandName = value;
            }
        }

        /// <summary>
        /// Текст ссылки
        /// </summary>
        public string Text
        {
            get
            {
                return LinkBtnName.InnerText;
            }
            set
            {
                LinkBtnName.InnerText = value;
            }
        }

        /// <summary>
        /// Адресс ссылки для перехода
        /// </summary>
        public string DestinationUrl
        {
            get
            {
                return IsPostBack && !StringExtensions.NotEmpty(DestinationUrlHf.Value)
                    ? Request[DestinationUrlHf.UniqueID]
                    : DestinationUrlHf.Value;
            }
            set
            {
                DestinationUrlHf.Value = value;
                _initialized = false;
            }
        }

        /// <summary>
        /// Адрес текущей страницы для возврата
        /// </summary>
        public string ReturnUrl
        {
            get
            {
                string returnUrl = ReturnUrlHf.Value;
                if (IsPostBack && !returnUrl.NotEmpty())
                    returnUrl = Request[ReturnUrlHf.UniqueID];

                return string.IsNullOrEmpty(returnUrl)
                    ? Page.GetAbsoluteUrl().UrlEncode()
                    : returnUrl;
            }
            set
            {
                ReturnUrlHf.Value = value;
                _initialized = false;
            }
        }

        /// <summary>
        /// Стиль для текста внутри кнопки
        /// </summary>
        public string CssClass { get; set; }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор
        /// </summary>
        public AdvancedLinkButtonControl()
        {
            CssClass = "number";
        }

        #endregion

        #region События

        /// <summary>
        /// Событие выполнение команды
        /// </summary>
        public event EventHandler<CommandEventArgs> Command;

        /// <summary>
        /// Событие выполнение нажатия на кнопку
        /// </summary>
        public event EventHandler Click;

        #endregion

        #region Методы

        public void Initialize(bool useReturnUrl = true)
        {
            if (_initialized)
                return;

            if (DestinationUrl.NotEmpty())
            {
                LinkBtn.Command += null;
                string url = useReturnUrl ? string.Format("{0}&ReturnUrl={1}", DestinationUrl, ReturnUrl) : DestinationUrl;

                LinkBtn.OnClientClick = $"var win = window.open('{url}', '_blank'); win.focus(); return false;";
                LinkBtn.Click += null;
            }
            else
            {
                LinkBtn.Command += LinkBtn_OnCommand;
                LinkBtn.Click += LinkBtn_OnClick;
            }

            ImageDiv.Visible = StringExtensions.NotEmpty(Image.Src);
            _initialized = true;
        }

        #endregion

        #region Обработчики событий

        protected void LinkBtn_OnCommand(object sender, CommandEventArgs e)
        {
            if (Command != null)
                Command(this, e);
        }

        protected void LinkBtn_OnClick(object sender, EventArgs e)
        {
            if (Click != null)
                Click(this, e);
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            Initialize();
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            if (!IsPostBack)
                Initialize();

            if (!Enabled)
                LinkBtn.OnClientClick = string.Empty;
        }

        #endregion
    }

}