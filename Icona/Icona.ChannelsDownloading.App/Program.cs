using System;
using Icona.ChannelsDownloading.App.Logic;

namespace Icona.ChannelsDownloading.App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new ChannelsDownloader().ProcessChannels();
        }
    }
}
