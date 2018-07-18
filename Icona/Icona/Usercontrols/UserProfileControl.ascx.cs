using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elar.Framework.Core.Enums;
using Elar.Framework.Core.Extensions;
using Elar.Framework.Core.Helpers;
using Icona.Logic.Entities;
using Icona.Logic.Extensions;

namespace Icona.Usercontrols
{
    public partial class UserProfileControl : BaseControl
    {
        #region  Свойства

        /// <summary>
        /// Только для чтения
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public UserProfile UserProfile { get; set; }

        #endregion

        #region События

        /// <summary>
        /// Событие сохранения
        /// </summary>
        public event EventHandler Saved;

        #endregion

        #region Методы

        /// <summary>
        /// Показать контрол
        /// </summary>
        public void Show()
        {
            if (UserProfile != null)
                Page.Script(string.Format("showUserDetails('{0}');", string.Format("Анкета пользователя {0} ({1})", UserProfile.FIO, UserProfile.Login)));
            else
                Page.Script("showUserDetails();");
        }

        /// <summary>
        /// Скрыть контрол
        /// </summary>
        public void Close()
        {
            UserProfile = null;
            Page.Script("closeUserDetails();");
        }

        /// <summary>
        /// Установка состояния для чтения
        /// </summary>
        public void SetReadOnly()
        {
            PasswordTxt.ReadOnly =
            EmailTxt.ReadOnly =
            LastNameTxt.ReadOnly =
            FirstNameTxt.ReadOnly =
            MiddleNameTxt.ReadOnly = ReadOnly;

            GenderDdl.Enabled =
            PasswordRequiredValidator.Enabled =
            PasswordDiv.Visible =
            SaveUserBtn.Visible = !ReadOnly;
        }

        /// <summary>
        /// Заполнение списков
        /// </summary>
        private void FillLists()
        {
            GenderDdl.DataSource = EnumNamesHelper.EnumToList<Genders>();
            GenderDdl.DataTextField = "Text";
            GenderDdl.DataValueField = "Value";
            GenderDdl.DataBind();
        }

        /// <summary>
        /// Заполнение данных пользователя
        /// </summary>
        /// <param name="id">Идентификатор профиля пользователя</param>
        public void FillData(int? id)
        {
            ClearUserDetails();
            SetReadOnly();

            if (id.HasValue)
            {
                UserProfile profile = UserProfile.Get(id.Value);
                UserProfile = profile;

                MembershipUser user = Membership.GetUser(profile.UserId);

                if (user != null)
                {
                    LoginTxt.Text = user.UserName;

                    LoginTxt.ReadOnly = true;
                    EmailTxt.Text = user.Email;

                    LastNameTxt.Text = profile.LastName;
                    FirstNameTxt.Text = profile.FirstName;
                    MiddleNameTxt.Text = profile.MiddleName;
                    GenderDdl.SelectedValue = profile.GenderId.ToStr("0");

                    PasswordRequiredValidator.Enabled = false;

                    SaveUserBtn.CommandArgument = id.ToString();
                    SaveUserBtn.CommandName = "update";
                }
            }
            else
            {
                LoginTxt.ReadOnly = false;

                LoginTxt.Text =
                EmailTxt.Text =
                LastNameTxt.Text =
                FirstNameTxt.Text =
                MiddleNameTxt.Text = string.Empty;
                GenderDdl.SelectedValue = "0";

                PasswordRequiredValidator.Enabled = true;
                SaveUserBtn.CommandName = "add";
            }
        }

        /// <summary>
        /// Очистка данных пользователя
        /// </summary>
        public void ClearUserDetails()
        {
            LastNameTxt.Text =
            FirstNameTxt.Text =
            MiddleNameTxt.Text =
            LoginTxt.Text =
            PasswordTxt.Text =
            EmailTxt.Text = string.Empty;
            GenderDdl.SelectedValue = "0";
        }

        /// <summary>
        /// Сохранение данных пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        public void SaveUserDetails(Guid userId)
        {
            MembershipUser user = Membership.GetUser(userId);

            if (user != null)
            {

                if (StringExtensions.NotEmpty(PasswordTxt.Text))
                    user.ChangePassword(user.GetPassword(), PasswordTxt.Text);                

                user.Email = EmailTxt.Text;
                Membership.UpdateUser(user);

                UserProfile userProfile;

                if (UserProfile.Exists(userId))
                {
                    userProfile = UserProfile.GetByUserId(userId);

                    userProfile.FirstName = FirstNameTxt.Text.Trim();
                    userProfile.LastName = LastNameTxt.Text.Trim();
                    userProfile.MiddleName = MiddleNameTxt.Text.Trim();
                    userProfile.GenderId = ConvertExtentions.ToInt32(GenderDdl.SelectedValue).NullIf((int)Genders.Unknown);

                    userProfile.Update();
                }
                else
                {
                    userProfile = new UserProfile
                    {
                        UserId = userId,
                        FirstName = FirstNameTxt.Text.Trim(),
                        LastName = LastNameTxt.Text.Trim(),
                        MiddleName = MiddleNameTxt.Text.Trim(),
                        GenderId = ConvertExtentions.ToInt32(GenderDdl.SelectedValue).NullIf((int)Genders.Unknown),
                    };
                    userProfile.Id = UserProfile.Add(userProfile);
                }
            }
        }

        #endregion

        #region Обработчики событий

        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorMessageLbl.Text = string.Empty;

            if (!IsPostBack)
                FillLists();
        }

        protected void UsersProfile_OnCommand(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                #region add

                case "add":
                    MembershipCreateStatus status;
                    MembershipUser user = Membership.CreateUser( LoginTxt.Text,
                                                                 PasswordTxt.Text,
                                                                 EmailTxt.Text,
                                                                 null,
                                                                 null,
                                                                 true,
                                                                 out status);

                    if (status != MembershipCreateStatus.Success || user == null)
                    {
                        switch (status)
                        {
                            case MembershipCreateStatus.DuplicateUserName:
                                ErrorMessageLbl.Text = "Пользователь с таким именем уже существует";
                                ErrorMessageLbl.Visible = true;
                                break;

                            case MembershipCreateStatus.DuplicateEmail:
                                ErrorMessageLbl.Text = "Пользователь с такой почтой уже существует";
                                ErrorMessageLbl.Visible = true;
                                break;

                            default:
                                ErrorMessageLbl.Text = "Произошла ошибка";
                                ErrorMessageLbl.Visible = true;
                                break;
                        }
                    }
                    else
                    {
                        SaveUserDetails(user.GetIdentity());
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "popup", "closeUserDetails();", true);
                    }

                    break;

                #endregion

                #region update

                case "update":
                    try
                    {
                        UserProfile profile = UserProfile.Get(Convert.ToInt32(e.CommandArgument.ToString()));
                        SaveUserDetails(profile.UserId);
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "popup", "closeUserDetails();", true);
                    }
                    catch (Exception exc)
                    {
                        Page.StandardErrorScript();
                    }
                    break;

                    #endregion
            }

            if (Saved != null)
                Saved(this, e);
        }

        #endregion
    }

}