<%@ Page Title="Главное меню" Language="C#" MasterPageFile="~/Masterpages/Menu.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Icona.Pages.Main" %>

<%@ Register TagName="MainMenuControl" Src="~/Usercontrols/Menu/MainMenuControl.ascx" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderStylePlaceHolder" runat="server" Visible="False"/>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContentPlaceHolder" runat="server">
    <uc1:MainMenuControl runat="server" ID="MainMenuPnl" SiteMapProvider="XmlSiteMapProvider" StartingNodeUrl="~/Pages/Main.aspx" ImageSizes="48x48"/>
</asp:Content>