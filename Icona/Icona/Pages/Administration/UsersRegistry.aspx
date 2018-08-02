<%@ Page Title="Управление пользователями" Language="C#" MasterPageFile="~/Masterpages/Main.Master" AutoEventWireup="true" CodeBehind="UsersRegistry.aspx.cs" Inherits="Icona.Pages.Administration.UsersRegistry" %>

<%@ Import Namespace="Ak.Framework.Core.Extensions" %>

<%@ Register Src="~/Usercontrols/UserProfileControl.ascx" TagName="UserProfilesControl" TagPrefix="uc1" %>
<%@ Register Src="~/Usercontrols/WaitControl.ascx" TagName="WaitControl" TagPrefix="uc1" %>
<%@ Register Src="~/Usercontrols/SmallImageButtonControl.ascx" TagName="SmallImageButtonControl" TagPrefix="uc1" %>
<%@ Register Src="~/Usercontrols/CookiedControls/CookiedTextBoxControl.ascx" TagPrefix="uc1" TagName="CookiedTextBoxControl" %>
<%@ Register Src="~/Usercontrols/CookiedControls/CookiedDropDownListControl.ascx" TagPrefix="uc1" TagName="CookiedDropDownListControl" %>
<%@ Register Src="~/Usercontrols/ToolsContainerControl.ascx" TagPrefix="uc1" TagName="ToolsContainerControl" %>

