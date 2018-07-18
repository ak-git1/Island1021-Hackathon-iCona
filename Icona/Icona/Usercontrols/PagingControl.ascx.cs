using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elar.Framework.Core.Extensions;
using Icona.Logic.Extensions;
using Icona.Logic.Interfaces;

namespace Icona.Usercontrols
{
    /// <summary>
    /// Элемент управления для постраничного отображения записей.
    /// </summary>
    public partial class PagingControl : UserControl, IPaging
    {
        #region События

        /// <summary>
        /// Происходит при изменении настроек постраничного отображения записей.
        /// </summary>
        public event EventHandler PagingChanged;

        /// <summary>
        /// Происходит при изменении общего количества страниц.
        /// </summary>
        public event EventHandler TotalPagesChanging;

        #endregion

        #region Свойства

        /// <summary>
        /// Показывать количество записей
        /// </summary>
        public bool ShowTotalNumber { get; set; }

        /// <summary>
        /// Опция "Все элементы" в выпадающем списке количества элементов
        /// </summary>
        public bool EnableAllElementsOption { get; set; }

        /// <summary>
        /// Количество записей на странице.
        /// </summary>
        public int RecordsPerPage => Convert.ToInt32((string) _ddlRecordsPerPage.SelectedValue);

        /// <summary>
        /// Номер текущей страницы.
        /// </summary>
        public int CurrentPage
        {
            get => Convert.ToInt32((string) _tbCurrentPage.Text);
            set
            {
                _tbCurrentPage.Text = value.ToString();
                this.AddToCookie("CurrentPage", value.ToString());
            }
        }

        /// <summary>
        /// Общее количество страниц.
        /// </summary>
        public int TotalPages
        {
            get => ConvertExtentions.ToInt32(_lTotalPages.Text);
            private set { _lTotalPages.Text = value.ToString(); }
        }

        /// <summary>
        /// Общее количество записей.
        /// </summary>
        public int TotalRecords
        {
            get { return ConvertExtentions.ToInt32(_hfTotalRecords.Value); }
            private set { _hfTotalRecords.Value = value.ToString(); }
        }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор
        /// </summary>
        public PagingControl()
        {
            ShowTotalNumber = false;
            EnableAllElementsOption = false;
        }

        #endregion

        #region Обработчики событий

        protected void OnStateChanged(object sender, EventArgs e)
        {
            if (PagingChanged != null)
                PagingChanged(sender, e);
        }

        protected void OnRecordsPerPageChanged(object sender, EventArgs e)
        {
            CurrentPage = 1;
            OnStateChanged(sender, e);
            this.AddToCookie("RecordsPerPage", _ddlRecordsPerPage.SelectedIndex.ToString());
            this.AddToCookie("CurrentPage", CurrentPage.ToString());
        }

        protected void OnCurrentPageTextChanged(object sender, EventArgs e)
        {
            if (CurrentPage > TotalPages)
                CurrentPage = TotalPages;
            else
                if (CurrentPage < 1)
                CurrentPage = 1;
            this.AddToCookie("CurrentPage", CurrentPage.ToString());
            OnStateChanged(sender, e);
        }

        protected override void OnInit(EventArgs e)
        {
            string[] recordsPerPage = "10,20,50,100".Split(',');
            recordsPerPage.ToList().ForEach(x => _ddlRecordsPerPage.Items.Add(new ListItem(string.Format("{0} записей", x), x)));

            if (EnableAllElementsOption)
                _ddlRecordsPerPage.Items.Add(new ListItem("Все элементы", "1000000"));

            CurrentPage = this.GetFromCookie("CurrentPage").ToInt32(1);
            _ddlRecordsPerPage.SelectedIndex = this.GetFromCookie("RecordsPerPage").ToInt32(10);

            base.OnInit(e);
        }

        /// <summary>
        /// Установить общее количество записей.
        /// </summary>
        /// <param name="totalRecords">Общее количество записей</param>
        public void SetTotalRecords(int totalRecords)
        {
            TotalRecords = totalRecords;

            _lTotalNumber.Visible = ShowTotalNumber;
            _lTotalNumber.Text = string.Format("Всего записей: {0} ", totalRecords);

            if (totalRecords == 0)
                totalRecords = CurrentPage = 1;

            int totalPages = (int)Math.Ceiling(totalRecords / (decimal)RecordsPerPage);

            if (TotalPagesChanging != null && TotalPages != totalPages)
                TotalPagesChanging(this, EventArgs.Empty);

            TotalPages = totalPages;

            if (TotalPages < 2)
            {
                _bForward.Enabled = _bBack.Enabled = false;
            }
            else if (CurrentPage == 1)
            {
                _bBack.Enabled = false;
                _bForward.Enabled = true;
            }
            else if (CurrentPage == TotalPages)
            {
                _bForward.Enabled = false;
                _bBack.Enabled = true;
            }
            else
            {
                _bForward.Enabled = _bBack.Enabled = true;
            }

            if (totalRecords > 0)
            {
                _tbCurrentPage.Enabled = true;
            }
            else
            {
                _bForward.Enabled = _bBack.Enabled = _tbCurrentPage.Enabled = false;
                CurrentPage = 0;
            }

            if (CurrentPage > TotalPages)
                CurrentPage = TotalPages;
        }

        protected void OnBackClick(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                this.AddToCookie("CurrentPage", CurrentPage.ToString());
                OnStateChanged(sender, e);
            }
        }

        protected void OnForwardClick(object sender, EventArgs e)
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
                this.AddToCookie("CurrentPage", CurrentPage.ToString());
                OnStateChanged(sender, e);
            }
        }

        #endregion
    }
}