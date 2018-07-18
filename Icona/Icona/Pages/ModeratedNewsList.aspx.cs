using System;
using System.Web.UI.WebControls;
using Elar.Framework.Core.Extensions;
using Icona.Logic.Entities;
using Icona.Logic.Enums;
using Icona.Logic.Extensions;
using Icona.Logic.Filters;
using Icona.Logic.UI;
using Icona.Usercontrols;

namespace Icona.Pages
{
    public partial class ModeratedNewsList : BaseFilteredPage
    {
        /// <summary>
        /// Фильтр страницы
        /// </summary>
        private NewsItemsListFilter PageFilter =>
            new NewsItemsListFilter
            {
                Title = FilterNameTxt.Text,
                DateFrom = FilterDateFromTxt.Text.ToDateTime(null),
                DateTill = FilterDateTillTxt.Text.ToDateTime(null),
                State = null,
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

        /// <summary>
        /// Очистка фильтра
        /// </summary>
        protected void ClearFilter()
        {
            FilterNameTxt.Text = string.Empty;
        }

        private void FillGrid()
        {
            ListEx<NewsItem> newsItems = NewsItem.GetList(PageFilter);
            int totalRecords = newsItems.TotalRecords;
            ToolsContainerControl.CheckForceFilterShow(totalRecords);
            PagingControl.SetTotalRecords(totalRecords);
            NewsItemsGrid.DataSource = newsItems;
            NewsItemsGrid.DataBind();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                FillGrid();
        }

        protected void Reload(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void ClearFilterBtn_OnClick(object sender, EventArgs e)
        {
            ClearFilter();
            FillGrid();
        }

        protected void ApplyFilterBtn_OnClick(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void NewsItemsGrid_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                NewsItem newsItem = (NewsItem)e.Row.DataItem;
                if (newsItem != null)
                {
                    SmallImageButtonControl btn = e.Row.FindControl("PublishBtn") as SmallImageButtonControl;
                    if (btn != null)
                        btn.Visible = newsItem.State == NewsItemsStates.New;

                    btn = e.Row.FindControl("DeclineBtn") as SmallImageButtonControl;
                    if (btn != null)
                        btn.Visible = newsItem.State == NewsItemsStates.New;

                    btn = e.Row.FindControl("DeleteButton") as SmallImageButtonControl;
                    if (btn != null)
                        btn.Visible = newsItem.State == NewsItemsStates.New;
                }
            }
        }

        protected void NewsItemsGrid_OnCommand(object sender, CommandEventArgs e)
        {
            Guid nodeId = e.CommandArgument.ToGuid();

            switch (e.CommandName)
            {
                #region publish

                case "publish":
                    try
                    {
                        NewsItem.UpdateState(nodeId, NewsItemsStates.Published);
                    }
                    catch (Exception exc)
                    {
                        this.StandardErrorScript();
                    }
                    break;

                #endregion

                #region decline

                case "decline":
                    try
                    {
                        NewsItem.UpdateState(nodeId, NewsItemsStates.Declined);
                    }
                    catch (Exception exc)
                    {
                        this.StandardErrorScript();
                    }
                    break;

                #endregion  

                #region delete-item

                case "delete-item":
                    try
                    {
                        NewsItem.Delete(nodeId);
                        FillGrid();
                    }
                    catch (Exception exc)
                    {
                        this.StandardErrorScript();
                    }
                    break;

                #endregion
            }
        }
    }
}