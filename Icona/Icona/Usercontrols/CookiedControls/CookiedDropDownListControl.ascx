<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CookiedDropDownListControl.ascx.cs" Inherits="Icona.Usercontrols.CookiedControls.CookiedDropDownListControl" %>

<script src="/Scripts/controls/CookiedDropDownListControl.js" type="text/javascript"></script>

<asp:DropDownList ID="DropDownList" CssClass="filter-ddl" runat="server" OnSelectedIndexChanged="DropDownList_OnSelectedIndexChanged" />
<asp:HiddenField ID="hdnDropDownListValue" runat="server" />