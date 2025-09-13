using System;

namespace WindowsFormsApp1
{
    public class RssSource
    {
        public string DisplayName { get; set; }
        public string Url { get; set; }
        
        public RssSource(string displayName, string url)
        {
            DisplayName = displayName;
            Url = url;
        }
        
        public override string ToString()
        {
            return DisplayName;
        }
    }
}