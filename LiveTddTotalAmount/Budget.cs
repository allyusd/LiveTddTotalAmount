using System;

namespace LiveTddTotalAmount
{
    public class Budget
    {
        public int Amount { get; set; }

        public string YearMonth { get; set; }

        public int TotalAmount(Period period)
        {
            return DailyAmount() * period.EffectiveDays(this);
        }

        public DateTime FirstDay
        {
            get { return DateTime.ParseExact(YearMonth + "01", "yyyyMMdd", null); }
        }

        public DateTime LastDay {
            get { return DateTime.ParseExact(YearMonth + TotalDay, "yyyyMMdd", null); }
        }

        private int TotalDay {
            get { return DateTime.DaysInMonth(FirstDay.Year, FirstDay.Month); }
        }

        private int DailyAmount()
        {
            return Amount / TotalDay;
        }
    }
}