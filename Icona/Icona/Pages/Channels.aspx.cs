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
    public partial class Channels : BaseFilteredPage
    {
        public int? CommunityId
        {
            get => ViewState["CommunityId"].ToInt32(null);
            set => ViewState.Add("CommunityId", value);
        }

        /// <summary>
        /// Фильтр страницы
        /// </summary>
        private ChannelsFilter PageFilter =>
            new ChannelsFilter
            {
                CommunityId = CommunityId.Value,
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

        /// <summary>
        /// Заполнения списка пользователей
        /// </summary>
        private void FillGrid()
        {
            ListEx<Channel> channels = Channel.GetList(PageFilter);
            int totalRecords = channels.TotalRecords;
            ToolsContainerControl.CheckForceFilterShow(totalRecords);
            PagingControl.SetTotalRecords(totalRecords);
            ChannelsGrid.DataSource = channels;
            ChannelsGrid.DataBind();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                if (Request["CommunityId"] != null)
                {
                    CommunityId = Request["CommunityId"].ToInt32(null);
                    Community community = Community.Get(CommunityId.Value);
                    HeaderLbl.Text = $"Каналы сообщества: '{community.Name}'";
                    FillGrid();
                }
                else
                {
                    Response.Redirect("~/Pages/Main.aspx", false);
                    Response.End();
                }
            }
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

        protected void AddChannelBtn_OnClick(object sender, EventArgs e)
        {
            try
            {
                ChannelControl.Show(null, CommunityId.Value);
            }
            catch (Exception exc)
            {
                this.StandardErrorScript();
            }
        }

        protected void ChannelControl_OnSaved(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void ChannelsGrid_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void ChannelsGrid_OnCommand(object sender, CommandEventArgs e)
        {
            Channel channel;
            int nodeId = e.CommandArgument.ToInt32();

            switch (e.CommandName)
            {
                #region get

                case "get":
                    try
                    {
                        ChannelControl.Show(nodeId);
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
                        channel = Channel.Get(nodeId);
                        channel.Delete();
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