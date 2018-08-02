using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ak.Framework.Core.Extensions;
using Icona.Logic.Entities;
using Icona.Logic.Extensions;
using Icona.Logic.Filters;
using Icona.Logic.UI;
using Icona.Usercontrols;

namespace Icona.Pages.Administration
{
    public partial class UsersRegistry : BaseFilteredPage
    {
        #region Переменные и константы

        /// <summary>
        /// Шаблон строки о количестве пользователей в системе
        /// </summary>
        private const string UsersNumberTemplate = "Кол-во пользователей в системе: {0} из {1}";

        #endregion

        #region Свойства

        /// <summary>
        /// Фильтр страницы
        /// </summary>
        private UsersRegistryFilter PageFilter =>
            new UsersRegistryFilter
            {
                Email = FilterEmailTxt.Text,
                IsBlocked = FilterBlockedDdl.SelectedIndex > 0 ? FilterBlockedDdl.SelectedIndex == 1 : (bool?)null,
                LastEnterDateFrom = FilterLastEnterDateFromTxt.Text.ToDateTime(null),
                LastEnterDateTill = FilterLastEnterDateTillTxt.Text.ToDateTime(null),
                Login = FilterLoginTxt.Text,
                FIO = FilterNameTxt.Text,
                RegistrationDateFrom = FilterRegistrationDateFromTxt.Text.ToDateTime(null),
                RegistrationDateTill = FilterRegistrationDateTillTxt.Text.ToDateTime(null),
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

        #endregion

        #region Методы

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
            FilterBlockedDdl.SelectedIndex = 0;

            FilterNameTxt.Text =
            FilterLoginTxt.Text =
            FilterEmailTxt.Text =
            FilterRegistrationDateFromTxt.Text =
            FilterRegistrationDateTillTxt.Text =
            FilterLastEnterDateFromTxt.Text =
            FilterLastEnterDateTillTxt.Text = string.Empty;
        }

        /// <summary>
        /// Заполнения списка пользователей
        /// </summary>
        private void FillGrid()
        {
            ListEx<UserProfile> users = UserProfile.GetList(PageFilter);
            int totalRecords = users.TotalRecords;
            ToolsContainerControl.CheckForceFilterShow(totalRecords);
            PagingControl.SetTotalRecords(totalRecords);
            UsersGrid.DataSource = users;
            UsersGrid.DataBind();
            UsersNumberLbl.Text = String.Format(UsersNumberTemplate, Membership.GetNumberOfUsersOnline(), totalRecords);
        }

        #endregion

        #region Обработчики событий

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                FillGrid();
        }

        protected void UsersGridRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                UserProfile user = (UserProfile)e.Row.DataItem;
                if (user != null)
                {
                    MembershipUser membershipUser = Membership.GetUser(user.UserId);

                    Label registerDateLbl = e.Row.FindControl("RegisterDateLbl") as Label;
                    if (registerDateLbl != null)
                        if (membershipUser != null)
                            registerDateLbl.Text = membershipUser.CreationDate.ToString("dd.MM.yyyy HH:mm");

                    Label lastLoginDateLbl = e.Row.FindControl("LastLoginDateLbl") as Label;
                    if (lastLoginDateLbl != null)
                        if (membershipUser != null)
                            lastLoginDateLbl.Text = membershipUser.LastLoginDate.ToString("dd.MM.yyyy HH:mm");

                    SmallImageButtonControl btn;
                    btn = e.Row.FindControl("EditUserBtn") as SmallImageButtonControl;
                    if (btn != null)
                    {
                        btn.Visible = true;
                        btn.ToolTip = "Редактировать";
                    }

                    btn = e.Row.FindControl("BlockUserBtn") as SmallImageButtonControl;
                    if (btn != null)
                        btn.Visible = !user.Blocked;

                    btn = e.Row.FindControl("UnblockUserBtn") as SmallImageButtonControl;
                    if (btn != null)
                        btn.Visible = user.Blocked;
                }
            }
        }

        protected void ApplyFilterBtn_OnClick(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void ClearFilterBtn_OnClick(object sender, EventArgs e)
        {
            ClearFilter();
            FillGrid();
        }

        protected void UsersGrid_OnCommand(object sender, CommandEventArgs e)
        {
            UserProfile profile;
            int nodeId = e.CommandArgument.ToInt32();

            switch (e.CommandName)
            {
                #region get

                case "get":
                    try
                    {
                        UserProfilesControl.ReadOnly = false;
                        UserProfilesControl.FillData(nodeId);

                        UserProfilesControl.Show();
                    }
                    catch (Exception exc)
                    {
                        this.StandardErrorScript();
                    }
                    break;

                #endregion

                #region block

                case "block":
                    try
                    {
                        profile = UserProfile.Get(nodeId);
                        profile.Block(CurrentUserId);
                        FillGrid();
                    }
                    catch (Exception exc)
                    {
                        this.StandardErrorScript();
                    }
                    break;

                #endregion

                #region unblock

                case "unblock":
                    try
                    {
                        profile = UserProfile.Get(nodeId);
                        profile.Unblock(CurrentUserId);
                        FillGrid();
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
                        profile = UserProfile.Get(nodeId);
                        profile.Delete(CurrentUserId);
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

        protected void Reload(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void AddUserBtnClick(object sender, EventArgs e)
        {
            try
            {
                UserProfilesControl.ReadOnly = false;
                UserProfilesControl.FillData(null);
                UserProfilesControl.Show();
            }
            catch (Exception exc)
            {
                this.StandardErrorScript();
            }
        }

        protected void UserProfilesControl_OnSaved(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void PrintBtn_OnPreRender(object sender, EventArgs e)
        {
            Page.RegisterDownloadControl((Control)sender);
        }

        #endregion
    }
}