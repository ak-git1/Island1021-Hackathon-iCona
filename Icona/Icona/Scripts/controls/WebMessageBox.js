//---------------------Функция для открытия WebMesageBox -----------------------------------
function showMessageBox(boxName, text, newTitle, icon, w, h) {
    w = w || 'auto';
    h = h || 'auto';
    $('#' + boxName + '_WebMessageBoxIcon').attr('src', '/Images/WebMessageBox/' + icon + '.png');
    $('#' + boxName + '_WebMessageBoxTextlbl').html(text);
    $('#' + boxName + '_WebMessageBoxPanel').dialog({ autoOpen: false, width: w, height: h, resizable: false, title: newTitle, draggable: true, modal: true, close: function (event, ui) { $(this).dialog('destroy'); } });;
    $('#' + boxName + '_WebMessageBoxPanel').dialog('open');
    $('#' + boxName + '_WebMessageBoxPanel').parent().appendTo($("form:first"));
}