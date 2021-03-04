using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Husky_sTestBot.RSSFeed
{
    public static class OverclockersRssFactor
    {
        public static List<OverclockersRss> OverclockersFactory()
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
                if (int.Parse(xmlNode.SelectSingleNode("pubDate").InnerText.Split(' ')[1]) == DateTime.Today.Day)
                {
                    retList.Add(new OverclockersRss(xmlNode.SelectSingleNode("title").InnerText, xmlNode.SelectSingleNode("link").InnerText, xmlNode.SelectSingleNode("description").InnerText, xmlNode.SelectSingleNode("pubDate").InnerText, xmlNode.SelectSingleNode("category").InnerText));
                }
            }
            return retList;
        }
    }

}




    

