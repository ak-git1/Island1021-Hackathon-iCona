<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FilterButton.ascx.cs" Inherits="Icona.Usercontrols.FilterButton" %>

<span style="position: relative;">
    <input id="visiblePnl" type="hidden" value="hidden" runat="server" ClientIDMode="Static" />
    <span class="filter-badge" id="filterBadge"></span>
    <input type="button" id="filterButton" value="Фильтр" class="button" onclick="toggleFilterPanel();" />
</span>