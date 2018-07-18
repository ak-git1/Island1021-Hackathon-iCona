using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Elar.Framework.Core.Extensions;
using Elar.Framework.Core.Helpers;
using Telerik.Web.UI;

namespace Icona.Logic.Extensions
{
    /// <summary>
    /// Расширения для элементов управления
    /// </summary>
    public static class ControlsExtensions
    {
        /// <summary>
        /// Установить выбранный элемент выпадающего списка.
        /// </summary>
        /// <param name="dropDownList">Выпадающий список</param>
        /// <param name="value">Значение</param>
        public static void SetSelectedValue(this DropDownList dropDownList, object value)
        {
            dropDownList.ClearSelection();
            if (value != null && value != DBNull.Value)
            {
                string valueStr = value.ToString();
                if (dropDownList.Items.Cast<ListItem>().Any(x => x.Value == valueStr))
                    dropDownList.SelectedValue = valueStr;
            }
        }

        /// <summary>
        /// Установить выбранный элемент выпадающего списка.
        /// </summary>
        /// <param name="dropDownList">Выпадающий список</param>
        /// <param name="value">Значение</param>
        public static void SetSelectedValue(this RadComboBox dropDownList, object value)
        {
            dropDownList.ClearSelection();
            if (value != null && value != DBNull.Value)
            {
                string valueStr = value.ToString();
                if (dropDownList.Items.Any(x => x.Value == valueStr))
                    dropDownList.SelectedValue = valueStr;
            }
        }

        /// <summary>
        /// Заполнение выпадающего списка на основе инумератора
        /// </summary>
        /// <typeparam name="T">Тип инумератора</typeparam>
        /// <param name="dropDownList">Выпадающий список</param>
        /// <param name="insertNotSetValue">Вставлять нулевое значение</param>
        public static void FillFromEnum<T>(this DropDownList dropDownList, bool insertNotSetValue = false)
        {
            if (typeof(T).IsEnum)
            {                
                dropDownList.Items.Clear();
                List<ListItem> items = EnumNamesHelper.EnumToList<T>();
                dropDownList.DataSource = items;
                dropDownList.DataTextField = "Text";
                dropDownList.DataValueField = "Value";
                dropDownList.DataBind();                
                
                if (insertNotSetValue && items.All(i=>i.Value.NotEmpty(false)))
                    dropDownList.Items.Insert(0, new ListItem("Не указано", string.Empty));            
            }
            else
                throw new Exception("Параметр T должен быть инумератором");
        }

        /// <summary>
        /// Заполнение группы флажков на основе инумератора
        /// </summary>
        /// <typeparam name="T">Тип инумератора</typeparam>
        /// <param name="checkBoxList">Группа флажков</param>
        /// <param name="withoutZeroValue">Не использовать нулевое значение перечисления</param>
        public static void FillFromEnum<T>(this CheckBoxList checkBoxList, bool withoutZeroValue)

        {
            if (typeof(T).IsEnum)
            {
                checkBoxList.Items.Clear();
                ListItem[] items = EnumNamesHelper.EnumToList<T>()
                    .Where(i => !withoutZeroValue || i.Value.ToInt32() != 0)
                    .ToArray();
                checkBoxList.DataSource = items;
                checkBoxList.DataTextField = "Text";
                checkBoxList.DataValueField = "Value";
                checkBoxList.DataBind();
            }
            else
                throw new Exception("Параметр T должен быть инумератором");
        }

        /// <summary>
        /// Заполнение выпадающего списка на основе списка значений инутмератора
        /// </summary>
        /// <param name="dropDownList">Выпадающий список</param>
        /// <param name="list">Список значений инумератора</param>
        /// <param name="insertNotSetValue">Вставлять нулевое значение</param>
        public static void FillFromEnumList(this DropDownList dropDownList, IEnumerable<Enum> list, bool insertNotSetValue = false)
        {
            dropDownList.Items.Clear();
            dropDownList.DataSource = list.Select(e => EnumNamesHelper.GenerateListItem(e));
            dropDownList.DataTextField = "Text";
            dropDownList.DataValueField = "Value";
            dropDownList.DataBind();

            if (insertNotSetValue)
                dropDownList.Items.Insert(0, new ListItem("Не указано", string.Empty));
        }

        /// <summary>
        /// Установить выбранный элемент выпадающего списка.
        /// </summary>
        /// <param name="dropDownList">Выпадающий список</param>
        /// <param name="text">Значение</param>
        public static void SetSelectedText(this DropDownList dropDownList, object text)
        {
            string valueStr = (text == null) ? null : text.ToString();
            if (dropDownList.Items.Cast<ListItem>().Any(x => x.Text == valueStr))
                dropDownList.SelectedValue = valueStr;
        }

