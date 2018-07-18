/*******************************************************************************
*   Асинхронное сообщение
*******************************************************************************/
function alertAsync(msgText) {
    setTimeout(function () {
        alert(msgText);
    }, 1);
}

/*******************************************************************************
*   Сообщение об ошибке
*******************************************************************************/
function alertError()
{
    setTimeout(function () {
        alert('К сожалению произошла ошибка. Попробуйте еще раз...');
    }, 1);    
}


/*******************************************************************************
*   Вывод сообщения в консоль
*******************************************************************************/
function log(message) {
    if (window.console && typeof window.console.log == "function") {
        console.log(message);
    }
}

/*******************************************************************************
*   Транслитерация
*******************************************************************************/
function cyr2lat(str) {
    var cyr2latChars = new Array(['а', 'a'], ['б', 'b'], ['в', 'v'], ['г', 'g'], ['д', 'd'], ['е', 'e'], ['ё', 'yo'], ['ж', 'zh'], ['з', 'z'], ['и', 'i'], ['й', 'y'], ['к', 'k'], ['л', 'l'], ['м', 'm'], ['н', 'n'], ['о', 'o'], ['п', 'p'], ['р', 'r'], ['с', 's'], ['т', 't'], ['у', 'u'], ['ф', 'f'], ['х', 'h'], ['ц', 'c'], ['ч', 'ch'], ['ш', 'sh'], ['щ', 'shch'], ['ъ', ''], ['ы', 'y'], ['ь', ''], ['э', 'e'], ['ю', 'yu'], ['я', 'ya'], ['А', 'A'], ['Б', 'B'], ['В', 'V'], ['Г', 'G'], ['Д', 'D'], ['Е', 'E'], ['Ё', 'YO'], ['Ж', 'ZH'], ['З', 'Z'], ['И', 'I'], ['Й', 'Y'], ['К', 'K'], ['Л', 'L'], ['М', 'M'], ['Н', 'N'], ['О', 'O'], ['П', 'P'], ['Р', 'R'], ['С', 'S'], ['Т', 'T'], ['У', 'U'], ['Ф', 'F'], ['Х', 'H'], ['Ц', 'C'], ['Ч', 'CH'], ['Ш', 'SH'], ['Щ', 'SHCH'], ['Ъ', ''], ['Ы', 'Y'], ['Ь', ''], ['Э', 'E'], ['Ю', 'YU'], ['Я', 'YA'], ['a', 'a'], ['b', 'b'], ['c', 'c'], ['d', 'd'], ['e', 'e'], ['f', 'f'], ['g', 'g'], ['h', 'h'], ['i', 'i'], ['j', 'j'], ['k', 'k'], ['l', 'l'], ['m', 'm'], ['n', 'n'], ['o', 'o'], ['p', 'p'], ['q', 'q'], ['r', 'r'], ['s', 's'], ['t', 't'], ['u', 'u'], ['v', 'v'], ['w', 'w'], ['x', 'x'], ['y', 'y'], ['z', 'z'], ['A', 'A'], ['B', 'B'], ['C', 'C'], ['D', 'D'], ['E', 'E'], ['F', 'F'], ['G', 'G'], ['H', 'H'], ['I', 'I'], ['J', 'J'], ['K', 'K'], ['L', 'L'], ['M', 'M'], ['N', 'N'], ['O', 'O'], ['P', 'P'], ['Q', 'Q'], ['R', 'R'], ['S', 'S'], ['T', 'T'], ['U', 'U'], ['V', 'V'], ['W', 'W'], ['X', 'X'], ['Y', 'Y'], ['Z', 'Z'], [' ', '-'], ['0', '0'], ['1', '1'], ['2', '2'], ['3', '3'], ['4', '4'], ['5', '5'], ['6', '6'], ['7', '7'], ['8', '8'], ['9', '9'], ['-', '-']);
    var newStr = new String();

    for (var i = 0; i < str.length; i++) {
        ch = str.charAt(i);
        var newCh = '';
        for (var j = 0; j < cyr2latChars.length; j++) {
            if (ch == cyr2latChars[j][0]) {
                newCh = cyr2latChars[j][1];
            }
        }
        newStr += newCh;
    }
    return newStr.replace(/[-]{2,}/gim, '-').replace(/\n/gim, '');
}

/*******************************************************************************
*   Приведение кнопки 'Закрыть' диалоговых окон на AjaxToolKit к виду jQuery
*******************************************************************************/
function upgradeCloseButton() {
    $('.ui-dialog-titlebar-close.rounded').hover(
        function () {
            $(this).addClass('ui-state-hover');
        },
        function () {
            $(this).removeClass('ui-state-hover');
        }
    );
}

