using System;
using System.Web.UI.WebControls;
using Icona.Logic.Entities;
using Icona.Logic.Filters;
using Icona.Logic.UI;
using Icona.Usercontrols;

namespace Icona.Pages.Administration
{
    public partial class TagsStatistics : BaseFilteredPage
    {
        /// <summary>
        /// Фильтр страницы
        /// </summary>
        private TagsStatisticsFilter PageFilter =>
            new TagsStatisticsFilter
            {
                Paging = PagingControl
            };

        /// <summary>
        /// Постраничный переход
        /// </summary>
        protected PagingControl PagingControl
        {
            get
            {
                EnsureChildControls();
                return ToolsContainerControl.Paging;
            }
        }

        /// <summary>
        /// Заполнение списков фильтров
        /// </summary>
        protected override void FillFilterLists()
        {
        }

        private void FillGrid()
        {
            ListEx<TagsStatisticsItem> newsItems = TagsStatisticsItem.GetList(PageFilter);
            int totalRecords = newsItems.TotalRecords;
            ToolsContainerControl.CheckForceFilterShow(totalRecords);
            PagingControl.SetTotalRecords(totalRecords);
            TagsStatisticsGrid.DataSource = newsItems;
            TagsStatisticsGrid.DataBind();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FillGrid();           
        }

        protected void Reload(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void TagsStatisticsGrid_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
    }
}