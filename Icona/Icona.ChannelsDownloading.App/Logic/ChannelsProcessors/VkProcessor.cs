using Ak.Framework.Core.Extensions;
using Icona.ChannelsDownloading.App.ChannelsService;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Icona.ChannelsDownloading.App.Logic.ChannelsProcessors
{
    class VkProcessor : BaseChannelProcessor
    {
        private string Token { get; }

        public VkProcessor(ChannelContract channel) : base(channel)
        {
            Token = Attributes["Token"];
        }

        public override void Process()
        {
            VkApi api = new VkApi();
            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 123456,
                AccessToken = Token,
                Settings = Settings.All
            });
            WallGetObject wall = api.Wall.Get(new WallGetParams
            {
                Domain = Channel.Url
            });

            if (wall != null && wall.WallPosts.Count > 0)
                foreach (Post post in wall.WallPosts)
                {
                    if (!Channel.LastSynchronizationDate.HasValue ||
                        (post.Date.HasValue && post.Date >= Channel.LastSynchronizationDate.Value))
                    {
                        NewsItems.Add(new NewsItemContract
                        {
                            ChannelId = Channel.Id,
                            Date = post.Date.Value,
                            Title = post.Text.TruncateSpecialSymbols(),
                            Description = post.Text.TruncateSpecialSymbols(),
                            Text = post.Text,
                            Url = $"https://vk.com/{Channel.Url}?w=wall{post.OwnerId}_{post.Id}"
                        });
                    }                    
                }

            FilterNewsItemsByTags();
            SaveNewsItems();
        }
    }
}
