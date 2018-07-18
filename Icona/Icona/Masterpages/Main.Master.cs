using System;
using System.Web.UI.HtmlControls;

namespace Icona.Masterpages
{
    public partial class Main : System.Web.UI.MasterPage
    {
        #region Приватные методы

        /// <summary>
        /// Предотвращение кэширования
        /// </summary>
        private void PreventCaching()
        {
            Head1.Controls.Add(new HtmlMeta { Name = "Cache-Control", Content = "no-store, no-cache, must-revalidate, max-age=0, max-stale=0" });
            Head1.Controls.Add(new HtmlMeta { Name = "Cache-Control", Content = "post-check=0, pre-check=0" });
            Head1.Controls.Add(new HtmlMeta { Name = "Expires", Content = "0" });
            Head1.Controls.Add(new HtmlMeta { Name = "Last-Modified", Content = DateTime.UtcNow.ToString("r") });
        }

        #endregion

        #region Обработчики событий

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PreventCaching();
            }
        }

        protected void LoginStatus_LoggedOut(object sender, EventArgs e)
        {
            Session.Clear();
        }

        #endregion
    }
}