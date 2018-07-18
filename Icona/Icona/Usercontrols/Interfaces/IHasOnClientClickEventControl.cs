namespace Icona.Usercontrols.Interfaces
{
    /// <summary>
    /// Интерфейс контрола, имеющего события клика
    /// </summary>
    public interface IHasOnClientClickEventControl
    {
        /// <summary>
        /// Обработчик события клика
        /// </summary>
        string OnClientClick { get; set; }
    }
}
