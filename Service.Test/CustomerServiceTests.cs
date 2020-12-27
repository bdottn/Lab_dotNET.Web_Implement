﻿using EntityOperation.Protocol;
using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Operation.Model;
using Service.Model;
using Service.Protocol;

namespace Service.Test
{
    [TestClass]
    public sealed class CustomerServiceTests
    {
        private ISQLRepository<Customer> repository;

        private ICustomerService service;

        [TestInitialize]
        public void TestInitialize()
        {
            this.repository = Substitute.For<ISQLRepository<Customer>>();

            this.service = new CustomerService(this.repository);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_CustomerCreate_預期得到Success()
        {
            var customer =
                new Customer()
                {
                    Id = 2,
                    Name = "CustomerName_2",
                };

            var expected =
                new ServiceResult<Customer>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = customer,
                };

            var actual = this.service.Create(customer);

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_CustomerCreate_預期ISQLRepository執行過一次()
        {
            var customer =
                new Customer()
                {
                    Id = 3,
                    Name = "CustomerName_3",
                };

            this.service.Create(customer);

            this.repository.Received(1).Create(customer);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_CustomerFind_預期得到Success()
        {
            var customer =
                new Customer()
                {
                    Id = 22,
                    Name = "CustomerName_22",
                };

            this.repository.Find(customer.Id).Returns(customer);

            var expected =
                new ServiceResult<Customer>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = customer,
                };

            var actual = this.service.Find(customer);

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_CustomerFind_預期得到Success()
        {
            var customer =
                new Customer()
                {
                    Id = 22,
                    Name = "CustomerName_22",
                };

            this.repository.Find(customer.Id).Returns(customer);

            var expected =
                new ServiceResult<Customer>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = customer,
                };

            var actual = this.service.Find(customer);

            expected.ToExpectedObject().ShouldEqual(actual);
        }
    }
}