using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ak.Framework.Core.Extensions;
using Icona.Usercontrols.Interfaces;

namespace Icona.Usercontrols
{
    /// <summary>
    /// Контрол для вывода дочерних контролов на основе повторяющегося шаблона
    /// </summary>
    [ParseChildren(true)]
    public class RepeatedControls : Control
    {
        #region Переменные и константы

        /// <summary>
        /// HTML-разметка перед заголовком
        /// </summary>
        private string _beforeLabelHtml;

        /// <summary>
        /// HTML-разметка перед контролом
        /// </summary>
        private string _beforeControlHtml;

        /// <summary>
        /// HTML-разметка между контролами
        /// </summary>
        private string _separatorHtml;

        /// <summary>
        /// HTML-разметка после контрола
        /// </summary>
        private string _afterControlHtml;

        #endregion

        #region Свойства

        /// <summary>
        /// Есть ли видимые дочерние контролы
        /// </summary>
        internal bool HasVisibleChildControls
        {
            get
            {
                EnsureChildControls();
                foreach (Control control in Controls)
                    if (control.Visible && !(control is LiteralControl))
                        return true;

                return false;
            }
        }

        /// <summary>
        /// HTML-разметка перед заголовком
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public string BeforeLabelHtml
        {
            get { return _beforeLabelHtml ?? string.Empty; }
            set { _beforeLabelHtml = value; }
        }

        /// <summary>
        /// HTML-разметка перед контролом
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public string BeforeControlHtml
        {
            get { return _beforeControlHtml ?? string.Empty; }
            set { _beforeControlHtml = value; }
        }

        /// <summary>
        /// HTML-разметка между контролами
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public string AfterControlHtml
        {
            get { return _afterControlHtml ?? string.Empty; }
            set { _afterControlHtml = value; }
        }

        /// <summary>
        /// HTML-разметка после контрола
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public string SeparatorHtml
        {
            get { return _separatorHtml ?? string.Empty; }
            set { _separatorHtml = value; }
        }

        /// <summary>
        /// Шаблонные контролы
        /// </summary>
        [TemplateContainer(typeof(UserControl)), PersistenceMode(PersistenceMode.InnerProperty), TemplateInstance(TemplateInstance.Single)]
        public ITemplate TemplateControls { get; set; }

        #endregion

        #region Приватные методы

        protected override void CreateChildControls()
        {
            if (TemplateControls != null)
                TemplateControls.InstantiateIn(this);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (Visible)
            {
                EnsureChildControls();
                if (HasVisibleChildControls)
                {
                    bool flag = true;
                    bool hasLabels = BeforeLabelHtml.NotEmpty();
                    foreach (Control control in Controls)
                    {
                        if (!control.Visible || (control is LiteralControl))
                            continue;

                        if (control is IRepeatedControlsContainer)
                        {
                            control.RenderControl(writer);
                            flag = true;
                            continue;
                        }

                        if (!flag)
                            writer.Write(HttpUtility.HtmlDecode(SeparatorHtml));
                        else
                            flag = false;

                        Label lbl;
                        if (hasLabels && (lbl = (control as Label)) != null)
                        {
                            writer.Write(HttpUtility.HtmlDecode(BeforeLabelHtml));
                            writer.Write(HttpUtility.HtmlDecode(lbl.Text));
                            flag = true;
                        }
                        else
                        {
                            writer.Write(HttpUtility.HtmlDecode(BeforeControlHtml));
                            control.RenderControl(writer);
                            writer.Write(HttpUtility.HtmlDecode(AfterControlHtml));
                        }
                    }
                }
            }
        }

        #endregion

        #region Обработчики событий

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            EnsureChildControls();
        }

        #endregion
    }
}