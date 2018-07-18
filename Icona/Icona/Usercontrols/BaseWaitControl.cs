using System.Web.UI;

namespace Icona.Usercontrols
{
    /// <summary>
    /// Базовый класс WaitControl'a
    /// </summary>
    public abstract class BaseWaitControl : UserControl
    {
        /// <summary>
        /// Получить обработчик события клика
        /// </summary>
        /// <returns>Обработчик события клика</returns>
        public static string OnClientClickEventHandler => "WaitControl_onClientClick";

        /// <summary>
        /// Получить уникальный ключ
        /// </summary>
        /// <returns>Уникальный ключ</returns>
        public abstract string GetUniqueKey();
    }
}
