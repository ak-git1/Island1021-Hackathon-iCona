using Icona.ChannelsDownloading.App.ChannelsService;

namespace Icona.ChannelsDownloading.App.Logic.ChannelsProcessors
{
    class FbProcessor : BaseChannelProcessor
    {
        private string Token { get; }

        public FbProcessor(ChannelContract channel) : base(channel)
        {
            Token = Attributes["Token"];
        }

        public override void Process()
        {
            FilterNewsItemsByTags();
            SaveNewsItems();
        }
    }
}
