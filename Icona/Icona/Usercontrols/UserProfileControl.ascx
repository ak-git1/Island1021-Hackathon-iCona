<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserProfileControl.ascx.cs" Inherits="Icona.Usercontrols.UserProfileControl" %>

<script type="text/javascript">
    $(function () {
        var dlg = $('#userDetailsPnl').dialog({ autoOpen: false, width: 820, resizable: false, draggable: true, modal: true });
        dlg.parent().appendTo($('form:first'));
    });

    function showUserDetails(title) {
        disableEnterKey('userDetailsPnl');
        $('#userDetailsPnl').dialog('open');

        if (title != null)
            $("#userDetailsPnl").dialog("option", "title", title);
        else
            $("#userDetailsPnl").dialog("option", "title", 'Анкета пользователя');
    }

    function closeUserDetails() {
        $('#userDetailsPnl').dialog('close');
        return false;
    }
</script>

<div id="userDetailsPnl" title="Анкета пользователя" class="popup">
    <asp:UpdatePanel runat="server" ID="UserDetailsUPnl">
        <ContentTemplate>
            <div class="table" style="margin: 3px auto;">
                <div class="row">
                    <div class="cell1">
                        Фамилия
                    </div>
                    <div class="cell2">
                        <asp:TextBox runat="server" ID="LastNameTxt" CssClass="textbox" ValidationGroup="UserDetails"/>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True" TargetControlID="LastNameTxt" InvalidChars="0123456789.,<>+_)(*&^%$#@![]{};:/?=" FilterMode="InvalidChars"/>
                        <asp:RequiredFieldValidator ID="LastNameTxtRequiredFieldValidator" runat="server" ControlToValidate="LastNameTxt"
                                                    CssClass="error" ValidationGroup="UserDetails" Display="None" ErrorMessage="Необходимо указать фамилию"/>
                        <ajaxToolkit:ValidatorCalloutExtender ID="LastNameTxtValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="LastNameTxtRequiredFieldValidator" PopupPosition="Left"/>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="row">
                    <div class="cell1">
                        Имя
                    </div>
                    <div class="cell2">
                        <asp:TextBox runat="server" ID="FirstNameTxt" CssClass="textbox" ValidationGroup="UserDetails"/>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="FirstNameTxt" InvalidChars="0123456789.,<>+_)(*&^%$#@![]{};:/?-=" FilterMode="InvalidChars"/>
                        <asp:RequiredFieldValidator ID="FirstNameTxtRequiredFieldValidator" runat="server" ControlToValidate="FirstNameTxt"
                                                    CssClass="error" ValidationGroup="UserDetails" Display="None" ErrorMessage="Необходимо указать имя"/>
                        <ajaxToolkit:ValidatorCalloutExtender ID="FirstNameTxtValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="FirstNameTxtRequiredFieldValidator" PopupPosition="Left"/>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="row">
                    <div class="cell1">
                        Отчество
                    </div>
                    <div class="cell2">
                        <asp:TextBox runat="server" ID="MiddleNameTxt" CssClass="textbox" ValidationGroup="UserDetails"/>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" TargetControlID="MiddleNameTxt" InvalidChars="0123456789.,<>+_)(*&^%$#@![]{};:/?-=" FilterMode="InvalidChars"/>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="row" id="GenderDiv" runat="server">
                    <div class="cell1">
                        Пол
                    </div>
                    <div class="cell2">
                        <asp:DropDownList ID="GenderDdl" Width="100px" runat="server"/>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="row">
                    <div class="cell1">
                        Логин <span class="required">*</span>
                    </div>
                    <div class="cell2">
                        <asp:TextBox runat="server" ID="LoginTxt" CssClass="textbox" ValidationGroup="UserDetails"/>
                        <asp:RequiredFieldValidator ID="LoginRequiredValidator" runat="server" ControlToValidate="LoginTxt"
                                                    CssClass="error" ValidationGroup="UserDetails" Display="None" ErrorMessage="Необходимо ввести логин"/>
                        <ajaxToolkit:ValidatorCalloutExtender ID="LoginRequiredValidator_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="LoginRequiredValidator" PopupPosition="BottomLeft"/>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="row" runat="server" id="PasswordDiv">
                    <div class="cell1">
                        Пароль <span class="required">*</span>
                    </div>
                    <div class="cell2">
                        <asp:TextBox runat="server" ID="PasswordTxt" CssClass="textbox" ValidationGroup="UserDetails" TextMode="Password"/>
                        <asp:RequiredFieldValidator ID="PasswordRequiredValidator" runat="server" ControlToValidate="PasswordTxt"
                                                    CssClass="error" ValidationGroup="UserDetails" Display="None" ErrorMessage="Необходимо ввести пароль"/>
                        <ajaxToolkit:ValidatorCalloutExtender ID="PasswordRequiredValidator_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="PasswordRequiredValidator" PopupPosition="BottomLeft"/>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="row">
                    <div class="cell1">
                        Email <span class="required">*</span>
                    </div>
                    <div class="cell2">
                        <asp:TextBox runat="server" ID="EmailTxt" CssClass="textbox" ValidationGroup="UserDetails"/>
                        <asp:RequiredFieldValidator ID="EmailTxtRequiredFieldValidator" runat="server" ControlToValidate="EmailTxt"
                                                    CssClass="error" ValidationGroup="UserDetails" ErrorMessage="Необходимо ввести email" Display="None"/>
                        <asp:RegularExpressionValidator ID="EmailTxtRegularExpressionValidator" runat="server" ControlToValidate="EmailTxt"
                                                        ValidationGroup="UserDetails" CssClass="error"
                                                        ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                                        Display="None"/>
                        <ajaxToolkit:ValidatorCalloutExtender ID="EmailTxtRequiredFieldValidator_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="EmailTxtRequiredFieldValidator" PopupPosition="TopLeft"/>
                        <ajaxToolkit:ValidatorCalloutExtender ID="EmailTxtRegularExpressionValidator_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="EmailTxtRegularExpressionValidator" PopupPosition="TopLeft"/>

                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="row">
                    <asp:Label runat="server" ID="ErrorMessageLbl" CssClass="error" Text="&nbsp;"/>
                </div>
            </div>
            <div style="text-align: right; margin-top: 5px; padding-right: 6px;">
                <asp:Button runat="server" ID="SaveUserBtn" CssClass="button" Text="Сохранить" OnCommand="UsersProfile_OnCommand" ValidationGroup="UserDetails"/>
                <input type="button" class="button" value="Отмена" onclick="closeUserDetails();"/>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>