using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Elar.Framework.Core.Extensions;
using Icona.Logic.Extensions;

namespace Icona.Usercontrols
{
    [ParseChildren(true)]
    public partial class ToolsContainerControl : BaseControl
    {
        #region События

        /// <summary>
        /// Событие очищения фильтра
        /// </summary>
        public event EventHandler ClearFilter;

        /// <summary>
        /// Событие применения фильтра
        /// </summary>
        public event EventHandler ApplyFilter;

        /// <summary>
        /// Событие перехода на другую страницу
        /// </summary>
        public event EventHandler PageChanged;

        #endregion

        #region Свойства

        /// <summary>
        /// Скрыт
        /// </summary>
        public bool Hidden
        {
            get { return !ToolsContainer.Visible; }
            set
            {
                ToolsContainer.Visible = !value;
                MenuUPnl.Update();
            }
        }

        /// <summary>
        /// Скрывать постраничный переход
        /// </summary>
        public bool HidePagingControl { get; set; }

        /// <summary>
        /// Скрытие кнопки "Фильтр"
        /// </summary>
        public bool HideFilterButton { get; set; }

        /// <summary>
        /// Отображать ссылку "Вернуться к списку"
        /// </summary>
        public bool ShowReturnButton
        {
            get { return ReturnBtn.Visible; }
            set { ReturnBtn.Visible = value; }
        }

        /// <summary>
        /// Текст ссылки "Вернуться к списку"
        /// </summary>
        public string ReturnButtonText
        {
            get { return ReturnBtn.Text; }
            set { ReturnBtn.Text = value; }
        }

        /// <summary>
        /// Адрес ссылки "Вернуться к списку"
        /// </summary>
        public string ReturnButtonUrl
        {
            get { return ReturnBtn.PostBackUrl; }
            set { ReturnBtn.PostBackUrl = value; }
        }

        /// <summary>
        /// Постраничный переход
        /// </summary>
        public Icona.Usercontrols.PagingControl Paging
        {
            get { return PagingControl; }
        }

        /// <summary>
        /// Кнопки
        /// </summary>
        public RepeatedControls Buttons => ButtonControls;

        /// <summary>
        /// Шаблонные контролы для вывода в левой колонке
        /// </summary>
        [TemplateContainer(typeof(UserControl)), PersistenceMode(PersistenceMode.InnerProperty), TemplateInstance(TemplateInstance.Single)]
        public ITemplate FilterLeftColumnTemplateControls { get; set; }

        /// <summary>
        /// Шаблонные контролы для вывода в правой колонке
        /// </summary>
        [TemplateContainer(typeof(UserControl)), PersistenceMode(PersistenceMode.InnerProperty), TemplateInstance(TemplateInstance.Single)]
        public ITemplate FilterRightColumnTemplateControls { get; set; }

        /// <summary>
        /// Шаблон кнопок
        /// </summary>
        [TemplateContainer(typeof(UserControl)), PersistenceMode(PersistenceMode.InnerProperty), TemplateInstance(TemplateInstance.Single)]
        public ITemplate ButtonsTemplate { get; set; }

        /// <summary>
        /// Шаблон информации сверху
        /// </summary>
        [TemplateContainer(typeof(UserControl)), PersistenceMode(PersistenceMode.InnerProperty), TemplateInstance(TemplateInstance.Single)]
        public ITemplate TopInfoTemplate { get; set; }

        /// <summary>
        /// Шаблон информации
        /// </summary>
        [TemplateContainer(typeof(UserControl)), PersistenceMode(PersistenceMode.InnerProperty), TemplateInstance(TemplateInstance.Single)]
        public ITemplate InfoTemplate { get; set; }

        /// <summary>
        /// Шаблон информации справа
        /// </summary>
        [TemplateContainer(typeof(UserControl)), PersistenceMode(PersistenceMode.InnerProperty), TemplateInstance(TemplateInstance.Single)]
        public ITemplate InfoRightTemplate { get; set; }

