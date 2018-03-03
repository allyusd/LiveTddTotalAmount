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

        public DateTime EffectiveStartDate(Budget budget)
        {
            var effectiveStartDate = StartDate > budget.FirstDay
                ? StartDate
                : budget.FirstDay;
            return effectiveStartDate;
        }

        public DateTime EffectiveEndDate(Budget budget)
        {
            var effectiveEndDate = EndDate < budget.LastDay
                ? EndDate
                : budget.LastDay;
            return effectiveEndDate;
        }

        public int EffectiveDays(Budget budget)
        {
            var effectiveEndDate = EffectiveEndDate(budget);
            var effectiveStartDate = EffectiveStartDate(budget);
            var effectiveDays = (effectiveEndDate - effectiveStartDate).Days + 1;
            return effectiveDays;
        }
    }
}