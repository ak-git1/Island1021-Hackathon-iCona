using System;
using System.Data;
using Elar.Framework.Core.Extensions;
using Icona.Logic.DAL;
using Icona.Logic.Enums;
using Icona.Logic.Filters;

namespace Icona.Logic.Entities
{
    /// <summary>
    /// Новость
    /// </summary>
    public class NewsItem
    {
        public Guid Id { get; set; }

        public int ChannelId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public DateTime Date { get; set; }

        public NewsItemsStates State { get; set; }

        public NewsItem()
        {
        }

        public NewsItem(IDataReader dr)
        {
            Id = dr["Id"].ToGuid();
            ChannelId = dr["ChannelId"].ToInt32();
            Title = dr["Title"].ToStr();
            Description = dr["Description"].ToStr();
            Url = dr["Url"].ToStr();
            Date = dr["Date"].ToDateTime();
            State = dr["State"].ToEnum<NewsItemsStates>();
        }

        public static void Delete(Guid id)
        {
            NewsItems.Delete(id);
        }

        public static void UpdateState(Guid id, NewsItemsStates state)
        {
            NewsItems.UpdateState(id, state);
        }

        public static ListEx<NewsItem> GetList(NewsItemsListFilter f)
        {
            return NewsItems.GetList(f);
        }
    }
}