/*******************************************************************************
*   Функция для открытия диалогов на основе jQuery UI
*******************************************************************************/
function openDialog(panelName, newTitle, w, h, disableEnter, cancelButtonId, isModal, titleBarVisible, closeButtonVisible) {

    w = w || 'auto';
    h = h || 'auto';
    var bDisableEnter = (disableEnter||"").toLowerCase() == 'true';
    var bIsModal = (isModal||"").toLowerCase() == 'true';

    if (bDisableEnter)
        disableEnterKey(panelName);

    $('#' + panelName).dialog({
        autoOpen: false, width: w, height: h, minHeight: 16, resizable: false, title: newTitle, draggable: true, modal: bIsModal, close: function (event, ui) {
            $(this).dialog('destroy');
            if (cancelButtonId != null) {
                var cancelButton = $("#" + cancelButtonId);
                if (cancelButton != null) {
                    cancelButton.click();
                }
            }
    } });
    $('#' + panelName).dialog('open');
    $('#' + panelName).parent().appendTo($("form:first"));

    if (titleBarVisible.toLowerCase() == "false") {
        $(".ui-dialog-titlebar").hide();
    }

    if (closeButtonVisible.toLowerCase() == "false") {
        $(".ui-dialog-titlebar-close").hide();
    }
}

/*******************************************************************************
*   Функция для закрытия диалогов на основе jQuery UI
*   (Таймер необходим для корректной работы в IE8)
*******************************************************************************/
function closeDialog(panelName) {
    setTimeout(function () {
        try
        {
            $('#' + panelName).dialog('close');
        }
        catch (e) {
            var message = 'Элемент [' + panelName + '] НЕ должен лежать на UpdatePanel, которая обновляется до его закрытия методом closeDialog. Исключение: [' + e + ']';
            log(message);
            return true;
        }
        return true;
    }, 10);
    return true;
}

/*******************************************************************************
*   Функция возвращает позицию курсора в текстовом окне
*******************************************************************************/
function doGetCaretPosition(ctrl) {
    var caretPos = 0; 
    if (document.selection) {
        ctrl.focus();
        var sel = document.selection.createRange();
        sel.moveStart('character', -ctrl.value.length);
        caretPos = sel.text.length;
    }else if (ctrl.selectionStart || ctrl.selectionStart == '0')
        caretPos = ctrl.selectionStart;
    return caretPos;
}

/*******************************************************************************
*   Функция устанавливает позицию в текстовом окне
*******************************************************************************/
function setCaretPosition(ctrl, pos) {
    if (ctrl.setSelectionRange) {
        ctrl.focus();
        ctrl.setSelectionRange(pos, pos);
    }
    else if (ctrl.createTextRange) {
        var range = ctrl.createTextRange();
        range.collapse(true);
        range.moveEnd('character', pos);
        range.moveStart('character', pos);
        range.select();
    }
}

