<%@ Page Title="Новостные каналы" Language="C#" MasterPageFile="~/Masterpages/Main.Master" AutoEventWireup="true" CodeBehind="Channels.aspx.cs" Inherits="Icona.Pages.Channels" %>

<%@ Import Namespace="Icona.Common.Enums" %>
<%@ Import Namespace="Ak.Framework.Core.Helpers" %>

<%@ Register Src="~/Usercontrols/ChannelControl.ascx" TagName="ChannelControl" TagPrefix="uc1" %>
<%@ Register Src="~/Usercontrols/WaitControl.ascx" TagName="WaitControl" TagPrefix="uc1" %>
<%@ Register Src="~/Usercontrols/SmallImageButtonControl.ascx" TagName="SmallImageButtonControl" TagPrefix="uc1" %>
<%@ Register Src="~/Usercontrols/CookiedControls/CookiedTextBoxControl.ascx" TagPrefix="uc1" TagName="CookiedTextBoxControl" %>
<%@ Register Src="~/Usercontrols/ToolsContainerControl.ascx" TagPrefix="uc1" TagName="ToolsContainerControl" %>

<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
        function confirmChannelDelete()
        {
            return confirm('Вы уверены, что хотите удалить новостной канал?');
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
                </FilterLeftColumnTemplateControls>
                <ButtonsTemplate>
                    <asp:Button runat="server" ID="AddChannelBtn" Text="Добавить" CssClass="button" OnClick="AddChannelBtn_OnClick" />&nbsp;
                </ButtonsTemplate>
            </uc1:ToolsContainerControl>
            <asp:UpdatePanel runat="server" ID="ChannelsUPnl" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="ChannelsGrid" AlternatingRowStyle-CssClass="odd" CssClass="grid-view" runat="server" AutoGenerateColumns="false" GridLines="None" OnRowDataBound="ChannelsGrid_OnRowDataBound"
                        ItemType="Icona.Logic.Entities.Channel">
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
                                    <asp:LinkButton runat="server" ID="ChannelLnk" CssClass="name" ToolTip="Открыть карточку новостного канала" CommandName="get" CommandArgument='<%# Item.Id %>' OnCommand="ChannelsGrid_OnCommand" >
                                        <%#Item.Title%>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Описание
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Item.Description %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px">
                                <HeaderTemplate>
                                    Тип
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# EnumNamesHelper.Get((ChannelTypes)Item.Type) %>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    Действие
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <uc1:SmallImageButtonControl runat="server" ID="EditChannelBtn" OnCommand="ChannelsGrid_OnCommand" ImageUrl="/images/icons/application_form_edit.png"
                                                                 ToolTip="Редактировать" CommandName="get" CommandArgument='<%#Item.Id%>'/>
                                    <uc1:SmallImageButtonControl runat="server" ID="DeleteChannelButton" OnCommand="ChannelsGrid_OnCommand"
                                                                 ToolTip="Удалить" ImageUrl="/images/icons/cross.png"
                                                                 CommandName="delete-item" OnClientClick="return confirmChannelDelete();"
                                                                 CommandArgument='<%#Item.Id%>'/>                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ChannelControl" EventName="Saved" />
                    <asp:AsyncPostBackTrigger ControlID="ToolsContainerControl" EventName="ClearFilter" />
                    <asp:AsyncPostBackTrigger ControlID="ToolsContainerControl" EventName="ApplyFilter" />
                    <asp:AsyncPostBackTrigger ControlID="ToolsContainerControl" EventName="PageChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <uc1:ChannelControl ID="ChannelControl" runat="server" OnSaved="ChannelControl_OnSaved" ShowRoles="True" />
        </div>
    </div>
    <uc1:WaitControl runat="server" ID="WaitControl" />
</asp:Content>
<asp:Content ContentPlaceHolderID="BottomScripts" runat="server">
    <script src="/Scripts/filters.js" type="text/javascript"></script>
</asp:Content>