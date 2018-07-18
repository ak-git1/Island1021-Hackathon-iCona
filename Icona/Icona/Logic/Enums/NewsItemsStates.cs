using System.ComponentModel;

namespace Icona.Logic.Enums
{
    /// <summary>
    /// Состояния новостей
    /// </summary>
    public enum NewsItemsStates
    {
        /// <summary>
        /// Новое
        /// </summary>
        [Description("Новое")]
        New = 0,

        /// <summary>
        /// Опубликовано
        /// </summary>
        [Description("Опубликовано")]
        Published = 1,

        /// <summary>
        /// Отклонено
        /// </summary>
        [Description("Отклонено")]
        Declined = 2
    }
}