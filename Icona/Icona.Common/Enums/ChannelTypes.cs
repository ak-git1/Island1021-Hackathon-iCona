using System.ComponentModel;

namespace Icona.Common.Enums
{
    /// <summary>
    /// Типы каналов
    /// </summary>
    public enum ChannelTypes
    {
        /// <summary>
        /// Не указано
        /// </summary>
        [Description("Не указано")]
        None = 0,

        /// <summary>
        /// Rss
        /// </summary>
        [Description("Rss")]
        Rss = 1,

        /// <summary>
        /// Facebook
        /// </summary>
        [Description("Facebook")]
        Facebook = 2,

        /// <summary>
        /// ВКонтакте
        /// </summary>
        [Description("ВКонтакте")]
        Vk = 3,

        /// <summary>
        /// Телеграмм
        /// </summary>
        [Description("Телеграмм")]
        Telegram = 4,

        /// <summary>
        /// Instagram
        /// </summary>
        [Description("Instagram")]
        Instagram = 5,

        /// <summary>
        /// Twitter
        /// </summary>
        [Description("Twitter")]
        Twitter = 6
    }
}