using System;
using System.Data;
using System.Runtime.Serialization;
using Ak.Framework.Core.Extensions;
using Icona.Common.Enums;
using Icona.Logic.Enums;

namespace Icona.Logic.Contracts
{    
    [DataContract]
    public class ChannelContract
    {
        [DataMember(IsRequired = true)]
        public int Id { get; set; }

        [DataMember(IsRequired = true)]
        public int CommunityId { get; set; }

        [DataMember(IsRequired = true)]
        public string Title { get; set; }

        [DataMember(IsRequired = true)]
        public string Description { get; set; }

        [DataMember(IsRequired = true)]
        public ChannelTypes Type { get; set; }

        [DataMember(IsRequired = true)]
        public string Url { get; set; }

        [DataMember(IsRequired = true)]
        public string Attributes { get; set; }

        [DataMember(IsRequired = true)]
        public string Tags { get; set; }

        [DataMember(IsRequired = true)]
        public DateTime? LastSynchronizationDate { get; set; }

        public ChannelContract()
        {
        }

        public ChannelContract(IDataReader dr)
        {
            Id = dr["Id"].ToInt32();
            CommunityId = dr["CommunityId"].ToInt32();
            Title = dr["Title"].ToString();
            Description = dr["Description"].ToStr();
            Type = dr["Type"].ToEnum<ChannelTypes>();
            Url = dr["Url"].ToStr();
            Attributes = dr["Attributes"].ToStr();
            Tags = dr["Tags"].ToStr();
            LastSynchronizationDate = dr["LastSynchronizationDate"].ToDateTime();
        }
    }
}