/********************************************************************************************************************  
*                                                                                                                   *
*   Функция для отображения вводимой информации в виде денежном формате                                                                                                                *
*                                                                                                                   *
*   Функция является обработчиком события onKeyDown текстового поля и приводит текст к формату 1 000 000 000,00     *
*   т.е. не дает вводить ничего кроме чисел и запятой, не дает вводить вторую запятую, делит число на триады и      *
*   после запятой дает ввести не более двух символов.                                                               *
*   При этом текстовое поле которому назаначен данный обработчик должно содержать следующие атрибуты:               *
*   CommaAllow - true/false - показывает разрешен ли ввод в данное текстовое поле запятой                           *
*   AfterCommaDigits - число знаков после запятой                                                                   *
*   Протестировано в IE7,IE8,IE9,IE10,Chrome 15.0.874.122,FireFox 20.0                                              *
*                                                                                                                   *
*********************************************************************************************************************/
function text2Currency(e) {
    var event = (e) ? e : window.event;
    var key = (event != 'undefined') ? event.keyCode : event.which;
    var controlId = (event.srcElement) ? event.srcElement.id : event.target.id;
    var commaAllow = $('#' + controlId).attr('CommaAllow');
    var afterCommaDigits = $('#' + controlId).attr('AfterCommaDigits');
    var minusAllow = $('#' + controlId).attr('MinusAllow');
    var returnValue = true;
    afterCommaDigits++;

    if ((key == 189 && minusAllow == 'true')    //если разрешен и нажат минус
        || (key == 188 && commaAllow == 'true') //разрешена и нажата запятая
        || key == 8                             //backspace 
        || key == 46                            // delete
        || key == 13                            // enter
        || (key >= 37 && key <= 40)             // стрелки
        || (key >= 48 && key <= 57)             // цифры
        || (key >= 96 && key <= 105)) {         // цифры на NumPad
        var text = $('#' + controlId).val().replace(/ /g, '');                                                  // убираем пробелы
        var textLength = (text.indexOf(',') < 0) ? text.length : text.indexOf(',');                             // вычисляем длину текста, либо длину текста до запятой 
        var count = 0;
        var index = 0;
        var newText = new Array();                                                                              // массив для хранения триад
        var newString = '';
        var pos = doGetCaretPosition(document.getElementById(controlId));                                       // текущая позиция курсора
        var spaceCount = ($('#' + controlId).val().substring(0, pos).split(' ').length - 1);                    // количество пробелов перед курсором

        if (!(key >= 37 && key <= 40)           // если не стрелки 
            && key != 189                       // не минус
            && key != 46                        // не delete
            && (text.indexOf('-') >= 0)         // минус в строке уже есть
            && (pos == 0)) {                    // курсор в начале строки
            e.returnValue = false;              // то не даем ничего вводить (перед уже стоящим в начале строки минусом)
            if (window.event) window.event.returnValue = false;
            if (e.preventDefault) e.preventDefault();
            return false;
        }

        if (key == 189                              // если минус
            && (text.indexOf('-') >= 0)) {          // минус уже есть 
            e.returnValue = false;                  // не даем вводить второй минус
            if (window.event) window.event.returnValue = false;
            if (e.preventDefault) e.preventDefault();
            return false;
        }

        if (key == 189                              // если минус
            && text.indexOf('-') < 0) {             // минуса в строке нет
            returnValue = (pos == 0);               // разрешаем ввести минус если курсор стоит в начале строки и выходим
            if (window.event) window.event.returnValue = returnValue;
            e.returnValue = returnValue;
            if (!returnValue && e.preventDefault) e.preventDefault();
            return returnValue;
        }
        
        if (key == 188                              // если запятая
            && (text.indexOf(',') >= 0)) {          // если запятая в стоке уже есть
            e.returnValue = false;                  // не даем вводить и выходим
            if (window.event) window.event.returnValue = false;
            if (e.preventDefault) e.preventDefault();
            return false;
        }
        if (text.indexOf(',') >= 0                  // если в строке есть запятая
            && pos >= text.indexOf(',')) {          // и курсор стоит после запятой
            if(((text.length - text.indexOf(',')) < afterCommaDigits)){ // если не превышено разрешенное количество символов после запятой
                returnValue = ((key >= 37 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105) || key == 46 || key == 8 || key == 13); // разрешаем вводить символы и управление
            }
            else returnValue = ((key >= 37 && key <= 40) || key == 8 || key == 46 || key == 13); // иначе только управляющие символы
                if (window.event) window.event.returnValue = returnValue;
                e.returnValue = returnValue;
                if (!returnValue && e.preventDefault) e.preventDefault();
                return returnValue;

        }
        if (text.indexOf(',') >= 0          // если запятая  
            && pos < text.indexOf(',')) {   // и курсор стоит до запятой
            newText[index++] = text.substring(text.indexOf(','), text.length); // сохраняем текст после запятой в массив триад
            text = text.substring(0, text.indexOf(','));                       // обрезаем текст
        }
        if (key != 188                              // если не запятая
            && !(key >= 37 && key <= 40)) {         // если не стрелки
                    returnValue = true;
                    if (textLength >= 3) {                              // если не первая триада
                        if (pos != (textLength + spaceCount)) {         // если курсор стоит не в конце текста
                            switch (key) {                              // вставляем в текст в нужную позицию новый символ
                                case 46://del
                                    text = text.substring(0, pos - spaceCount) + text.substring(((pos - spaceCount) + 1), textLength);
                                    textLength--;
                                    break;
                                case 8://backspace
                                    text = text.substring(0, (pos - spaceCount) - 1) + text.substring((pos - spaceCount), textLength);
                                    pos --;
                                    textLength--;
                                    break;
                                default:
                                    text = text.substring(0, pos - spaceCount) + String.fromCharCode(key) + text.substring(pos - spaceCount, textLength);
                                    textLength++;
                                    pos++;
                                    break;
                            }
                            for (var i = textLength; i > 0  ; i -= 3) {     // разбиваем строку на триады и складываем  массив
                                count = i - 3;
                                if (i < 0) i = 0;
                                newText[index++] = text.substring(count, i);
                            }
                            returnValue = false;
                        } else {                            // курсор в конце текста
                            switch (key) {
                                case 46://del
                                    break;
                                case 8://backspace
                                    for (var i = textLength-1; i > 0  ; i -= 3) {
                                        count = i - 3;
                                        if (i < 0) i = 0;
                                        newText[index++] = text.substring(count, i);
                                    }
                                    returnValue = false;
                                    break;
                                default:
                                    pos++;
                                    newText[index++] = text.substring(textLength - 2, textLength);
                                    text = text.substring(0, textLength - 2);
                                    if (text.length > 3) {
                                        for (var i = textLength - 2; i > 0  ; i -= 3) {
                                            count = i - 3;
                                            if (i < 0) i = 0;
                                            newText[index++] = text.substring(count, i);
                                        }
                                    } else {
                                        newText[index++] = text;
                                    }
                                    break;
                            }
                        }
                        
                        if (newText.length > 0) {                               // собираем новую строку из массива триад
                                for (var i = newText.length - 1; i >= 0; i--) {
                                    newString = newString + newText[i];
                                    if (i != 0) newString = newString + ' ';
                                }
                                $('#' + controlId).val(newString);              // присваиваем новую строку контролу
                            }
                        
                    } else { // если первая триада
                        if (key == 46){
                            text = text.substring(0, pos - spaceCount) + text.substring(((pos - spaceCount) + 1), textLength);
                        }
                        $('#' + controlId).val(text);
                    }
            }
        var newSpaceCount =  newString.substring(0, pos + spaceCount).split(' ').length - 1;
        if (!returnValue) setCaretPosition(document.getElementById(controlId), pos + (newSpaceCount-spaceCount));
        e.returnValue = returnValue;
        if (window.event) window.event.returnValue = returnValue;
        return returnValue;
    } else {
        if (window.event) window.event.returnValue = false;
        e.returnValue = false;
        if (e.preventDefault) e.preventDefault();
        return false;
    }
}

