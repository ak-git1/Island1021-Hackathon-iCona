using System;

namespace Icona.Logic.UI
{
    /// <summary>
    /// Базовый класс для страниц с фильтрами
    /// </summary>
    public abstract class BaseFilteredPage : BasePage
    {
        #region Приватные методы

        /// <summary>
        /// Заполнение списков фильтров
        /// </summary>
        protected abstract void FillFilterLists();

        #endregion

        #region Обработчики событий

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FillFilterLists();
        }

        #endregion
    }
}
