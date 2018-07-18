<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommunityControl.ascx.cs" Inherits="Icona.Usercontrols.CommunityControl" %>

<asp:Panel ID="CommunityPanel" runat="server" class="popup">
    <asp:UpdatePanel runat="server" ID="CommunityUPnl" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div class="table bordered-control">
                <div class="row">
                    <div class="cell1">
                        Название
                    </div>
                    <div class="cell2">
                        <asp:TextBox runat="server" CssClass="all-textbox wide" ID="NameTxt" />
                        <asp:CustomValidator ID="NameTxtCustomValidator" runat="server" ValidateEmptyText="true" Enabled="true" ValidationGroup="CommunityDetails" ClientValidationFunction="checkAndPaint" ControlToValidate="NameTxt" Display="None" ErrorMessage="Название не может быть пустым">*</asp:CustomValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="NameTxtValidatorCalloutExtender" PopupPosition="Left" runat="server" Enabled="True" TargetControlID="NameTxtCustomValidator" />
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="row">
                    <div class="cell1">
                    </div>
                    <div class="cell2">
                        <asp:Button ID="SaveBtn" runat="server" CssClass="button" Text="Сохранить" OnClick="SaveBtnClick" ValidationGroup="CommunityDetails"/>
                        <span onclick="closeDialog('<%=CommunityPanel.ClientID %>');">
                            <input id="CancelBtn" type="button" class="button" value="Отмена" />
                        </span>
                    </div>
                    <div class="clear">
                    </div>
                </div>                                          
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>