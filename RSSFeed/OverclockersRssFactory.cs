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
                    retList.Add(new OverclockersRss(xmlNode.SelectSingleNode("title").InnerText, xmlNode.SelectSingleNode("link").InnerText, xmlNode.SelectSingleNode("description").InnerText, 
                        System.Text.RegularExpressions.Regex.Match(xmlNode.SelectSingleNode("link").InnerText, @"\d*-\d*-\d*").Value +"-"+System.Text.RegularExpressions.Regex.Match(xmlNode.SelectSingleNode("pubDate").InnerText, @"\d*:\d*:\d*").Value, xmlNode.SelectSingleNode("category").InnerText));
            }
            return retList;
        }
    }

}




    

