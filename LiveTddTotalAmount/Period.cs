using System;
using System.Collections.Generic;

namespace LiveTddTotalAmount
{
    public class Period
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public Period(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException();
            }

            StartDate = startDate;
            EndDate = endDate;
        }

        public int EffectiveDays(Budget budget)
        {
            return (EffectiveEndDate(budget) - EffectiveStartDate(budget)).Days + 1;
        }

        private DateTime EffectiveStartDate(Budget budget)
        {
            return StartDate > budget.FirstDay
                ? StartDate
                : budget.FirstDay;
        }

        private DateTime EffectiveEndDate(Budget budget)
        {
            return EndDate < budget.LastDay
                ? EndDate
                : budget.LastDay;
        }
    }
}