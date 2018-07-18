<%@ Page Title="Администрирование" Language="C#" MasterPageFile="~/Masterpages/Menu.Master" AutoEventWireup="true" CodeBehind="Administration.aspx.cs" Inherits="Icona.Pages.Administration.Administration" %>

<%@ Register Src="~/Usercontrols/Menu/MainMenuControl.ascx" TagName="MainMenuControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    Администрирование
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MenuContentPlaceHolder">
    <uc1:MainMenuControl runat="server" ID="MainMenuPnl" SiteMapProvider="XmlSiteMapProvider" StartingNodeUrl="~/Pages/Administration/Administration.aspx" ImageSizes="48x48" />
</asp:Content>