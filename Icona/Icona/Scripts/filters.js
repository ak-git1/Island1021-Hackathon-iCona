/*******************************************************************************
*                                   Функции для отображения/скрытия панели фильтров
*
*   Элемент div с идентификатором filterPnl содержит панель фильтров
*   Невидимый элемент div с идентификатором visiblePnl содержит значение val в котором хранится текущее состояние панели фильтров
*   Важно: элемент visiblePnl должен находиться ВНЕ элементов UpdatePanel, иначе его значение val будет затираться при коллбеках (в том числе и с других UpdatePanel)
*   Кроме того, в искусственном атрибуте applyButtonName должно храниться клиентское имя кнопки 'Применить' для переключения на нее скриптами фокуса при нажатии кнопки Enter
*   Новое: теперь состояние панели фильтров хранится в куках уникальных для каждой страницы.
*******************************************************************************/

function GridFilter(container) {
    this.$filterPnl = null;
    this.$applyButton = null;

    this.initialize = function () {
        var $container = $(container);
        var $filterBadge = $container.find('.filter-badge');
        $container.find('input')[0].gridFilter = this;
        this.$filterPnl = this.findFilterPanel($container);
        this.$applyButton = this.findApplyButton($container);
        this.$filterPnl.find('input[type=text]').attr('applyFilterButtonId', this.$applyButton[0].id).keydown(function (event) {
            if (event.keyCode == 13) {
                $('#' + this.getAttribute('applyFilterButtonId')).focus().click();
                return false;
            }
        });
        var filterItems = fullFilterItems(this.$filterPnl);
        if (filterItems > 0) {
            $filterBadge.addClass('filter-bage-visible');
            $filterBadge.html(filterItems);
        } else {
            $filterBadge.removeClass('filter-bage-visible');
        }
    };

    this.findApplyButton = function ($container) {
        var $visiblePnl = $container.find('#visiblePnl');
        if ($visiblePnl == null || $visiblePnl.length < 1) {
            alert('На странице отсутcтвует обязательный элемент visiblePnl');
            return null;
        }
        return $('#' + $visiblePnl.attr('applyButtonName'));;
    }

    this.findFilterPanel = function ($container) {
        var $gridContainer = $container.closest('.grid-container');
        var $filterPnl = null;
        do {
            if ($gridContainer != null && $gridContainer.length > 0)
                $filterPnl = $gridContainer.find('#filterPnl');
        } while ($filterPnl == null && ($gridContainer = $container = $container.parent()));

        if ($filterPnl == null)
            $filterPnl = $('#filterPnl');
        if ($filterPnl == null || $filterPnl.length < 1) {
            alert('На странице отсутcтвует обязательный элемент filterPnl');
        }
        return $filterPnl;
    }

    this.toggleFilterPanel = function (init) {
        init = (init === true);
        var id = this.$applyButton[0].id + '_filterPanelState';
        var filterPanelState = getFromCookie(id);
        var visible = (filterPanelState == 'shown');
        if (!init)
            visible = !visible;
        if (visible) {
            if (init)
                this.$filterPnl.show();
            else {
                this.$filterPnl.slideDown(300);
                add2Cookie(id, 'shown');
            }
        } else {
            if (init)
                this.$filterPnl.hide();
            else {
                this.$filterPnl.slideUp(300);
                add2Cookie(id, 'hidden');
            }
        }
    };

    this.initialize();
}

/*******************************************************************************
*   Добавление обработчиков нажатия клавиши Enter всем текстовым окнам фильтра
*******************************************************************************/
function InitializeGridFilter() {
    $('.filter-badge').each(function () {
        var node = this.parentNode;
        var gf = new GridFilter(node);
        var button = $(node).find("#filterButton");
        if (button.length) {
            button[0].gridFilter = gf;
        }
    });

    filter_multiselect_click();
}

$(function () {
    InitializeGridFilter();
});

/*******************************************************************************
*   Создание обработчика события завершения коллбека UpdatePanel
*******************************************************************************/
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler = function (sender, args) {
    InitializeGridFilter();
});

