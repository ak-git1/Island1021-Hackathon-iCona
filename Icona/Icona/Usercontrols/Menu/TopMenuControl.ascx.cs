using System;
using System.Web.UI;
using Ak.Framework.Core.Extensions;
using Telerik.Web.UI;

namespace Icona.Usercontrols.Menu
{
    public partial class TopMenuControl : UserControl
    {
        #region Свойства

        /// <summary>
        /// Провайдер карты сайта 
        /// </summary>
        public string SiteMapProvider { get; set; }

        /// <summary>
        /// Размеры изображений (в формате 00x00, например, 48x48)
        /// </summary>
        public string ImageSizes { get; set; }

        #endregion

        #region Обработчики событий

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SiteMapProvider))
                SiteMap.SiteMapProvider = SiteMapProvider;
        }

        protected void RadMenu_OnItemDataBound(object sender, RadMenuEventArgs e)
        {
            System.Web.SiteMapNode node = (System.Web.SiteMapNode)e.Item.DataItem;
            string imageUrl;
            bool root = e.Item.Parent is RadMenu;
            if (node != null && (imageUrl = node["icon"]).NotEmpty())
                e.Item.ImageUrl = root ? string.Empty : string.Format(imageUrl, ImageSizes);

            if (root && node != null && node.ChildNodes.Count > 0)
                e.Item.NavigateUrl = null;

            if (root && e.Item.Index != 0)
            {
                RadMenuItem separator = new RadMenuItem();
                separator.IsSeparator = true;
                e.Item.Owner.Items.Insert(e.Item.Index, separator);
            }
        }

        #endregion
    }
}