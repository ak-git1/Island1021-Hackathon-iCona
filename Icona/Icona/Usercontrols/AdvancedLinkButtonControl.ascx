<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvancedLinkButtonControl.ascx.cs" Inherits="Icona.Usercontrols.AdvancedLinkButtonControl" %>

<asp:HiddenField ID="DestinationUrlHf" runat="server" />
<asp:HiddenField ID="ReturnUrlHf" runat="server" />
<div class="icon" runat="server" ID="ImageDiv">
    <img id="Image" runat="server" />
</div>
<asp:LinkButton ID="LinkBtn" runat="server">        
    <span id="LinkBtnName" runat="server" class="<%#CssClass%>"></span>
</asp:LinkButton>