/*******************************************************************************
*   Создание обработчика события selectedItemsChange, вызываемого при обновлении
    выделенных элементов после завершения коллбека UpdatePanel
*******************************************************************************/
$(document).on("selectedItemsChange","div[id='filter_DDL_names']", function () {
    InitializeGridFilter();
});

/*******************************************************************************
*   Функция показывающая/скрывающая панель фильтров
*******************************************************************************/
function toggleFilterPanel(button) {
    if (typeof (button) == "undefined" || button == null) {
        var evt = window.event || arguments.callee.caller.arguments[0];
        if (evt != null) {
            button = evt.srcElement || evt.target;
        }
    }
    if (!button) return;
    var gf = button.gridFilter;
    if (!gf) {
        gf = new GridFilter(button.parentNode);
        button.gridFilter = gf;
    }
    gf.toggleFilterPanel();
}

/*******************************************************************************
*       Функция возвращает количество элементов панели фильтров в которых есть какое либо значение
********************************************************************************/
function fullFilterItems($filterPnl) {
    if (typeof ($filterPnl) == "undefined" || $filterPnl == null) {
        $filterPnl = $('#filterPnl');
    }
    if ($filterPnl == null || $filterPnl.length < 1)
        return 0;

    // Счетчик заполненных элементов
    var count = 0;
    // Перечисляем текстовые окна, считаем те в которых есть текст
    var filterElements = $filterPnl.find('input[type=text]');
    for (var i = 0; i < filterElements.length; i++) {
        if (filterElements.eq(i).val() != '')
            count++;
    }

    //Перечисляем выпадающие списки считаем те у которых значения отличны от незаполненных
    filterElements = $filterPnl.find('select');
    for (var i = 0; i < filterElements.length; i++) {
        if (filterElements.eq(i).val() == '0'
            || filterElements.eq(i).val() == 'Не указано'
            || filterElements.eq(i).find('option:selected').text() == 'Не указан'
            || filterElements.eq(i).val() == null
            || filterElements.eq(i).val() == 'null'
            || filterElements.eq(i).val() == ''
            || filterElements.eq(i).val() == '00000000-0000-0000-0000-000000000000')
            continue;
        else
            count++;
    }
    // Перечисляем телериковские мультиселектовый контролы и вытаскиваем для каждого их них его значение из кук
    filterElements = $filterPnl.find("div[id='filter_DDL_names']");    
    for (var i = 0; i < filterElements.length; i++) {
        var name = filterElements.eq(i).attr('ddlname') + '_SelectedValues';
        name = name.substring(name.indexOf('_', 0) + 1, name.length);
        var value = getFromCookie(name);
        if (value != '' && value != null) {
            count += value.split(',').length;
        }
    }

    return count;
}
/*******************************************************************************
*       Функция для снятия выбора с родительского узла при снятии выбора с одного из дочерних узлов в телериковском мультиселектовом контроле
********************************************************************************/
function filter_multiselect_click() {
   $('.rddtPopup li.rtLI ul.rtUL input[type="checkbox"]').click(function () {
        if (!this.checked) {
            //элемент, с которого был снят выбор
            var self = this;
            //родительские элементы с установленным выбором
            var roots = new Array();

            var check = this;
            var $parent;
            //поиск родительских элементов, с которых нужно снять выбор
            while (($parent = $(check).closest('ul.rtUL').closest('li.rtLI').find('input[type="checkbox"]:first')) != null && $parent.length > 0) {
                check = $parent[0];
                if (check.checked) {
                    roots.push(check);
                }
            }

            if (roots.length > 0) {
                //корневой элемент
                var root = roots[roots.length - 1];

                //все выбранные элементы у корневого
                var $checkList = $(root).closest('li.rtLI').find('ul input[type="checkbox"]:checked');

                //снятие выбора (со всех)
                root.click();

                //установить корректное состояние 
                self.click();
                self.checked = false;

                //снова отметить выбранные ранее элементы
                $checkList.each(function() {
                    if (!this.checked && self != this) {
                        for (var i = 0; i < roots.length; i++) {
                            if (this == roots[i]) {
                                return;
                            }
                        }
                        this.click();
                    }
                });
            }
        }
    });
}
