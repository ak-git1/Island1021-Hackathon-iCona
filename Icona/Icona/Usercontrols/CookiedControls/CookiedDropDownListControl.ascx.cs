using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Icona.Logic.UI;

namespace Icona.Usercontrols.CookiedControls
{
    /// <summary>
    /// Контрол сохраняющий значение выпадающего списка в куках. Предназначен для использования в панелях фильтров.
    /// </summary>
    [ParseChildren(typeof(ListItemCollection), DefaultProperty = "Items", ChildrenAsProperties = true)]
    public partial class CookiedDropDownListControl : UserControl
    {
        #region Свойства

        /// <summary>
        /// CSS
        /// </summary>
        public string CssClass
        {
            get
            {
                return DropDownList.CssClass;
            }
            set
            {
                DropDownList.CssClass = value;
            }
        }

        /// <summary>
        /// Сохранять ли содержимое текстового окна в куках
        /// </summary>
        public bool SaveValueToCookie { get; set; }

        /// <summary>
        /// Значение текстового поля
        /// </summary>
        public string DataTextField
        {
            set
            {
                DropDownList.DataTextField = value;
            }
        }

        /// <summary>
        /// Значение поля данных
        /// </summary>
        public string DataValueField
        {
            set
            {
                DropDownList.DataValueField = value;
            }
        }

        /// <summary>
        /// Источник данных
        /// </summary>
        public object DataSource
        {
            set
            {
                DropDownList.DataSource = value;
            }
        }

        /// <summary>
        /// Выбранный элемент
        /// </summary>
        public ListItem SelectedItem
        {
            get
            {
                return DropDownList.SelectedItem;
            }
        }

        /// <summary>
        /// Индекс выбранного элемента
        /// </summary>
        public int SelectedIndex
        {
            set
            {
                if (DropDownList.Items.Count > 0)
                {
                    DropDownList.SelectedIndex = value;
                    hdnDropDownListValue.Value = DropDownList.SelectedValue;
                    if (SaveValueToCookie)
                    {
                        CookiedControlsValueManager.Instance.SaveValue(this, DropDownList.SelectedValue);
                    }
                }
            }
            get
            {
                return DropDownList.SelectedIndex;
            }
        }

        /// <summary>
        /// Доступен
        /// </summary>
        public bool Enabled
        {
            get { return DropDownList.Enabled; }
            set { DropDownList.Enabled = value; }
        }

        /// <summary>
        /// Коллекция элементов списка
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ListItemCollection Items
        {
            get
            {
                return DropDownList.Items;
            }
        }

        /// <summary>
        /// Выбранное значение
        /// </summary>
        public string SelectedValue
        {
            get { return DropDownList.SelectedValue; }
            set { DropDownList.SelectedValue = value; }
        }

        /// <summary>
        /// AutoPostBack
        /// </summary>
        public bool AutoPostBack
        {
            get { return DropDownList.AutoPostBack; }
            set { DropDownList.AutoPostBack = value; }
        }

        /// <summary>
        /// Ширина
        /// </summary>
        public Unit Width
        {
            get
            {
                return DropDownList.Width;
            }
            set
            {
                DropDownList.Width = value;
            }
        }

        /// <summary>
        /// При создании контрола заполнить список простым списком типа Не указано/Согласовано/Не согласовано
        /// </summary>
        public bool CreateSimpleList { get; set; }

        /// <summary>
        /// Строка положительного значения элемента списка
        /// </summary>
        public string PositiveSimpleListString { get; set; }

        /// <summary>
        /// Строка отрицательного значения элемента списка
        /// </summary>
        public string NegativeSimpleListString { get; set; }

        #endregion

        #region Публичные методы

        /// <summary>
        /// Привязка источника данных
        /// </summary>
        /// <param name="firstListItem">Первый элемент</param>
        public void DataBind(ListItem firstListItem = null)
        {
            DropDownList.DataBind();

            if (firstListItem != null)
                DropDownList.Items.Insert(0, firstListItem);

            if (SaveValueToCookie)
            {
                string selectedValue = Page.IsPostBack ? hdnDropDownListValue.Value : CookiedControlsValueManager.Instance.GetValue(this);
                if (DropDownList.Items.FindByValue(selectedValue) != null)
                    DropDownList.SelectedValue = hdnDropDownListValue.Value = selectedValue;
            }
        }

        /// <summary>
        /// Сбросить выбранный элемент
        /// </summary>
        public void ClearSelection()
        {
            SelectedIndex = 0;
        }

        #endregion

        #region События

        /// <summary>
        /// Событие измненеия активного элемента списка
        /// </summary>
        public event EventHandler Changed;

        #endregion

        #region Обработчики событий

        protected override void OnInit(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (CreateSimpleList)
                {
                    DropDownList.Items.Add(new ListItem("Не указано", String.Empty));
                    DropDownList.Items.Add(new ListItem(String.IsNullOrEmpty(PositiveSimpleListString) ? "Да" : PositiveSimpleListString, "true"));
                    DropDownList.Items.Add(new ListItem(String.IsNullOrEmpty(NegativeSimpleListString) ? "Нет" : NegativeSimpleListString, "false"));
                }

                if (SaveValueToCookie)
                {
                    DropDownList.Attributes.Add("onchange", $"cookiedDDLOnChange(this, '{hdnDropDownListValue.ClientID}')");

                    string selectedValue = CookiedControlsValueManager.Instance.GetValue(this);
                    if (DropDownList.Items.FindByValue(selectedValue) != null)
                        DropDownList.SelectedValue = hdnDropDownListValue.Value = selectedValue;
                }
            }
            base.OnInit(e);
        }



        protected override void OnLoad(EventArgs e)
        {
            if (SaveValueToCookie)
            {
                if (Page.IsPostBack)
                    CookiedControlsValueManager.Instance.SaveValue(this, hdnDropDownListValue.Value);
            }

            base.OnLoad(e);
        }

        #endregion


        protected void DropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (Changed != null)
                Changed(sender, e);
        }
    }

}