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


                return budgets[0].TotalAmount(period);
            }

            return 0;
        }
    }
}