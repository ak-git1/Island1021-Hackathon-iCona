<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WaitControl.ascx.cs" Inherits="Icona.Usercontrols.WaitControl" %>

<style type="text/css">
    div.wait
    {
        position: absolute;
        color: #000;
        background-color: #ffdc87;
        padding: 8px;
        text-align: center;
        font-weight: bold;
        z-index: 9000;
    }

    div.wait div
    {
        float: left;
        padding-right: 7px;
    }
</style>

<script src="/Scripts/jquery.signalR-1.1.4.min.js"></script>
<script src="/signalr/hubs"></script>
<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_initializeRequest(WaitControl_initializeRequest);
    prm.add_endRequest(WaitControl_endRequest);
    var WaitControl_timer;

    function WaitControl_initializeRequest(sender, args) {
        WaitControl_timer = window.setTimeout(WaitControl_onWait, 600);
    }

    function WaitControl_endRequest(sender, args) {
        WaitControl_hideWait();
        window.clearTimeout(WaitControl_timer);
    }

    function WaitControl_scrollHandler() {
        $('#waitPnl').css('top', (WaitControl_scrollTop() - 3));
    }

    function WaitControl_onWait(str) {
        var isInAsyncPostBack = prm.get_isInAsyncPostBack();
        if (!isInAsyncPostBack) {
            WaitControl_endRequest();
        } else {
            WaitControl_showWait();
        }
    }
    
    function WaitControl_showWait() {
        var el = $('#waitPnl');
        el.css('top', (WaitControl_scrollTop() - 3));
        el.css('left', (WaitControl_windowSize().width / 2 - el.width()));
        el.slideDown("slow");
        window.onscroll = WaitControl_scrollHandler;
    }

    function WaitControl_hideWait() {
        $('#waitPnl').slideUp("slow");
        window.onscroll = null;
    }

    function WaitControl_windowSize() {
        var w = 0;
        var h = 0;
        if (!window.innerWidth) {
            if (!(document.documentElement.clientWidth == 0)) {
                w = document.documentElement.clientWidth;
                h = document.documentElement.clientHeight;
            } else {
                w = document.body.clientWidth;
                h = document.body.clientHeight;
            }
        } else {
            w = window.innerWidth;
            h = window.innerHeight;
        }
        return { width: w, height: h };
    }

    function WaitControl_scrollTop() {
        if (document.documentElement.scrollTop)
            return document.documentElement.scrollTop;
        else
            return document.body.scrollTop;
    }

    var waitControlUniqueKey;

    var WaitControl_onClientClick = function () {
        var waitControlUniqueKey = $get("<% =hfWaitControlUniqueKey.ClientID %>").value;
        window.waitControlUniqueKey = waitControlUniqueKey;
        WaitControl_showWait();

        var fileTransferStartedHub = $.connection.fileTransferStartedHub;

        fileTransferStartedHub.client.fileTransferStarted = function (uniqueKey) {
            if (uniqueKey && window.waitControlUniqueKey && uniqueKey === window.waitControlUniqueKey) {
                WaitControl_hideWait();
            }

            $.connection.hub.stop();
        };

        $.connection.hub.start();
    }
</script>

<div id="waitPnl" class="wait ui-corner-all popup">
    <div>
        <img src="/Images/loader.gif" alt="Загрузка"/>
    </div>
    <asp:UpdatePanel ID="WaitUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label runat="server" ID="TextLbl" Text="Мы работаем над этим..." />
        </ContentTemplate>
    </asp:UpdatePanel> 
</div>

<asp:HiddenField runat="server" ID="hfWaitControlUniqueKey" />