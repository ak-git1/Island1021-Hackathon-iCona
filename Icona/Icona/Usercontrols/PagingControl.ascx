<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PagingControl.ascx.cs" Inherits="Icona.Usercontrols.PagingControl" %>

<div class="navigation">
    <asp:Label runat="server" ID="_lTotalNumber"/>&nbsp;
    <asp:Button ID="_bBack" runat="server" Text="назад" CssClass="button" OnClick="OnBackClick" />
    <asp:TextBox ID="_tbCurrentPage" autocomplete="off" runat="server" AutoPostBack="true" Width="25" OnTextChanged="OnCurrentPageTextChanged" Text="1" CssClass="all-textbox" />
    <ajaxToolkit:FilteredTextBoxExtender ID="_tbCurrentPage_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="_tbCurrentPage" ValidChars="0123456789"/>    
    &nbsp;/&nbsp;<asp:Label ID="_lTotalPages" runat="server" />    
    <asp:Button ID="_bForward" runat="server" Text="вперед" CssClass="button" OnClick="OnForwardClick" />
    &nbsp;&nbsp; Показывать по
    <asp:DropDownList runat="server" ID="_ddlRecordsPerPage" AutoPostBack="true" OnSelectedIndexChanged="OnRecordsPerPageChanged" CssClass="all-list" Width="120px" />
    <asp:HiddenField runat="server" ID="_hfTotalRecords" />
</div>
<div class="clear">
</div>
