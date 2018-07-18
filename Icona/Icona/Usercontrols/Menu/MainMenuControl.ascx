<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainMenuControl.ascx.cs" Inherits="Icona.Usercontrols.Menu.MainMenuControl" %>

<link href="/Content/main-menu.css" rel="Stylesheet" type="text/css" />
<asp:SiteMapDataSource ID="SiteMap" runat="server" ShowStartingNode="false" />
<div class="main-menu">
    <div class="row">
        <asp:Repeater runat="server" ID="menu" DataSourceID="SiteMap">
            <ItemTemplate>
                <div class="item">
                    <div class="icon">
                        <a href='<%#Eval("url")%>' title='<%#Eval("title")%>'>
                            <img src='<%# string.Format(((SiteMapNode) Container.DataItem)["icon"], ImageSizes)%>' alt='<%#Eval("title")%>' />
                        </a>
                    </div>
                    <div class="descr">
                        <a href='<%#Eval("url")%>' title='<%#Eval("title")%>'><%#Eval("title")%></a><br />
                        <%#Eval("description")%>
                    </div>
                    <div class="clear"></div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>