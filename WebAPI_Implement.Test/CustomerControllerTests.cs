using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Operation.Model;
using Service.Model;
using Service.Protocol;
using System.Net;
using System.Web.Http.Results;
using View.Model;

namespace WebAPI_Implement.Test
{
    [TestClass]
    public sealed class CustomerControllerTests
    {
        private ICustomerService service;
        private IMapper mapper;

        private CustomerController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            this.service = Substitute.For<ICustomerService>();
            this.mapper = Substitute.For<IMapper>();

            this.controller =
                new CustomerController(this.service, this.mapper);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Controller_CustomerCreate_預期得到Created()
        {
            var customerData =
                new CustomerData()
                {
                    Name = "CustomerName_5",
                };

            var customer =
                new Customer()
                {
                    Name = customerData.Name,
                };

            this.mapper.Map<Customer>(customerData).Returns(customer);

            var serviceResult =
                new ServiceResult<Customer>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = customer,
                };

            this.service.Create(customer).Returns(serviceResult);

            var customerInfo =
                new CustomerInfo()
                {
                    Name = customer.Name,
                };

            this.mapper.Map<CustomerInfo>(customer).Returns(customerInfo);

            var actual = this.controller.Create(customerData) as NegotiatedContentResult<CustomerInfo>;

            Assert.AreEqual(HttpStatusCode.Created, actual.StatusCode);
            Assert.AreEqual(customerInfo, actual.Content);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Controller_CustomerFind_預期得到OK()
        {
            var customerId = 1;

            var customer =
                new Customer()
                {
                    Id = customerId,
                    Name = "TestName",
                };

            var serviceResult =
                new ServiceResult<Customer>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = customer,
                };

            this.service.Find(customerId).Returns(serviceResult);

            var customerInfo =
                new CustomerInfo()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                };

            this.mapper.Map<CustomerInfo>(customer).Returns(customerInfo);

            var actual = this.controller.Find(customerId) as NegotiatedContentResult<CustomerInfo>;

            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
            Assert.AreEqual(customerInfo, actual.Content);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Controller_CustomerUpdate_預期得到OK()
        {
            var customerId = 5;

            var customerData =
                new CustomerData()
                {
                    Name = "CustomerName_5",
                };

            var customer =
                new Customer()
                {
                    Id = customerId,
                    Name = customerData.Name,
                };

            this.mapper.Map<Customer>(customerData).Returns(customer);

            var serviceResult =
                new ServiceResult<Customer>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = customer,
                };

            this.service.Update(customerId, customer).Returns(serviceResult);

            var customerInfo =
                new CustomerInfo()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                };

            this.mapper.Map<CustomerInfo>(customer).Returns(customerInfo);

            var actual = this.controller.Update(customerId, customerData) as NegotiatedContentResult<CustomerInfo>;

            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
            Assert.AreEqual(customerInfo, actual.Content);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Controller_CustomerDelete_預期得到OK()
        {
            var customerId = 1;

            var serviceResult =
                new ServiceResult<Customer>()
                {
                    ResultType = ServiceResultType.Warning,
                    Message = "沒有東西",
                };

            this.service.Delete(customerId).Returns(serviceResult);

            var actual = this.controller.Delete(customerId) as NegotiatedContentResult<string>;

            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
            Assert.AreEqual(serviceResult.Message, actual.Content);
        }
    }
}