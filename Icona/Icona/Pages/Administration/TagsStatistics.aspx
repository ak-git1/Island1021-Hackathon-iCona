<%@ Page Title="Статистика использования тегов" Language="C#" MasterPageFile="~/Masterpages/Main.Master" AutoEventWireup="true" CodeBehind="TagsStatistics.aspx.cs" Inherits="Icona.Pages.Administration.TagsStatistics" %>

<%@ Import Namespace="Ak.Framework.Core.Extensions" %>

<%@ Register Src="~/Usercontrols/WaitControl.ascx" TagName="WaitControl" TagPrefix="uc1" %>
<%@ Register Src="~/Usercontrols/ToolsContainerControl.ascx" TagPrefix="uc1" TagName="ToolsContainerControl" %>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="main-container">
        <h1>Статистика использования тегов</h1>
        <div class="grid-container">
            <uc1:ToolsContainerControl runat="server" ID="ToolsContainerControl" OnPageChanged="Reload">
            </uc1:ToolsContainerControl>
            <asp:UpdatePanel runat="server" ID="TagsStatisticsUPnl" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="TagsStatisticsGrid" AlternatingRowStyle-CssClass="odd" CssClass="grid-view" runat="server" AutoGenerateColumns="false" GridLines="None" OnRowDataBound="TagsStatisticsGrid_OnRowDataBound"
                        ItemType="Icona.Logic.Entities.TagsStatisticsItem">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="3%">
                                <HeaderTemplate>
                                    №
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + (PagingControl.CurrentPage - 1) * PagingControl.RecordsPerPage + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="150px">
                                <HeaderTemplate>
                                    Название
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Item.Name %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Новостной канал
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Item.ChannelName %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="110px">
                                <HeaderTemplate>
                                    Опубликовано
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Item.PublishedNewsQuantity %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="110px">
                                <HeaderTemplate>
                                    Просмотрено
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Item.ViewsQuantity %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="110px">
                                <HeaderTemplate>
                                    Нравится
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Item.PositiveVotesQuantity %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="110px">
                                <HeaderTemplate>
                                    Не нравится
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Item.NegativeVotesQuantity %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="110px">
                                <HeaderTemplate>
                                    Комментарии
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Item.CommentsQuantity %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ToolsContainerControl" EventName="PageChanged" />
                </Triggers>
            </asp:UpdatePanel>            
        </div>
    </div>
    <uc1:WaitControl runat="server" ID="WaitControl" />
</asp:Content>
<asp:Content ContentPlaceHolderID="BottomScripts" runat="server">
    <script src="/Scripts/filters.js" type="text/javascript"></script>
</asp:Content>