<%@ Page Title="Модерирование списка новостей" Language="C#" MasterPageFile="~/Masterpages/Main.Master" AutoEventWireup="true" CodeBehind="ModeratedNewsList.aspx.cs" Inherits="Icona.Pages.ModeratedNewsList" %>

<%@ Import Namespace="Elar.Framework.Core.Extensions" %>

<%@ Register Src="~/Usercontrols/WaitControl.ascx" TagName="WaitControl" TagPrefix="uc1" %>
<%@ Register Src="~/Usercontrols/SmallImageButtonControl.ascx" TagName="SmallImageButtonControl" TagPrefix="uc1" %>
<%@ Register Src="~/Usercontrols/AdvancedLinkButtonControl.ascx" TagName="AdvancedLinkButtonControl" TagPrefix="uc1" %>
<%@ Register Src="~/Usercontrols/CookiedControls/CookiedTextBoxControl.ascx" TagPrefix="uc1" TagName="CookiedTextBoxControl" %>
<%@ Register Src="~/Usercontrols/ToolsContainerControl.ascx" TagPrefix="uc1" TagName="ToolsContainerControl" %>

<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
        function confirmNewsItemDelete()
        {
            return confirm('Вы уверены, что хотите удалить новость?');
        }
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="main-container">
        <h1><asp:Label runat="server" ID="HeaderLbl" /></h1>
        <div class="grid-container">
            <uc1:ToolsContainerControl runat="server" ID="ToolsContainerControl" OnPageChanged="Reload" OnClearFilter="ClearFilterBtn_OnClick" OnApplyFilter="ApplyFilterBtn_OnClick">
                <FilterLeftColumnTemplateControls>
                    <asp:Label runat="server" Text="Название:" />
                    <uc1:CookiedTextBoxControl runat="server" ID="FilterNameTxt" SaveValueToCookie="true"  CssClass="filter-long-text" />
                    <asp:Label runat="server" Text="Дата&nbsp;новости&nbsp;с:" />
                    <asp:Panel runat="server">
                        <uc1:CookiedTextBoxControl runat="server" ID="FilterDateFromTxt"  SaveValueToCookie="true" CalendarEnabled="true" CssClass="filter-short-text"/>
                        по
                        <uc1:CookiedTextBoxControl runat="server" ID="FilterDateTillTxt" SaveValueToCookie="true" CalendarEnabled="true" CssClass="filter-short-text"/>
                    </asp:Panel>
                </FilterLeftColumnTemplateControls>
            </uc1:ToolsContainerControl>
            <asp:UpdatePanel runat="server" ID="NewsItemsUPnl" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="NewsItemsGrid" AlternatingRowStyle-CssClass="odd" CssClass="grid-view" runat="server" AutoGenerateColumns="false" GridLines="None" OnRowDataBound="NewsItemsGrid_OnRowDataBound"
                        ItemType="Icona.Logic.Entities.NewsItem">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="3%">
                                <HeaderTemplate>
                                    №
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + (PagingControl.CurrentPage - 1) * PagingControl.RecordsPerPage + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Название
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <uc1:AdvancedLinkButtonControl ID="NewsItemLnk" CssClass="name" ToolTip="Перейти по ссылке к новости" runat="server"
                                                                   Text="<%#Item.Title%>" DestinationUrl='<%#Item.Url %>' />
                                    <%# Item.Description.TruncateString(255) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                <HeaderTemplate>
                                    Дата
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Item.Date.ToDateTime() %>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    Действие
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <uc1:SmallImageButtonControl runat="server" ID="PublishBtn" OnCommand="NewsItemsGrid_OnCommand" ImageUrl="/images/icons/application_form_edit.png"
                                                                 Visible="false" ToolTip="Согласовать" CommandName="publish" CommandArgument='<%#Item.Id%>'/>
                                    <uc1:SmallImageButtonControl runat="server" ID="DeclineBtn" ImageUrl="/images/icons/link_edit.png" OnCommand="NewsItemsGrid_OnCommand"
                                                                 Visible="True" ToolTip="Отклонить" CommandName="decline" CommandArgument='<%#Item.Id%>'/>
                                    <uc1:SmallImageButtonControl runat="server" ID="DeleteButton" OnCommand="NewsItemsGrid_OnCommand"
                                                                 ToolTip="Удалить" ImageUrl="/images/icons/cross.png"
                                                                 CommandName="delete-item" OnClientClick="return confirmNewsItemDelete();"
                                                                 CommandArgument='<%#Item.Id%>'/>                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ToolsContainerControl" EventName="ClearFilter" />
                    <asp:AsyncPostBackTrigger ControlID="ToolsContainerControl" EventName="ApplyFilter" />
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