using System;
using System.Web.UI;

namespace Icona.Usercontrols.Menu
{
    public partial class MainMenuControl : UserControl
    {
        #region Свойства

        /// <summary>
        /// Провайдер карты сайта 
        /// </summary>
        public string SiteMapProvider { get; set; }

        /// <summary>
        /// Начальный Url карты сайта
        /// </summary>
        public string StartingNodeUrl { get; set; }

        /// <summary>
        /// Размеры изображений (в формате 00x00, например, 48x48)
        /// </summary>
        public string ImageSizes { get; set; }

        #endregion

        #region События на странице

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SiteMapProvider))
            {
                SiteMap.SiteMapProvider = SiteMapProvider;
                SiteMap.StartingNodeUrl = StartingNodeUrl;
            }
        }

        #endregion
    }
}