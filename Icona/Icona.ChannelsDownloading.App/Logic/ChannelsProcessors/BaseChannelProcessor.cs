using System.Collections.Generic;
using Accord.MachineLearning.Text.Stemmers;
using Ak.Framework.Core.Extensions;
using Icona.ChannelsDownloading.App.ChannelsService;

namespace Icona.ChannelsDownloading.App.Logic.ChannelsProcessors
{
    abstract class BaseChannelProcessor
    {
        private readonly List<string> _tags = new List<string>();

        protected Dictionary<string, string> Attributes { get; }

        public ChannelContract Channel { get; }        

        public List<NewsItemContract> NewsItems = new List<NewsItemContract>();

        protected BaseChannelProcessor(ChannelContract channel)
        {
            Channel = channel;
            Attributes = new AttributesReader(Channel.Attributes).Read();

            StemmerBase stemmer = new RussianStemmer();
            foreach (string s in Channel.Tags.Split(';'))
                _tags.Add(stemmer.Stem(s.Trim()));
        }

        public abstract void Process();

        protected void SaveNewsItems()
        {
            ChannelsServiceClient client = new ChannelsServiceClient();
            client.AddNewsItems(NewsItems.ToArray());
        }

        protected void FilterNewsItemsByTags()
        {
            List<NewsItemContract> filteredItems = new List<NewsItemContract>();

            // Очень упрощенная фильтрация, может давать ложноположительные результаты
            foreach (NewsItemContract item in NewsItems)
            {
                foreach (string tag in _tags)
                {
                    if (StringExtensions.Contains(item.Text, tag))
                    {
                        filteredItems.Add(item);
                        break;
                    }
                }
            }

            NewsItems = filteredItems;
        }
    }
}
