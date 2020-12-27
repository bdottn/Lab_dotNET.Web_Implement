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
        public void Service_CustomerCreate_預期ISQLRepositoryCreate執行過一次()
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
        public void Service_CustomerCreate_驗證錯誤_預期得到Fail()
        {
            var customer =
                new Customer()
                {
                    Id = 2,
                    Name = "CustomerName_2",
                };

            this.repository.Validate(customer).Returns("error");

            var expected =
                new ServiceResult<Customer>()
                {
                    ResultType = ServiceResultType.Fail,
                    Message = "屬性驗證錯誤：error",
                };

            var actual = this.service.Create(customer);

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_CustomerCreate_驗證錯誤_預期ISQLRepositoryCreate沒有執行過()
        {
            var customer =
                new Customer()
                {
                    Id = 2,
                    Name = "CustomerName_2",
                };

            this.repository.Validate(customer).Returns("error");

            this.service.Create(customer);

            this.repository.DidNotReceiveWithAnyArgs().Create(customer);
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
        public void Service_CustomerFind_資料不存在_預期得到Warning()
        {
            var customer =
                new Customer()
                {
                    Id = 22,
                    Name = "CustomerName_22",
                };

            this.repository.Find(customer.Id).Returns((Customer)null);

            var expected =
                new ServiceResult<Customer>()
                {
                    ResultType = ServiceResultType.Warning,
                    Message = "資料庫不存在客戶資料",
                };

            var actual = this.service.Find(customer);

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_CustomerUpdate_預期得到Success()
        {
            var customer =
                new Customer()
                {
                    Id = 999,
                    Name = "CustomerName_999",
                };

            this.repository.Find(customer.Id).Returns(customer);

            this.repository.Validate(customer).Returns("");

            var expected =
                new ServiceResult<Customer>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = customer,
                };

            var actual = this.service.Update(customer);

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_CustomerUpdate_資料不存在_預期得到Fail()
        {
            var customer =
                new Customer()
                {
                    Id = 999,
                    Name = "CustomerName_999",
                };

            this.repository.Find(customer.Id).Returns((Customer)null);

            this.repository.Validate(customer).Returns("");

            var expected =
                new ServiceResult<Customer>()
                {
                    ResultType = ServiceResultType.Fail,
                    Message = "資料庫不存在客戶資料",
                };

            var actual = this.service.Update(customer);

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_CustomerUpdate_驗證錯誤_預期得到Fail()
        {
            var customer =
                new Customer()
                {
                    Id = 999,
                    Name = "CustomerName_999",
                };

            this.repository.Find(customer.Id).Returns(customer);

            this.repository.Validate(customer).Returns("RRRR");

            var expected =
                new ServiceResult<Customer>()
                {
                    ResultType = ServiceResultType.Fail,
                    Message = "屬性驗證錯誤：RRRR",
                };

            var actual = this.service.Update(customer);

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_CustomerUpdate_資料不存在_預期ISQLRepositoryUpdate沒有執行過()
        {
            var customer =
                new Customer()
                {
                    Id = 999,
                    Name = "CustomerName_999",
                };

            this.repository.Find(customer.Id).Returns((Customer)null);

            this.repository.Validate(customer).Returns("");

            this.service.Update(customer);

            this.repository.DidNotReceiveWithAnyArgs().Update(customer);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_CustomerUpdate_驗證錯誤_預期ISQLRepositoryUpdate沒有執行過()
        {
            var customer =
                new Customer()
                {
                    Id = 999,
                    Name = "CustomerName_999",
                };

            this.repository.Find(customer.Id).Returns(customer);

            this.repository.Validate(customer).Returns("RRRR");

            this.service.Update(customer);

            this.repository.DidNotReceiveWithAnyArgs().Update(customer);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_CustomerDelete_預期得到Success()
        {
            var customer =
                new Customer()
                {
                    Id = 22,
                    Name = "CustomerName_22",
                };

            this.repository.Find(customer.Id).Returns(customer);

            var expected =
                new ServiceResult()
                {
                    ResultType = ServiceResultType.Success,
                };

            var actual = this.service.Delete(customer);

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_CustomerDelete_資料不存在_預期得到Warning()
        {
            var customer =
                new Customer()
                {
                    Id = 22,
                    Name = "CustomerName_22",
                };

            this.repository.Find(customer.Id).Returns((Customer)null);

            var expected =
                new ServiceResult()
                {
                    ResultType = ServiceResultType.Warning,
                    Message = "資料庫不存在客戶資料",
                };

            var actual = this.service.Delete(customer);

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_CustomerDelete_資料不存在_預期ISQLRepositoryDelete沒有執行過()
        {
            var customer =
                new Customer()
                {
                    Id = 22,
                    Name = "CustomerName_22",
                };

            this.repository.Find(customer.Id).Returns((Customer)null);

            this.service.Delete(customer);

            this.repository.DidNotReceiveWithAnyArgs().Delete(customer);
        }
    }
}