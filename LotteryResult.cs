using System;

namespace WindowsFormsApp1
{
    public class LotteryResult
    {
        public string Date { get; set; }
        public string Prize { get; set; }
        public string Result { get; set; }
        public string Province { get; set; }
        
        public LotteryResult()
        {
        }
        
        public LotteryResult(string date, string prize, string result, string province = "")
        {
            Date = date;
            Prize = prize;
            Result = result;
            Province = province;
        }
    }
}