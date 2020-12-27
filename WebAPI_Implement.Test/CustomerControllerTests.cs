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
    }
}