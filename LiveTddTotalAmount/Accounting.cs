using System;
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

                var effectiveEndDate = period.EndDate < budgets[0].LastDay
                    ? period.EndDate
                    : budgets[0].LastDay;

                var effectiveStartDate = period.StartDate > budgets[0].FirstDay
                    ? period.StartDate
                    : budgets[0].FirstDay;

                return (effectiveEndDate - effectiveStartDate).Days + 1;
            }

            return 0;
        }
    }
}