using System;

namespace Icona.Usercontrols
{
    public partial class WaitControl : BaseWaitControl
    {
        #region Свойства

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text
        {
            set
            {
                TextLbl.Text = value;
                WaitUpdatePanel.Update();
            }
            get
            {
                return TextLbl.Text;
            }
        }

        #endregion

        #region Обработчики событий

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeUniqueKey();
        }

        #endregion

        #region Публичные методы

        public override string GetUniqueKey()
        {
            return hfWaitControlUniqueKey.Value;
        }

        #endregion

        #region Приватные методы

        /// <summary>
        /// Инициализирует значение уникального ключа
        /// </summary>
        private void InitializeUniqueKey()
        {
            if (!IsPostBack)
            {
                hfWaitControlUniqueKey.Value = Guid.NewGuid().ToString();
            }
        }

        #endregion
    }
}