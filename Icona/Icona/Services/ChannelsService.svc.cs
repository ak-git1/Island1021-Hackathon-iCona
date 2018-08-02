using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Icona.Logic.Contracts;
using Icona.Logic.Entities;

namespace Icona.Services
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ChannelsService
    {
        [OperationContract, FaultContract(typeof(FaultException))]
        public List<ChannelContract> GetChannels()
        {
            try
            {
                return Channel.GetAllList()
                    .Select(channel => new ChannelContract
                    {
                        Id = channel.Id,
                        Attributes = channel.Attributes,
                        CommunityId = channel.CommunityId,
                        Description = channel.Description,
                        Title = channel.Title,
                        Type = channel.Type,
                        Tags = channel.Tags,
                        Url = channel.Url,
                        LastSynchronizationDate = channel.LastSynchronizationDate
                    })
                    .ToList();
            }
            catch (Exception e)
            {
                // Сделать обработку ошибок
                throw;
            }
        }

        [OperationContract, FaultContract(typeof(FaultException))]
        public void AddNewsItems(List<NewsItemContract> items)
        {
            try
            {
                int? channelId = null;
                foreach (NewsItemContract item in items)
                {
                    try
                    {
                        channelId = item.ChannelId;
                        NewsItem.Add(item);
                    }
                    catch (Exception e)
                    {
                        // Добавить логирование
                    }
                }

                if (channelId.HasValue)
                    Channel.Get(channelId.Value).UpdateSynchronizationDate();
            }
            catch (Exception e)
            {
                // Сделать обработку ошибок
                throw;
            }
        }
    }
}
