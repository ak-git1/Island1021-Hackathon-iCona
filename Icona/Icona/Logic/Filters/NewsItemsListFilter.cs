using System;
using Icona.Logic.Enums;

namespace Icona.Logic.Filters
{
    public class NewsItemsListFilter : BaseFilter
    {
        public int CommunityId { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// Дата с
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Дата по
        /// </summary>
        public DateTime? DateTill { get; set; }

        public NewsItemsStates? State { get; set; }
    }
}