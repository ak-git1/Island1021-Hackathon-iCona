<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenuControl.ascx.cs" Inherits="Icona.Usercontrols.Menu.TopMenuControl" %>

<telerik:RadMenu ID="RadMenu" DataSourceID="SiteMap" ClickToOpen="True" EnableEmbeddedSkins="False" ExpandDelay="500" CollapseDelay="1000" OnItemDataBound="RadMenu_OnItemDataBound" runat="server">
    <ExpandAnimation Type="None" Duration="300" />
    <CollapseAnimation Type="None" Duration="300" />
</telerik:RadMenu>
<asp:SiteMapDataSource ID="SiteMap" runat="server" ShowStartingNode="false" />