using System;
using Icona.Logic.Interfaces;

namespace Icona.Logic.Filters
{
    /// <summary>
    /// Класс для создания заглушки фильтра для печати всех страниц выборки
    /// </summary>
    public class FilterPager : IPaging
    {
        #region Свойства

        /// <summary>
        /// Количество записей на странице
        /// </summary>
        public int RecordsPerPage { get; private set; }

        /// <summary>
        /// Номер текущей страницы
        /// </summary>
        public int CurrentPage { get; private set; }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="currentPage">Номер текущей страницы</param>
        /// <param name="recordsPerPage">Количество записей на странице</param>
        
        public FilterPager(int currentPage, int recordsPerPage)
        {
            RecordsPerPage = recordsPerPage;
            CurrentPage = currentPage;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public FilterPager()
        {
            RecordsPerPage = Int32.MaxValue;
            CurrentPage = 1;
        }

        #endregion
    }
}
