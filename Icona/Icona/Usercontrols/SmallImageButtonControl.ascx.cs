using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ak.Framework.Core.Extensions;
using Icona.Logic.Extensions;
using Icona.Usercontrols.Interfaces;

namespace Icona.Usercontrols
{
    public partial class SmallImageButtonControl : UserControl, IHasOnClientClickEventControl
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
            set
            {
                base.Visible = value;
                LinkBtn.Visible = value;
            }
            get => base.Visible;
        }

        /// <summary>
        /// Валидировать форму после нажатия на кнопку
        /// </summary>
        public bool CausesValidation
        {
            set => LinkBtn.CausesValidation = value;
            get => LinkBtn.CausesValidation;
        }

        /// <summary>
        /// Текст бейджа
        /// </summary>
        public string BadgeText
        {
            get => Badge.InnerText;
            set
            {
                Badge.InnerText = value;
                if (StringExtensions.NotEmpty(Badge.InnerText))
                {
                    Badge.Style.Add("display", "inline");
                    Badge.Visible = true;
                }
                else
                {
                    Badge.Style.Add("display", "none");
                    Badge.Visible = false;
                }
            }
        }

        /// <summary>
        /// Текст бейджа
        /// </summary>
        public string BadgeToolTip
        {
            get => ConvertExtentions.ToStr(Badge.Attributes["title"]);
            set => Badge.Attributes.Add("title", value);
        }

        /// <summary>
        /// Всплывающая подсказка
        /// </summary>
        public string ToolTip
        {
            get => LinkBtn.ToolTip;
            set
            {
                LinkBtn.ToolTip = value;
                ImageBtn.Attributes.Add("title", value);
            }
        }

        /// <summary>
        /// Включено
        /// </summary>
        public bool Enabled
        {
            get => LinkBtn.Enabled;
            set => LinkBtn.Enabled = value;
        }

        /// <summary>
        /// Путь к изображению
        /// (рекомендуемый размер изображения 17x17)
        /// </summary>
        public string ImageUrl
        {
            get => ImageBtn.Src;
            set => ImageBtn.Src = value;
        }

        /// <summary>
        /// Обработка клика на клиенсткой стороне с помощью javascript
        /// </summary>
        public string OnClientClick
        {
            get => LinkBtn.OnClientClick;
            set => LinkBtn.OnClientClick = value;
        }

        /// <summary>
        /// Аргумент выполнения команды
        /// </summary>
        public string CommandArgument
        {
            get => LinkBtn.CommandArgument;
            set => LinkBtn.CommandArgument = value;
        }

        /// <summary>
        /// Название команды
        /// </summary>
        public string CommandName
        {
            get => LinkBtn.CommandName;
            set => LinkBtn.CommandName = value;
        }

        /// <summary>
        /// Адресс ссылки для перехода
        /// </summary>
        public string DestinationUrl
        {
            get
            {
                if (IsPostBack && !StringExtensions.NotEmpty(DestinationUrlHf.Value))
                    return Request[DestinationUrlHf.UniqueID];
                return DestinationUrlHf.Value;
            }
            set
            {
                DestinationUrlHf.Value = value;
                // Проводим переинициализацию ссылки
                InitializeLink();
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
                // Проводим переинициализацию ссылки
                InitializeLink();
            }
        }

        /// <summary>
        /// ClientID
        /// </summary>
        public override string ClientID => LinkBtn.ClientID;

        /// <summary>
        /// Url для перенаправления
        /// </summary>
        private string ResponseRedirectUrl { get; set; }

        #endregion

        #region Методы

        /// <summary>
        /// Инициализация данных контрола
        /// </summary>
        public void Initialize()
        {
            if (_initialized)
                return;

            InitializeLink();
            _initialized = true;
        }

        /// <summary>
        /// Инициализировать ссылку
        /// </summary>
        private void InitializeLink()
        {
            if (DestinationUrl.NotEmpty())
            {
                LinkBtn.Command += null;
                string url = string.Format("{0}&ReturnUrl={1}", DestinationUrl, ReturnUrl);

                LinkBtn.OnClientClick = string.Format("var win = window.open('{0}', '_blank');win.focus(); return false;", url);
                LinkBtn.Click += null;
            }
            else
            {
                LinkBtn.Command += LinkBtn_OnCommand;
                LinkBtn.Click += LinkBtn_OnClick;
            }
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

        #region Обработчики событий

        protected void LinkBtn_OnCommand(object sender, CommandEventArgs e)
        {
            if (Command != null)
                Command(this, e);
        }

        protected void LinkBtn_OnClick_Redirect(object sender, EventArgs e)
        {
            Response.Redirect(ResponseRedirectUrl, false);
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

        protected void Page_Load(object sender, EventArgs e)
        {
            Initialize();

            if (!Enabled)
                LinkBtn.OnClientClick = string.Empty;
        }

        #endregion
    }
}