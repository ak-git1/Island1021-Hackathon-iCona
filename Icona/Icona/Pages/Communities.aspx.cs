using System;
using System.Web.UI.WebControls;
using Elar.Framework.Core.Extensions;
using Icona.Logic.Entities;
using Icona.Logic.Extensions;
using Icona.Logic.Filters;
using Icona.Logic.UI;
using Icona.Usercontrols;

namespace Icona.Pages
{
    public partial class Communities : BaseFilteredPage
    {
        /// <summary>
        /// Фильтр страницы
        /// </summary>
        private CommunitiesFilter PageFilter =>
            new CommunitiesFilter
            {
                Name = FilterNameTxt.Text,
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
            ListEx<Community> communities = Community.GetList(PageFilter);
            int totalRecords = communities.TotalRecords;
            ToolsContainerControl.CheckForceFilterShow(totalRecords);
            PagingControl.SetTotalRecords(totalRecords);
            CommunitiesGrid.DataSource = communities;
            CommunitiesGrid.DataBind();
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

        protected void AddCommunityBtn_OnClick(object sender, EventArgs e)
        {
            try
            {
                CommunityControl.Show();
            }
            catch (Exception exc)
            {
                this.StandardErrorScript();
            }
        }

        protected void CommunityControl_OnSaved(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void CommunitiesGrid_OnCommand(object sender, CommandEventArgs e)
        {
            Community community;
            int nodeId = e.CommandArgument.ToInt32();

            switch (e.CommandName)
            {
                #region get

                case "get":
                    try
                    {
                        CommunityControl.Show(nodeId);
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
                        community = Community.Get(nodeId);
                        community.Delete();
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

        protected void CommunitiesGrid_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SmallImageButtonControl btn = e.Row.FindControl("EditCommunityBtn") as SmallImageButtonControl;
                if (btn != null)
                {
                    btn.Visible = true;
                    btn.ToolTip = "Редактировать";
                }

                btn = e.Row.FindControl("DeleteCommunityButton") as SmallImageButtonControl;
                if (btn != null)
                    btn.Visible = true;
            }
        }
    }
}