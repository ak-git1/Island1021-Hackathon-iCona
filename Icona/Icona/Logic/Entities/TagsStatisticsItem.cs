using System.Data;
using Ak.Framework.Core.Extensions;
using Icona.Logic.DAL;
using Icona.Logic.Filters;

namespace Icona.Logic.Entities
{
    public class TagsStatisticsItem : BaseEntity
    {
        public string Name { get; set; }

        public int ChannelId { get; set; }

        public string ChannelName { get; set; }

        public int PublishedNewsQuantity { get; set; }

        public int PositiveVotesQuantity { get; set; }

        public int NegativeVotesQuantity { get; set; }

        public int CommentsQuantity { get; set; }

        public int ViewsQuantity { get; set; }

        public TagsStatisticsItem()
        {
        }

        public TagsStatisticsItem(IDataReader dr) : base(dr)
        {
            Name = dr["Name"].ToStr();
            ChannelId = dr["ChannelId"].ToInt32();
            ChannelName = dr["ChannelName"].ToStr();
            PublishedNewsQuantity = dr["PublishedNewsQuantity"].ToInt32();
            NegativeVotesQuantity = dr["NegativeVotesQuantity"].ToInt32();
            PositiveVotesQuantity = dr["PositiveVotesQuantity"].ToInt32();
            CommentsQuantity = dr["CommentsQuantity"].ToInt32();
            ViewsQuantity = dr["ViewsQuantity"].ToInt32();
        }

        public static ListEx<TagsStatisticsItem> GetList(TagsStatisticsFilter f)
        {
            return TagsStatistics.GetList(f);
        }
    }
}