        /// <summary>
        /// Показывать фильтр принудительно
        /// </summary>
        private bool ForceFilterShow
        {
            get
            {
                if (ViewState["ForceFilterShow"] == null)
                    return false;
                else
                    return (bool)ViewState["ForceFilterShow"];
            }
            set
            {
                ViewState["ForceFilterShow"] = value;
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Проверка на принудительное отображение фильтра 
        /// в зависимости от количества отображаемых записей в отфильтрованном списке
        /// </summary>
        /// <param name="totalRecords">Количество записей в отображаемой выборке</param>
        public void CheckForceFilterShow(int totalRecords)
        {
            ForceFilterShow = totalRecords == 0;
        }

        /// <summary>
        /// Создание дочерних контролов
        /// </summary>
        protected override void CreateChildControls()
        {
            InstantiateControls();
            SetControlAttributes(false);
        }

        /// <summary>
        /// Инстанциация контролов
        /// </summary>
        private void InstantiateControls()
        {
            if (FilterLeftColumnTemplateControls != null)
            {
                FilterLeftColumnTemplateControls.InstantiateIn(LeftColumn);
            }
            if (FilterRightColumnTemplateControls != null)
            {
                FilterRightColumnTemplateControls.InstantiateIn(RightColumn);
            }
            if (ButtonsTemplate != null)
            {
                ButtonControls.Controls.Clear();
                ButtonsTemplate.InstantiateIn(ButtonControls);
            }
            if (TopInfoTemplate != null)
            {
                TopInfoControls.Controls.Clear();
                TopInfoTemplate.InstantiateIn(TopInfoControls);
            }
            TopInfoDiv.Visible = TopInfoControls.HasControls();
            if (InfoTemplate != null)
            {
                InfoControls.Controls.Clear();
                InfoTemplate.InstantiateIn(InfoControls);
            }
            if (InfoRightTemplate != null)
            {
                InfoRightControls.Controls.Clear();
                InfoRightTemplate.InstantiateIn(InfoRightControls);
            }
        }

        /// <summary>
        /// Установка атрибуты контролов
        /// </summary>
        /// <param name="hideSectionsWithInvisibleControls">Прятать секции с невидимыми контролами</param>
        private void SetControlAttributes(bool hideSectionsWithInvisibleControls)
        {
            if (TopInfoTemplate != null)
            {
                bool controlsInvisible = IsControlsInvisible(TopInfoControls.Controls);
                TopInfoDiv.Visible = !(hideSectionsWithInvisibleControls && controlsInvisible);
            }

            if (HideFilterButton || (FilterLeftColumnTemplateControls == null && FilterRightColumnTemplateControls == null))
            {
                FilterBtn.Visible = filterPnl.Visible = false;
                filterPnl.ID = "filterVisiblePnl";
            }
            else
            {
                FilterBtn.Visible = filterPnl.Visible = true;
                FilterBtn.DataBind();

                FilterRightColumn.Visible = RightColumn.HasControls();
            }

            PagingControl.Visible = !HidePagingControl;

            if (ShowReturnButton)
            {
                ReturnButtonUrl = Page.GetReturnUrl(ReturnButtonUrl);
                if (!ReturnButtonUrl.NotEmpty())
                    ShowReturnButton = false;
            }
        }

        /// <summary>
        /// Все контролы этой группы невидимы (или это пустые литералы)
        /// </summary>
        /// <param name="controls">Контролы</param>
        /// <returns></returns>
        private bool IsControlsInvisible(ControlCollection controls)
        {
            if (controls == null || controls.Count == 0) return true;

            IEnumerable<Control> topInfoControls = TopInfoControls.Controls.Cast<Control>();
            IEnumerable<LiteralControl> literals = topInfoControls.Select(c => c as LiteralControl).Where(c => c != null);
            bool literalsInvisible = literals.All(c => !c.Visible || !c.Text.NotEmpty(false, false));
            bool commonControlsInvisible = topInfoControls.All(c => literals.Contains(c) || !c.Visible);
            return literalsInvisible && commonControlsInvisible;
        }

        /// <summary>
        /// Обновление панели инструментов
        /// </summary>
        /// <param name="hideSectionsWithInvisibleControls">Прятать секции с невидимыми контролами</param>
        public void Refresh(bool hideSectionsWithInvisibleControls = true)
        {
            MenuUPnl.Update();
            //Ручная установка атрибутов, т.к. Update панели не порождает вызова CreateChildControls 
            SetControlAttributes(hideSectionsWithInvisibleControls);
        }

        #endregion

        #region Обработчики событий

        protected void Page_Load(object sender, EventArgs e)
        {
            bool hideFilterPanel = ApplyFilterBtn.GetFromCookie(Request, "filterPanelState") != "shown";
            filterPnl.Style.Add("display", hideFilterPanel ? "none" : "block");

            // Принудительно показываем фильтр
            if (ForceFilterShow)
            {
                filterPnl.Style.Add("display", "block");
                Page.Script($"add2Cookie('{ApplyFilterBtn.ClientID}_{"filterPanelState"}','{"shown"}');");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            EnsureChildControls();
        }

        protected void ClearFilterBtn_OnClick(object sender, EventArgs e)
        {
            if (ClearFilter != null)
                ClearFilter(this, EventArgs.Empty);

            Refresh();
        }

        protected void ApplyFilterBtn_OnClick(object sender, EventArgs e)
        {
            PagingControl.CurrentPage = 1;
            if (ApplyFilter != null)
                ApplyFilter(this, EventArgs.Empty);
        }

        protected void PagingControl_Reload(object sender, EventArgs e)
        {
            if (PageChanged != null)
                PageChanged(this, EventArgs.Empty);
        }

        #endregion
    }

}