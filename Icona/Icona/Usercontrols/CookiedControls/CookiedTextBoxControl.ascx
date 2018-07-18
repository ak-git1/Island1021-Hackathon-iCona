<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CookiedTextBoxControl.ascx.cs" Inherits="Icona.Usercontrols.CookiedControls.CookiedTextBoxControl" %>

<script src="/Scripts/controls/CookiedTextBoxControl.js" type="text/javascript"></script>

<asp:TextBox ID="TextBox" runat="server" CssClass="filter-textbox" />
<ajaxToolkit:CalendarExtender ID="TextBoxCalendarExtender" runat="server" Enabled="false" TargetControlID="TextBox" />
<ajaxToolkit:AutoCompleteExtender runat="server" Enabled="false" MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false"
    CompletionSetCount="10" ID="AutoCompleteExtender" FirstRowSelected = "false" TargetControlID="TextBox" />