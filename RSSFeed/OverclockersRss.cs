using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Husky_sTestBot.RSSFeed
{
    public class OverclockersRss
    {
        private static string RssUrl { get; } = "https://www.overclockers.ua/rss.xml";
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string PubDate { get; set; }
        public string Category { get; set; }

        public OverclockersRss(string Title, string Link, string Description, string PubDate, string Category)
        {

        }
    }
}
