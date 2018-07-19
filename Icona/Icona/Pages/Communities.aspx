<%@ Page Title="Список сообществ" Language="C#" MasterPageFile="~/Masterpages/Main.Master" AutoEventWireup="true" CodeBehind="Communities.aspx.cs" Inherits="Icona.Pages.Communities" %>

<%@ Import Namespace="Elar.Framework.Core.Extensions" %>

<%@ Register Src="~/Usercontrols/CommunityControl.ascx" TagName="CommunityControl" TagPrefix="uc1" %>
<%@ Register Src="~/Usercontrols/WaitControl.ascx" TagName="WaitControl" TagPrefix="uc1" %>
<%@ Register Src="~/Usercontrols/SmallImageButtonControl.ascx" TagName="SmallImageButtonControl" TagPrefix="uc1" %>
<%@ Register Src="~/Usercontrols/AdvancedLinkButtonControl.ascx" TagName="AdvancedLinkButtonControl" TagPrefix="uc1" %>
<%@ Register Src="~/Usercontrols/CookiedControls/CookiedTextBoxControl.ascx" TagPrefix="uc1" TagName="CookiedTextBoxControl" %>
<%@ Register Src="~/Usercontrols/ToolsContainerControl.ascx" TagPrefix="uc1" TagName="ToolsContainerControl" %>

<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
        function confirmCommunityDelete()
        {
            return confirm('Вы уверены, что хотите удалить сообщество?');
        }
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="main-container">
        <h1>Список сообществ</h1>
        <div class="grid-container">
            <uc1:ToolsContainerControl runat="server" ID="ToolsContainerControl" OnPageChanged="Reload" OnClearFilter="ClearFilterBtn_OnClick" OnApplyFilter="ApplyFilterBtn_OnClick">
                <FilterLeftColumnTemplateControls>
                    <asp:Label runat="server" Text="Название:" />
                    <uc1:CookiedTextBoxControl runat="server" ID="FilterNameTxt" SaveValueToCookie="true"  CssClass="filter-long-text" />
                </FilterLeftColumnTemplateControls>
                <ButtonsTemplate>
                    <asp:Button runat="server" ID="AddCommunityBtn" Text="Добавить" CssClass="button" OnClick="AddCommunityBtn_OnClick" />&nbsp;
                </ButtonsTemplate>
            </uc1:ToolsContainerControl>
            <asp:UpdatePanel runat="server" ID="CommunitiesUPnl" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="CommunitiesGrid" AlternatingRowStyle-CssClass="odd" CssClass="grid-view" runat="server" AutoGenerateColumns="false" GridLines="None" OnRowDataBound="CommunitiesGrid_OnRowDataBound"
                        ItemType="Icona.Logic.Entities.Community">
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
                                    <uc1:AdvancedLinkButtonControl ID="CommunityLnk" CssClass="name" ToolTip="Перейти к новостям сообщества" runat="server"
                                                                   Text="<%#Item.Name%>"  DestinationUrl='<%#$"/Pages/NewsList.aspx?CommunityId={Item.Id}" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                <HeaderTemplate>
                                    Дата создания
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Item.CreationDate.ToDateTime() %>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    Действие
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <uc1:SmallImageButtonControl runat="server" ID="EditCommunityBtn" OnCommand="CommunitiesGrid_OnCommand" ImageUrl="/images/icons/application_form_edit.png"
                                                                 Visible="false" ToolTip="Редактировать" CommandName="get" CommandArgument='<%#Item.Id%>'/>
                                    <uc1:SmallImageButtonControl runat="server" ID="ChannelsBtn" ImageUrl="/images/icons/link_edit.png"
                                                                 Visible="True" ToolTip="Перейти к каналам" DestinationUrl='<%#$"/Pages/Channels.aspx?CommunityId={Item.Id}" %>'/>
                                    <uc1:SmallImageButtonControl runat="server" ID="NewsModerationBtn" ImageUrl="/images/icons/newspaper.png"
                                                                 Visible="True" ToolTip="Перейти к управлению новостями" DestinationUrl='<%#$"/Pages/ModeratedNewsList.aspx?CommunityId={Item.Id}" %>'/>
                                    <uc1:SmallImageButtonControl runat="server" ID="DeleteCommunityButton" OnCommand="CommunitiesGrid_OnCommand"
                                                                 ToolTip="Удалить" ImageUrl="/images/icons/cross.png"
                                                                 CommandName="delete-item" OnClientClick="return confirmCommunityDelete();"
                                                                 CommandArgument='<%#Item.Id%>'/>                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="CommunityControl" EventName="Saved" />
                    <asp:AsyncPostBackTrigger ControlID="ToolsContainerControl" EventName="ClearFilter" />
                    <asp:AsyncPostBackTrigger ControlID="ToolsContainerControl" EventName="ApplyFilter" />
                    <asp:AsyncPostBackTrigger ControlID="ToolsContainerControl" EventName="PageChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <uc1:CommunityControl ID="CommunityControl" runat="server" OnSaved="CommunityControl_OnSaved" ShowRoles="True" />
        </div>
    </div>
    <uc1:WaitControl runat="server" ID="WaitControl" />
</asp:Content>
<asp:Content ContentPlaceHolderID="BottomScripts" runat="server">
    <script src="/Scripts/filters.js" type="text/javascript"></script>
</asp:Content>