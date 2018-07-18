<%@ Page Title="iCona - Internet Communities News Aggregator" Language="C#" MasterPageFile="~/Masterpages/Blank.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Icona.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Styles" runat="server">
    <link href="/Content/login.css" rel="Stylesheet" type="text/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="loginBar">
        <asp:UpdatePanel ID="LoginUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="loginBorder">
                    <img id="logoImg" src="Images/login_logo.png" />
                    <asp:Login ID="LoginPnl" CssClass="login-container" runat="server" DestinationPageUrl="~/Pages/Main.aspx"
                        OnLoginError="LoginPnl_LoginError" OnLoggingIn="LoginPnl_LoggingIn" OnLoggedIn="LoginPnl_LoggedIn">
                        <LayoutTemplate>
                            <table cellpadding="2" cellspacing="1">
                                <tr>
                                    <td colspan="2" class="c">
                                        <asp:Label ID="TitleText" runat="server" Text="Вход в систему"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="l">Логин
                                    </td>
                                    <td>
                                        <asp:TextBox ID="UserName" runat="server" CssClass="all-textbox" Width="100%"/>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" Text="*" Display="None" ErrorMessage="Укажите Ваш логин" ValidationGroup="LoginValidationGroup"/>
                                        <asp:CustomValidator SetFocusOnError="true" Enabled="True" ValidateEmptyText="false" ClientValidationFunction="checkLogin" ID="UserNameFieldValidator" runat="server" ControlToValidate="UserName" Display="None" ErrorMessage="Символ ',' является недопустимым." ValidationGroup="LoginValidationGroup" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="UserNameRequired" PopupPosition="Right"/>                                
                                        <ajaxToolkit:ValidatorCalloutExtender ID="UserNameFieldValidator_ValidatorCalloutExtender" PopupPosition="Right" runat="server" Enabled="True" TargetControlID="UserNameFieldValidator" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="l">Пароль&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Password" runat="server" CssClass="all-textbox" TextMode="Password" Width="100%"/>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" Display="None" Text="*" ErrorMessage="Укажите Ваш пароль" ValidationGroup="LoginValidationGroup"/>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="PasswordRequired" PopupPosition="Right"/>                                
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td class="l">
                                        <asp:Button ID="Login" runat="server" Text="Войти" CommandName="Login" CssClass="button display-inline" Width="150px" ValidationGroup="LoginValidationGroup" />
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                    </asp:Login>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>