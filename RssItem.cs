using System;

namespace WindowsFormsApp1
{
    public class RssItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string PubDate { get; set; }
        public DateTime DatePublished { get; set; }
        
        public RssItem()
        {
        }
        
        public RssItem(string title, string description, string link, string pubDate)
        {
            Title = title;
            Description = description;
            Link = link;
            PubDate = pubDate;
            
            // Thử parse ngày tháng
            if (DateTime.TryParse(pubDate, out DateTime parsedDate))
            {
                DatePublished = parsedDate;
            }
            else
            {
                DatePublished = DateTime.MinValue;
            }
        }
    }
}