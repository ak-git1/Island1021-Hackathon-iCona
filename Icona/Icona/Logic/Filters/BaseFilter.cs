using System;
using System.Data.SqlClient;
using Icona.Logic.Interfaces;

namespace Icona.Logic.Filters
{
    /// <summary>
    /// Базовый класс для передачи значений в фильтр
    /// </summary>
    public abstract class BaseFilter : ICloneable
    {
        #region Свойства

        /// <summary>
        /// Пейджинг
        /// </summary>
        public IPaging Paging { get; set; }

        /// <summary>
        /// Текущий пользователь
        /// </summary>
        public int CurrentUserId { get; set; }

        /// <summary>
        /// Колонка для сортировки
        /// </summary>
        public string SortColumnName { get; set; }

        /// <summary>
        /// Порядок сортировки
        /// </summary>
        public SortOrder SortOrder { get; set; } = SortOrder.Unspecified;

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор
        /// </summary>
        protected BaseFilter()
        {
            Paging = new FilterPager(1, 20);  
        }

        #endregion

        #region ICloneable

        /// <summary>
        /// Клонирование фильтра
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}