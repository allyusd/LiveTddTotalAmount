﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveTddTotalAmount
{
    public class Accounting
    {
        private IRepository<Budget> _repository;

        public Accounting(IRepository<Budget> repository)
        {
            _repository = repository;
        }

        public decimal TotalAmount(DateTime startDate, DateTime endDate)
        {
            var period = new Period(startDate, endDate);

            if (period.EndDate < period.StartDate)
            {
                throw new ArgumentException();
            }

            var budgets = _repository.GetAll();

            if (budgets.Any())
            {
                if (endDate < budgets[0].FirstDay)
                {
                    return 0;
                }

                if (startDate > budgets[0].LastDay)
                {
                    return 0;
                }

                var effectiveEndDate = period.EffectiveEndDate(budgets);

                var effectiveStartDate = period.EffectiveStartDate(budgets);

                return (effectiveEndDate - effectiveStartDate).Days + 1;
            }

            return 0;
        }
    }
}