using System.Data;
using Elar.Framework.Core.Extensions;
using Icona.Logic.DAL;
using Icona.Logic.Filters;

namespace Icona.Logic.Entities
{
    /// <summary>
    /// Новостной канал
    /// </summary>
    public class Channel : BaseEntity
    {
        public int CommunityId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Type { get; set; }

        public string Url { get; set; }

        public string Attributes { get; set; }

        public string Tags { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Channel()
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dr">Данные</param>
        internal Channel(IDataReader dr) : base(dr)
        {
            CommunityId = dr["CommunityId"].ToInt32();
            Title = dr["Title"].ToString();
            Description = dr["Description"].ToStr();
            Url = dr["Url"].ToStr();
            Attributes = dr["Attributes"].ToStr();
            Tags = dr["Tags"].ToStr();
        }

        public static int Add(Channel channel)
        {
            return Channels.Add(channel);
        }

        public static Channel Get(int id)
        {
            return Channels.Get(id);
        }

        public void Update()
        {
            Channels.Update(this);
        }

        public void Delete()
        {
            Channels.Delete(Id);
        }

        public static ListEx<Channel> GetList(ChannelsFilter f)
        {
            return Channels.GetList(f);
        }
    }
}