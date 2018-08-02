using System;
using Icona.ChannelsDownloading.App.ChannelsService;
using Icona.Common.Enums;

namespace Icona.ChannelsDownloading.App.Logic.ChannelsProcessors
{
    internal static class ChannelsProcessorFactory
    {
        public static BaseChannelProcessor Create(ChannelContract channel)
        {
            switch (channel.Type)
            {
                //case ChannelTypes.Facebook:
                //    return new FbProcessor(channel);

                case ChannelTypes.Vk:
                    return new VkProcessor(channel);

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
