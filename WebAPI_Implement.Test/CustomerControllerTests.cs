using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Operation.Model;
using Service.Model;
using Service.Protocol;
using System.Net;
using System.Web.Http.Results;

namespace WebAPI_Implement.Test
{
    [TestClass]
    public sealed class CustomerControllerTests
    {
        private ICustomerService service;

        private CustomerController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            this.service = Substitute.For<ICustomerService>();

            this.controller = new CustomerController(this.service);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Controller_CustomerCreate_預期得到Created_回傳值與傳入值一致()
        {
            var customer =
                new Customer()
                {
                    Id = 5,
                    Name = "CustomerName_5",
                };

            var serviceResult =
                new ServiceResult<Customer>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = customer,
                };

            this.service.Create(customer).Returns(serviceResult);

            var actual = this.controller.Create(customer) as NegotiatedContentResult<Customer>;

            Assert.AreEqual(HttpStatusCode.Created, actual.StatusCode);
            Assert.AreEqual(customer, actual.Content);
        }
    }
}