        /// <summary>
        /// Делает кнопку неактивной после нажатия в случае, если страница валидна. Защита от двойного нажатия при создании объектов. 
        /// </summary>
        /// <param name="btn">Кнопка</param>
        public static void DisableIfPageValid(this Button btn)
        {
            btn.Attributes.Add("onclick", String.Format("return disableIfValid('{0}', '{1}');", btn.ClientID, btn.ValidationGroup));
        }

        /// <summary>
        /// Добавляет в куки параметр уникальный для данного контрола
        /// (Этот метод срабатывает только, если после изменения куков идет постбек, т.е. не во всех случаях. 
        /// Используйте клиентские методы типа: Page.Script(String.Format("add2Cookie('{0}_{1}','{2}')", uc.ClientID,name,value));)
        /// </summary>
        /// <param name="uc">Контрол</param>
        /// <param name="name">Название</param>
        /// <param name="value">Значение</param>
        public static void AddToCookie(this UserControl uc, string name, string value)
        {
            name = String.Format("{0}_{1}_{2}", uc.Request.Path.Replace("/", "_"), uc.ClientID, name);
            name = name.ToMD5();
            HttpCookie cookie = new HttpCookie(name, value)
                                    {
                                        Expires = DateTime.Now.AddDays(30)
                                    };
            if (string.IsNullOrEmpty(value))
                uc.Response.Cookies.Remove(cookie.Name);
            else
                uc.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Читает из кук параметр уникальный для данного контрола
        /// </summary>
        /// <param name="uc">Контрол</param>
        /// <param name="name">Название</param>
        /// <returns></returns>
        public static string GetFromCookie(this UserControl uc, string name)
        {
            return GetFromCookie(uc, uc.Request, name);
        }

        /// <summary>
        /// Читает из кук параметр уникальный для данного контрола
        /// </summary>
        /// <param name="control">Контрол</param>
        /// <param name="request">Запрос</param>
        /// <param name="name">Название</param>
        /// <returns></returns>
        public static string GetFromCookie(this Control control, HttpRequest request, string name)
        {
            string s = $"{request.Path.Replace("/", "_")}_{control.ClientID}_{name}";
            s = s.ToMD5();
            HttpCookie cookie = request.Cookies.Get(s);
            return (cookie != null) ? (cookie.Value ?? string.Empty).UrlDecode() : string.Empty;
        }

        /// <summary>
        /// Установка состояния для чтения.
        /// </summary>
        /// <param name="userControl">Элемент управления.</param>
        /// <param name="readOnly">Только для чтения.</param>
        public static void SetReadOnly(this UserControl userControl, bool readOnly)
        {
            List<FieldInfo> fields = userControl.GetType().
                GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).
                Where(x => x.FieldType == typeof (TextBox) ||
                           x.FieldType == typeof (DropDownList) ||
                           x.FieldType == typeof (CheckBox) ||
                           x.FieldType == typeof(CalendarExtender) ||
                           x.FieldType == typeof (RadNumericTextBox)).
                ToList();

            foreach (FieldInfo field in fields.Where(
                x => x.FieldType == typeof (DropDownList) ||
                     x.FieldType == typeof (CheckBox)))
            {
                WebControl webControl = (WebControl) field.GetValue(userControl);
                webControl.Enabled = !readOnly;
            }

            foreach (FieldInfo field in fields.Where(x => x.FieldType == typeof(CalendarExtender)))
            {
                CalendarExtender calendar = (CalendarExtender)field.GetValue(userControl);
                calendar.Enabled = !readOnly;
            }

            foreach (FieldInfo field in fields.Where(x => x.FieldType == typeof (TextBox)))
            {
                TextBox textBox = (TextBox)field.GetValue(userControl);
                textBox.ReadOnly = readOnly;
            }

            foreach (FieldInfo field in fields.Where(x => x.FieldType == typeof(RadNumericTextBox)))
            {
                RadNumericTextBox radNumericTextBox = (RadNumericTextBox)field.GetValue(userControl);
                radNumericTextBox.ReadOnly = readOnly;
            }
        }

        /// <summary>
        /// Добавление css-класса
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="className"></param>
        public static void AddCssClass(this WebControl ctrl, string className)
        {
            ctrl.CssClass = string.Format(" {0} ", ctrl.CssClass);
            if (!ctrl.CssClass.Contains(string.Format(" {0} ", className)))
                ctrl.CssClass += className;
            ctrl.CssClass = ctrl.CssClass.Trim();
        }
    }
}