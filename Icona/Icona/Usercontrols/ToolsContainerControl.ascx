<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ToolsContainerControl.ascx.cs" Inherits="Icona.Usercontrols.ToolsContainerControl" %>

<%@ Register Namespace="Icona.Usercontrols" Assembly="Icona" TagPrefix="c" %>
<%@ Register Src="~/Usercontrols/PagingControl.ascx" TagName="PagingControl" TagPrefix="uc1" %>
<%@ Register Src="~/Usercontrols/FilterButton.ascx" TagPrefix="uc1" TagName="FilterButton" %>

<asp:UpdatePanel runat="server" ID="MenuUPnl" UpdateMode="Conditional">
    <ContentTemplate>
        <div ID="ToolsContainer" runat="server">
            <div class="menu-container header">
            </div>
            <div ID="TopInfoDiv" runat="server">
                <div class="menu-container info">
                    <div class="padding-10px">
                        <c:RepeatedControls ID="TopInfoControls" runat="server" />
                    </div>
                </div>
                <div class="menu-container footer">
                </div>
            </div>
            <div class="menu-container">
                <div id="buttonsPnl" class="buttons">
                    <div class="padding-5px">
                        <uc1:FilterButton runat="server" ID="FilterBtn" ApplyButtonName='<%# ApplyFilterBtn.ClientID %>' />
                        <c:RepeatedControls ID="ButtonControls" runat="server">
                            <SeparatorHtml>&#xA;</SeparatorHtml>
                        </c:RepeatedControls>
                    </div>
                </div>
                <div class="navigation">
                    <uc1:PagingControl runat="server" ID="PagingControl" ShowTotalNumber="true" OnPagingChanged="PagingControl_Reload" />
                </div>
                <div class="info">
                    <c:RepeatedControls ID="InfoControls" runat="server" />
                </div>
                <div class="info-right">
                    <c:RepeatedControls ID="InfoRightControls" runat="server" />
                    <asp:LinkButton runat="server" ID="ReturnBtn" Visible="False" Text="Вернуться к списку" CssClass="return-link" />
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="menu-container footer">
            </div>

            <asp:Panel ID="filterPnl"  ClientIDMode="Static" runat="server">
                <div class="menu-container">
                    <table>
                        <tr>
                            <td class="filter-left-column">
                                <table class="wide">
                                    <tr>
                                        <c:RepeatedControls ID="LeftColumn" runat="server">
                                            <BeforeLabelHtml>&lt;td class="filter-grid-cell"&gt;</BeforeLabelHtml>
                                            <BeforeControlHtml>&lt;/td&gt;&lt;td class="filter-grid-cell"&gt;</BeforeControlHtml>
                                            <AfterControlHtml>&lt;/td&gt;</AfterControlHtml>
                                            <SeparatorHtml>&lt;/tr&gt;&lt;tr&gt;</SeparatorHtml>
                                        </c:RepeatedControls>
                                    </tr>
                                </table>
                            </td>
                            <td class="filter-middle-column">&nbsp;</td>
                            <td class="filter-right-column" ID="FilterRightColumn" runat="server">
                                <table class="wide">
                                    <tr>
                                        <c:RepeatedControls ID="RightColumn" runat="server">
                                            <BeforeLabelHtml>&lt;td class="filter-grid-cell"&gt;</BeforeLabelHtml>
                                            <BeforeControlHtml>&lt;/td&gt;&lt;td class="filter-grid-cell"&gt;</BeforeControlHtml>
                                            <AfterControlHtml>&lt;/td&gt;</AfterControlHtml>
                                            <SeparatorHtml>&lt;/tr&gt;&lt;tr&gt;</SeparatorHtml>
                                        </c:RepeatedControls>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button runat="server" ID="ClearFilterBtn" Text="Очистить" CssClass="button" OnClick="ClearFilterBtn_OnClick" />
                                <asp:Button runat="server" ID="ApplyFilterBtn" Text="Применить" CssClass="button" OnClick="ApplyFilterBtn_OnClick" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="menu-container footer">
                </div>
            </asp:Panel>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ClearFilterBtn" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="ApplyFilterBtn" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="PagingControl" EventName="PagingChanged" />
        <asp:AsyncPostBackTrigger ControlID="PagingControl" EventName="TotalPagesChanging" />
    </Triggers>
</asp:UpdatePanel>