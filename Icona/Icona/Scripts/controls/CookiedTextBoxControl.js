/*
*   Функция для сохранения значения текстбокса в куках
*/
function cookiedTextBoxOnChange(textbox) {
    add2Cookie(textbox.id+"_Text", textbox.value);
}