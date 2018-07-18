<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChannelControl.ascx.cs" Inherits="Icona.Usercontrols.ChannelControl" %>

<asp:Panel ID="ChannelPanel" runat="server" class="popup">
    <asp:UpdatePanel runat="server" ID="ChannelUPnl" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div class="table bordered-control">
                <div class="row">
                    <div class="cell1">
                        Название
                    </div>
                    <div class="cell2">
                        <asp:TextBox runat="server" CssClass="all-textbox wide" ID="NameTxt" />
                        <asp:CustomValidator ID="NameTxtCustomValidator" runat="server" ValidateEmptyText="true" Enabled="true" ValidationGroup="ChannelDetails" ClientValidationFunction="checkAndPaint" ControlToValidate="NameTxt" Display="None" ErrorMessage="Название не может быть пустым">*</asp:CustomValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="NameTxtValidatorCalloutExtender" PopupPosition="Left" runat="server" Enabled="True" TargetControlID="NameTxtCustomValidator" />
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="row">
                    <div class="cell1">
                        Тип канала <span class="required">*</span>
                    </div>
                    <div class="cell2">
                        <asp:DropDownList ID="TypesDdl" runat="server" CssClass="list all-list" Width="95%" AutoPostBack="False" />
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="row">
                    <div class="cell1">
                        Url
                    </div>
                    <div class="cell2">
                        <asp:TextBox runat="server" CssClass="all-textbox wide" ID="UrlTxt" />
                        <asp:CustomValidator ID="UrlTxtCustomValidator" runat="server" ValidateEmptyText="true" Enabled="true" ValidationGroup="ChannelDetails" ClientValidationFunction="checkAndPaint" ControlToValidate="UrlTxt" Display="None" ErrorMessage="Url не может быть пустым">*</asp:CustomValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="UrlTxtValidatorCalloutExtender" PopupPosition="Left" runat="server" Enabled="True" TargetControlID="UrlTxtCustomValidator" />
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="row">
                    <div class="cell1">
                        Настройки доступа
                    </div>
                    <div class="cell2">
                        <asp:TextBox ID="AttributesTxt" runat="server" Rows="3" CssClass="all-multilinetextbox wide" TextMode="MultiLine" />
                        <div class="description">
                            Параметры для получения доступа к данным в источнике
                        </div>
                    </div>                 
                    <div class="clear">
                    </div>
                </div>
                <div class="row">
                    <div class="cell1">
                        Теги
                    </div>
                    <div class="cell2">
                        <asp:TextBox ID="TagsTxt" runat="server" Rows="3" CssClass="all-multilinetextbox wide" TextMode="MultiLine" />
                        <div class="description">
                            Через точку с запятой
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="row">
                    <div class="cell1">
                        Описание
                    </div>
                    <div class="cell2">
                        <asp:TextBox ID="DescriptionTxt" runat="server" Rows="3" CssClass="all-multilinetextbox wide" TextMode="MultiLine" />
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="row">
                    <div class="cell1">
                    </div>
                    <div class="cell2">
                        <asp:Button ID="SaveBtn" runat="server" CssClass="button" Text="Сохранить" OnClick="SaveBtnClick" ValidationGroup="ChannelDetails"/>
                        <span onclick="closeDialog('<%=ChannelPanel.ClientID %>');">
                            <input id="CancelBtn" type="button" class="button" value="Отмена" />
                        </span>
                    </div>
                    <div class="clear">
                    </div>
                </div>                                          
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>