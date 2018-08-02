using System;
using System.Runtime.Serialization;

namespace Icona.Logic.Contracts
{
    [DataContract]
    public class NewsItemContract
    {
        [DataMember(IsRequired = true)]
        public int ChannelId { get; set; }

        [DataMember(IsRequired = true)]
        public string Title { get; set; }

        [DataMember(IsRequired = true)]
        public string Description { get; set; }

        [DataMember(IsRequired = true)]
        public string Url { get; set; }

        [DataMember(IsRequired = true)]
        public string Text { get; set; }

        [DataMember(IsRequired = true)]
        public DateTime Date { get; set; }
    }
}
