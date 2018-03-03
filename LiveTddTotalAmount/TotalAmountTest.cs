using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace LiveTddTotalAmount
{
    [TestClass]
    public class TotalAmountTest
    {
        [TestMethod]
        public void no_budget()
        {
            var repository = Substitute.For<IRepository<Budget>>();
            repository.GetAll().Returns(new List<Budget>());

            var accounting = new Accounting(repository);
            var startDate = new DateTime(2018, 4, 1);
            var endDate = new DateTime(2018, 4, 1);
            var amount = accounting.TotalAmount(startDate, endDate);
            Assert.Equals(0, amount);
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
            throw new NotImplementedException();
        }
    }

    public class Budget
    {
    }

    public interface IRepository<T>
    {
        List<T> GetAll();
    }
}