/*******************************************************************************************************************
*   Функция для закрытия диалога создания нового этапа в филде календарного плана
*******************************************************************************************************************/
function closeCalendarPlanFieldDialog(panelName, text1, text2, text3, text4) {
    setTimeout(function () {
        try {
            if ($('#' + text1).val() != '' && $('#' + text2).val() != '' && $('#' + text3).val() != '' && $('#' + text4).val() != '') {
                $('#' + panelName).dialog('close');
            }
        }
        catch (e) {
            alert(e);
            return true;
        }
        return true;
    }, 10);
    return true;
}

/*******************************************************************************************************************
*   Функция для отмены срабатывания клавиши Enter на диалоговых окнах jQuery. 
*   (В браузере FF такое нажатие приводит к генерации неизвестного события и появлению алерта с сообщением об ошибке.)
*******************************************************************************************************************/
function disableEnterKey(dialogName){
    $('#' + dialogName + ' input[type=text]').keydown(function (event) {
        if (event.keyCode == 13) {
            return false;
        }
    });
}

/*******************************************************************************
*   Скрытие контрола
*******************************************************************************/
function setHiddenControl(objectName, parentName) {
    var $obj = null;
    if (parentName > "")
        $obj = $('#' + parentName).find('#' + objectName);
    if ($obj == null || !($obj.length) > 1)
        $obj = $('#' + objectName);
    if ($obj)
        $obj.hide();
}

/*******************************************************************************
*   Показать звездочку как признак обязательность заполнения соответствующего ей контрола
*******************************************************************************/
function setRequiredControl(objectName, parentName) {
    var $obj = null;
    if (parentName > "")
        $obj = $('#' + parentName).find('#' + objectName);
    if ($obj == null || !($obj.length) > 1)
        $obj = $('#' + objectName);
    if ($obj)
        $obj.removeClass("hidden");
}

/*******************************************************************************************************************
*   Функция для отключения кнопки "Сохранить" в случае, если форма прошла валидацию.  
*   (Необходима для того, чтобы невозможно было нажать кнопку "Сохранить" несколько раз подряд.)
*******************************************************************************************************************/
function disableIfValid(buttonName, validationGroup) {
    var isValid = true;
    if (typeof (Page_ClientValidate) == 'function')
        isValid = Page_ClientValidate(validationGroup);
    if (isValid) {
        setTimeout(function () {
            $('#' + buttonName).attr('disabled', 'disabled');
        }, 1);
    }
    return true;
}

/*******************************************************************************
*   Функция возвращает имя страницы со слешами замененными подчеркиванием.
*   (Используется для генерации уникальных имен куков)
*******************************************************************************/
function getPageName() {
    var str = window.location.pathname;
    return str.replace(new RegExp('/','g'), '_');
}

/*******************************************************************************
*   Добавляет в куки уникальное для данной страницы значение
*******************************************************************************/
function add2Cookie(name, value) {
    name = getPageName() + '_' + name;
    name = hex_md5(name);
    $.cookie(name, value, { expires: 30 });
}

/*******************************************************************************
*   Читает из кук уникальное для данной страницы значение
*******************************************************************************/
function getFromCookie(name) {
    name = getPageName() + '_' + name;
    name = hex_md5(name);
    return $.cookie(name);
}