﻿using System;
using System.Collections.Generic;

namespace LiveTddTotalAmount
{
    public class Period
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public Period(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime EffectiveStartDate(List<Budget> budgets)
        {
            var effectiveStartDate = StartDate > budgets[0].FirstDay
                ? StartDate
                : budgets[0].FirstDay;
            return effectiveStartDate;
        }

        public DateTime EffectiveEndDate(List<Budget> budgets)
        {
            var effectiveEndDate = EndDate < budgets[0].LastDay
                ? EndDate
                : budgets[0].LastDay;
            return effectiveEndDate;
        }
    }
}