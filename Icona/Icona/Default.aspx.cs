using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ak.Framework.Core.Extensions;
using Icona.Logic.Entities;
using Icona.Logic.Extensions;

namespace Icona
{
    public partial class Default : Page
    {
        #region Свойства

        /// <summary>
        /// Разрешить валидацию
        /// </summary>
        public bool EnableValidation
        {
            get
            {
                RequiredFieldValidator requiredControl = LoginPnl.FindControl("UserNameRequired") as RequiredFieldValidator;
                return requiredControl.Enabled;
            }
            set
            {
                RequiredFieldValidator requiredControl = LoginPnl.FindControl("UserNameRequired") as RequiredFieldValidator;
                requiredControl.Enabled = value;
                requiredControl = LoginPnl.FindControl("PasswordRequired") as RequiredFieldValidator;
                requiredControl.Enabled = value;
            }
        }

        /// <summary>
        /// Логин
        /// </summary>
        private string _userName;

        /// <summary>
        /// Логин
        /// </summary>
        public string UserName => _userName ?? (_userName = LoginPnl.UserName.Trim());

        #endregion

        #region Приватные методы

        /// <summary>
        /// Аутентификация
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        private bool Authetificate(string login, string password)
        {
            bool result = true;

            if (Membership.ValidateUser(login, password))
            {
                UserProfile userProfile = UserProfile.GetByUserId(Membership.GetUser(login).GetIdentity());

                if (userProfile.Blocked)
                {
                    Label errorLbl = LoginPnl.FindControl("TitleText") as Label;
                    errorLbl.CssClass = "error";
                    errorLbl.Text = "Данный пользователь заблокирован.";
                    result = false;
                }

                if (userProfile.Deleted)
                {
                    Label errorLbl = LoginPnl.FindControl("TitleText") as Label;
                    errorLbl.CssClass = "error";
                    errorLbl.Text = "Данный пользователь удален из системы.";

                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Завершение успешной аутентификации
        /// </summary>
        /// <param name="login">Логин</param>
        private void CompleteSuccessfulAuthetification(string login)
        {
            FormsAuthentication.SetAuthCookie(login, false);

            string redirectUrl = Request["ReturnUrl"];
            Response.Redirect(redirectUrl.NotEmpty() && redirectUrl.IndexOf(Request.CurrentExecutionFilePath, StringComparison.InvariantCultureIgnoreCase) < 0 ? redirectUrl : FormsAuthentication.DefaultUrl);
        }

        #endregion

        #region События

        protected void LoginPnl_LoginError(object sender, EventArgs e)
        {
            Label errorLbl = LoginPnl.FindControl("TitleText") as Label;
            errorLbl.CssClass = "error";
            LoginPnl.FailureText = string.Empty;

            MembershipUser user = Membership.GetUser(UserName);
            if (user != null)
            {
                if (user.IsLockedOut)
                {
                    errorLbl.Text = "Ваша учетная запись заблокирована. Обратитесь в службу тех. поддержки...";
                }
                else if (!Membership.ValidateUser(user.UserName, LoginPnl.Password))
                {
                    errorLbl.Text = "Введен неверный пароль. Попробуйте снова...";
                }
            }
            else
            {
                errorLbl.Text = "Пользователь с указанным логином не найден...";
            }
        }

        protected void LoginPnl_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            e.Cancel = !Authetificate(UserName, LoginPnl.Password);
        }

        protected void LoginPnl_LoggedIn(object sender, EventArgs e)
        {
            CompleteSuccessfulAuthetification(UserName);
        }

        #endregion
    }
}