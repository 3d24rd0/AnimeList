using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace servicio.clases
{
    public class Read_rss
    {

        public static List<AnnimeNews> GetData()
        {
            XNamespace slashNamespace = "http://wpf.com.es";

            XDocument rssFeed = XDocument.Load("http://feeds.feedburner.com/bittorrent");

            var news = from item in rssFeed.Descendants("item")
                        select new AnnimeNews
                        {
                            Title = item.Element("title").Value,
                            Description = item.Element("description").Value,
                            Author = item.Element("author").Value,
                            Guid = item.Element("guid").Value,
                            Link = item.Element("link").Value,
                           // PubDate = DateTime.Parse(item.Element("pubDate").Value),
                          /*  Tags = (from category in item.Elements("category")
                                    orderby category.Value
                                    select category.Value).ToList()*/
                        };
            //where (Datetime.now - item.publised).days < 7
            List<AnnimeNews> lista = new List<AnnimeNews>();
            foreach (AnnimeNews item in news)
            {
                if (!lista.Contains(item))
                {
                    lista.Add(item);
                }
            }
            return lista;
           // ListView1.DataSource = posts;
            //ListView1.DataBind();
        }
    }
}