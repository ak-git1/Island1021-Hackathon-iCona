<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebMessageBox.ascx.cs" Inherits="Icona.Usercontrols.WebMessageBox" %>

<script src="/Scripts/controls/WebMessageBox.js" type="text/javascript"></script>
<link href="/Content/controls/WebMessageBox.css" rel="Stylesheet" type="text/css" />  

<asp:Panel ID="WebMessageBoxPanel" runat="server" CssClass="popup">
    <asp:UpdatePanel ID="WebMessageBoxUpdPnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="webMessageBox-space">
                <div class="webMessageBox-ico">
                    <asp:Image ID="WebMessageBoxIcon" runat="server" ImageUrl="~/Images/webMessageBox/info.png"/>
                </div>
                <div runat="server" class="webMessageBox-txt">
                    <asp:Label ID="WebMessageBoxTextlbl" runat="server" Text="Label"/>
                </div>
                <div class="webMessageBox-spacer"></div>
                <div class="webMessageBox-buttonBox">
                    <span onclick="closeDialog('<%= WebMessageBoxPanel.ClientID %>');">
                        <asp:Button ID="YesBtn" runat="server" Text="Да" OnClick="YesBtn_Click" UseSubmitBehavior="true" CssClass="button webMessageBox-button" CausesValidation="False" />
                    </span>
                    <span onclick="closeDialog('<%= WebMessageBoxPanel.ClientID %>');">
                        <asp:Button ID="NoBtn" runat="server" Text="Нет" OnClick="NoBtn_Click" UseSubmitBehavior="true" CssClass="button webMessageBox-button" CausesValidation="False"  />
                    </span>
                    <span onclick="closeDialog('<%= WebMessageBoxPanel.ClientID %>');">
                        <asp:Button ID="CancelBtn" runat="server" Text="Отмена" CssClass="button webMessageBox-button" CausesValidation="False"  />
                    </span>
                </div>
                <div class="clear"></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>

