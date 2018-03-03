using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace LiveTddTotalAmount
{
    [TestClass]
    public class TotalAmountTest
    {
        private IRepository<Budget> _repository = Substitute.For<IRepository<Budget>>();
        private Accounting _accounting;

        [TestInitialize]
        public void TestInit()
        {
            _accounting = new Accounting(_repository);
        }

        [TestMethod]
        public void no_budget()
        {
            GivenBudgets();
            TotalAmountShouldBe(
                new DateTime(2018, 4, 1),
                new DateTime(2018, 4, 1),
                0);
        }

        [TestMethod]
        public void one_effecttive_day_period_inside_budget_month()
        {
            GivenBudgets(
                    new Budget() { YearMonth = "201804", Amount = 30}
                );
            TotalAmountShouldBe(
                new DateTime(2018, 4, 1),
                new DateTime(2018, 4, 1),
                1);
        }

        private void TotalAmountShouldBe(DateTime startDate, DateTime endDate, int excepted)
        {
            var amount = _accounting.TotalAmount(startDate, endDate);
            Assert.AreEqual(excepted, amount);
        }

        private void GivenBudgets(params Budget[] bugets)
        {
            _repository.GetAll().Returns(bugets.ToList());
        }
    }

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
                return (period.EndDate - period.StartDate).Days + 1;
            }

            return 0;
        }
    }

    public class Period
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public Period(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }

    public class Budget
    {
        public int Amount { get; set; }
        public string YearMonth { get; set; }
    }

    public interface IRepository<T>
    {
        List<T> GetAll();
    }
}
