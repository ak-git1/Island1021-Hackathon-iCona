<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SmallImageButtonControl.ascx.cs" Inherits="Icona.Usercontrols.SmallImageButtonControl" %>

<asp:LinkButton runat="server" ID="LinkBtn" CssClass="top-bottom-margin ui-button ui-widget ui-state-default ui-corner-all " CausesValidation="False">
    <asp:HiddenField ID="DestinationUrlHf" runat="server" />
    <asp:HiddenField ID="ReturnUrlHf" runat="server" />
    <span class="ui-button-badge" id="Badge" runat="server" />
    <img alt="" class="icon" id="ImageBtn" runat="server" />
</asp:LinkButton>