<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
        function confirmUserDelete(userFio)
        {
            return confirm('Вы уверены, что хотите удалить пользователя ' + userFio + ' ?');
        }
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="main-container">
        <h1>Управление пользователями</h1>
        <div class="grid-container">
            <uc1:ToolsContainerControl runat="server" ID="ToolsContainerControl" OnPageChanged="Reload" OnClearFilter="ClearFilterBtn_OnClick" OnApplyFilter="ApplyFilterBtn_OnClick">
                <FilterLeftColumnTemplateControls>
                    <asp:Label runat="server" Text="ФИО:" />
                    <uc1:CookiedTextBoxControl runat="server" ID="FilterNameTxt" SaveValueToCookie="true"  CssClass="filter-long-text" />
                    <asp:Label runat="server" Text="Логин:" />
                    <uc1:CookiedTextBoxControl runat="server" ID="FilterLoginTxt" SaveValueToCookie="true" CssClass="filter-long-text" />
                    <asp:Label runat="server" Text="E-mail:" />
                    <uc1:CookiedTextBoxControl runat="server" ID="FilterEmailTxt" SaveValueToCookie="true"  CssClass="filter-long-text" />
                </FilterLeftColumnTemplateControls>
                <FilterRightColumnTemplateControls>
                    <asp:Label runat="server" Text="Дата&nbsp;регистрации&nbsp;от:" />
                    <asp:Panel runat="server">
                        <uc1:CookiedTextBoxControl runat="server" ID="FilterRegistrationDateFromTxt" SaveValueToCookie="true" CalendarEnabled="true"  CssClass="filter-short-text" />
                        до
                        <uc1:CookiedTextBoxControl runat="server" ID="FilterRegistrationDateTillTxt" SaveValueToCookie="true" CalendarEnabled="true" CssClass="filter-short-text"/>
                    </asp:Panel>
                    <asp:Label runat="server" Text="Дата&nbsp;последнего&nbsp;входа&nbsp;с:" />
                    <asp:Panel runat="server">
                        <uc1:CookiedTextBoxControl runat="server" ID="FilterLastEnterDateFromTxt"  SaveValueToCookie="true" CalendarEnabled="true" CssClass="filter-short-text"/>
                        по
                        <uc1:CookiedTextBoxControl runat="server" ID="FilterLastEnterDateTillTxt" SaveValueToCookie="true" CalendarEnabled="true" CssClass="filter-short-text"/>
                    </asp:Panel>
                    <asp:Label runat="server" Text="Заблокирован:" />
                    <uc1:CookiedDropDownListControl runat="server" ID="FilterBlockedDdl" SaveValueToCookie="true" CreateSimpleList="true"/>
                </FilterRightColumnTemplateControls>
                <ButtonsTemplate>
                    <asp:Button runat="server" ID="AddUserBtn" Text="Добавить" CssClass="button" OnClick="AddUserBtnClick" />&nbsp;
                </ButtonsTemplate>
                <InfoRightTemplate>
                    <asp:Label runat="server" ID="UsersNumberLbl"  />
                </InfoRightTemplate>
            </uc1:ToolsContainerControl>
            <asp:UpdatePanel runat="server" ID="UsersUPnl" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="UsersGrid" AlternatingRowStyle-CssClass="odd" CssClass="grid-view" runat="server" AutoGenerateColumns="false" GridLines="None" OnRowDataBound="UsersGridRowDataBound"
                        ItemType="Icona.Logic.Entities.UserProfile">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="3%">
                                <HeaderTemplate>
                                    №
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + (PagingControl.CurrentPage - 1) * PagingControl.RecordsPerPage + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="15%">
                                <HeaderTemplate>
                                    ФИО пользователя
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="UserLnk" ToolTip="Открыть карточку пользователя" CommandName="get" CommandArgument='<%# Item.Id %>' OnCommand="UsersGrid_OnCommand" >
                                        <div class="icon">
                                            <img src="/Images/icons/user.png" title="Пользователь"/>
                                        </div>
                                        <span class="name">
                                            <%#Item.FIO%>
                                        </span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    Логин
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Item.Login%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="120px" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    Email
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href='mailto:<%#Item.Email%>'><%#Item.Email%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px">
                                <HeaderTemplate>
                                    Зарегистрирован
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="RegisterDateLbl"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px">
                                <HeaderTemplate>
                                    Последний&nbsp;вход
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LastLoginDateLbl"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="20px" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    Заблокирован
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="BlockedImgBtn" ToolTip='<%# Item.Blocked.ToBooleanString() %>'
                                        ImageUrl='<%# string.Format("~/Images/figures/{0}_16x16.png",  Item.Blocked ? "red_round" : "green_round") %>' />

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    Действие
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <uc1:SmallImageButtonControl runat="server" ID="EditUserBtn" OnCommand="UsersGrid_OnCommand" ImageUrl="/images/icons/vcard_edit.png"
                                                                 Visible="false" ToolTip="Редактировать" CommandName="get" CommandArgument='<%#Item.Id%>'/>                                  
                                    <uc1:SmallImageButtonControl runat="server" ID="BlockUserBtn" OnCommand="UsersGrid_OnCommand"
                                                                 ToolTip="Заблокировать" Visible="false" ImageUrl="/images/icons/lock.png"
                                                                 CommandName="block" OnClientClick="return confirm('Вы уверены, что хотите заблокировать пользователя?');"
                                                                 CommandArgument='<%#Item.Id%>'/>                                    
                                    <uc1:SmallImageButtonControl runat="server" ID="UnblockUserBtn" OnCommand="UsersGrid_OnCommand"
                                                                 ToolTip="Разблокировать" Visible="false" ImageUrl="/images/icons/lock_open.png"
                                                                 CommandName="unblock" OnClientClick="return confirm('Вы уверены, что хотите разблокировать пользователя?');"
                                                                 CommandArgument='<%#Item.Id%>'/>                                
                                    <uc1:SmallImageButtonControl runat="server" ID="DeleteUserLinkButton" OnCommand="UsersGrid_OnCommand"
                                                                 ToolTip="Удалить" ImageUrl="/images/icons/cross.png"
                                                                 CommandName="delete-item" OnClientClick='<%# string.Format("return confirmUserDelete(\"{0}\")", Item.Login)%>'
                                                                 CommandArgument='<%#Item.Id%>'/>                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="UserProfilesControl" EventName="Saved" />
                    <asp:AsyncPostBackTrigger ControlID="ToolsContainerControl" EventName="ClearFilter" />
                    <asp:AsyncPostBackTrigger ControlID="ToolsContainerControl" EventName="ApplyFilter" />
                    <asp:AsyncPostBackTrigger ControlID="ToolsContainerControl" EventName="PageChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <uc1:UserProfilesControl ID="UserProfilesControl" runat="server" OnSaved="UserProfilesControl_OnSaved" ShowRoles="True" />
        </div>
    </div>
    <uc1:WaitControl runat="server" ID="WaitControl" />
</asp:Content>
<asp:Content ContentPlaceHolderID="BottomScripts" runat="server">
    <script src="/Scripts/filters.js" type="text/javascript"></script>
</asp:Content>