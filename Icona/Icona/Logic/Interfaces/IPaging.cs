namespace Icona.Logic.Interfaces
{
    /// <summary>
    /// Класс для хранения информации о нумерации страниц.
    /// </summary>
    public interface IPaging
    {
        /// <summary>
        /// Количество записей на странице.
        /// </summary>
        int RecordsPerPage { get;}

        /// <summary>
        /// Номер текущей страницы.
        /// </summary>
        int CurrentPage { get;}
    }
}