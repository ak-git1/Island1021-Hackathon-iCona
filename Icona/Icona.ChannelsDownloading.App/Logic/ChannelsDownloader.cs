using System;
using System.Collections.Generic;
using System.Linq;
using Icona.ChannelsDownloading.App.ChannelsService;
using Icona.ChannelsDownloading.App.Logic.ChannelsProcessors;

namespace Icona.ChannelsDownloading.App.Logic
{
    class ChannelsDownloader
    {
        public void ProcessChannels()
        {
            try
            {
                ChannelsServiceClient client = new ChannelsServiceClient();                
                List<ChannelContract> channels = client.GetChannels().ToList();
                foreach (ChannelContract channel in channels)
                {
                    try
                    {
                        if (channel != null)
                            ChannelsProcessorFactory.Create(channel).Process();
                    }
                    catch (Exception e)
                    {
                        // Сделать обработку ошибок
                    }
                }
            }
            catch (Exception e)
            {
                // Сделать обработку ошибок
            }
        }
    }
}
