using System.ComponentModel;

namespace Icona.Logic.Enums
{
    /// <summary>
    /// Иконки для окна сообщений
    /// </summary>
    public enum WebMessageBoxIcons
    {
        /// <summary>
        /// Нет иконки
        /// </summary>
        [Description("Нет иконки")]
        None,

        /// <summary>
        /// Информационное сообщение
        /// </summary>
        [Description("Информационное сообщение")]
        Info,

        /// <summary>
        /// Предупреждение
        /// </summary>
        [Description("Предупреждение")]
        Warning,

        /// <summary>
        /// Ошибка
        /// </summary>
        [Description("Ошибка")]
        Error,

        /// <summary>
        /// Вопрос
        /// </summary>
        [Description("Вопрос")]
        Question
    }
}
