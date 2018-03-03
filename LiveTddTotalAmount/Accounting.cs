using System;
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

            return budgets.Sum(w => w.TotalAmount(period));
        }
    }
}