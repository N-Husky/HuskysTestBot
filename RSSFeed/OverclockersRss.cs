using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Husky_sTestBot.RSSFeed
{
    public class OverclockersRss : IComparable
    {
        private static string RssUrl { get; } = "https://www.overclockers.ua/rss.xml";
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string PubDate { get; set; }
        public string Category { get; set; }

        public OverclockersRss(string title, string link, string description, string pubDate, string category)
        {
            this.Title = title;
            this.Link = link;
            this.Description = description;
            this.PubDate = pubDate;
            this.Category = category;
        }
        public static List<OverclockersRss> OverclockersRssFactory()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("https://www.overclockers.ua/rss.xml");
            XmlElement xRoot = doc.DocumentElement;
            XmlNode channel = xRoot.SelectSingleNode("channel");
            //Console.WriteLine(childNodes.ToString());
            XmlNodeList list = channel.SelectNodes("item");
            List<OverclockersRss> retList = new List<OverclockersRss>();
            foreach (XmlNode xmlNode in list)
            {
                if(int.Parse(xmlNode.SelectSingleNode("pubDate ").InnerText.Split(' ')[1]) == DateTime.Today.Day){
                    retList.Add(new OverclockersRss(xmlNode.SelectSingleNode("title").InnerText, xmlNode.SelectSingleNode("link").InnerText, xmlNode.SelectSingleNode("description").InnerText, xmlNode.SelectSingleNode("pubDate").InnerText, xmlNode.SelectSingleNode("category").InnerText));
                }
            }
            return retList;
        }

        public int CompareTo(object obj)
        {
            OverclockersRss rss = obj as OverclockersRss;
            return Category.CompareTo(rss.Category);
        }
    }
}
