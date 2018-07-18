/*
*       Функция для сохранения в скрытом поле значения выбранного элемента выпадающего списка
*/
function cookiedDDLOnChange(ddl, hdn) {
    $('#' + hdn).val(ddl.value);
}