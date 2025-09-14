using System;

namespace WindowsFormsApp1
{
    public class RssSource
    {
        public string DisplayName { get; set; }
        public string Url { get; set; }
        public string Region { get; set; }
        public string Province { get; set; }
        
        public RssSource(string displayName, string url, string region = "", string province = "")
        {
            DisplayName = displayName;
            Url = url;
            Region = region;
            Province = province;
        }
        
        public override string ToString()
        {
            return DisplayName;
        }
    }
}