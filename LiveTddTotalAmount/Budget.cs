using System;

namespace LiveTddTotalAmount
{
    public class Budget
    {
        public int Amount { get; set; }
        public string YearMonth { get; set; }

        public DateTime FirstDay
        {
            get { return DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null); }
        }

        public DateTime LastDay {
            get { return DateTime.ParseExact(YearMonth + TotalDay, "yyyyMMdd", null); }
        }

        public int TotalDay {
            get { return DateTime.DaysInMonth(FirstDay.Year, FirstDay.Month); }
        }

        public int DailyAmount()
        {
            var dailyAmount = Amount / TotalDay;
            return dailyAmount;
        }
    }
}