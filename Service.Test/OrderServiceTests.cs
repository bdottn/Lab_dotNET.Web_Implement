using EntityOperation.Protocol;
using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Operation.Model;
using Service.Model;
using Service.Protocol;
using System.Collections.Generic;

namespace Service.Test
{
    [TestClass]
    public sealed class OrderServiceTests
    {
        private ISQLRepository<Customer> customerRepository;
        private ISQLRepository<Order> repository;
        private ISQLRepository<Product> productRepository;

        private IOrderService service;

        [TestInitialize]
        public void TestInitialize()
        {
            this.customerRepository = Substitute.For<ISQLRepository<Customer>>();
            this.repository = Substitute.For<ISQLRepository<Order>>();
            this.productRepository = Substitute.For<ISQLRepository<Product>>();

            this.service = new OrderService(
                this.customerRepository,
                this.repository,
                this.productRepository);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_OrderCreate_預期得到Success()
        {
            var orderDetail1 = new OrderDetail() { ProductId = 1, Quantity = 1, };
            var orderDetail2 = new OrderDetail() { ProductId = 3, Quantity = 5, };

            var order =
                new Order()
                {
                    CustomerId = 1,
                    Address = "台北市",
                    OrderDetails =
                        new List<OrderDetail>()
                        {
                            orderDetail1,
                            orderDetail2,
                        },
                };

            this.customerRepository.Find(order.CustomerId).Returns(new Customer());
            this.productRepository.Find(orderDetail1.ProductId).Returns(new Product());
            this.productRepository.Find(orderDetail2.ProductId).Returns(new Product());

            var expected =
                new ServiceResult<Order>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = order,
                };

            var actual = this.service.Create(order);

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_OrderCreate_得到Fail_預期ISQLRepositoryCreate沒有執行過()
        {
            var orderDetail = new OrderDetail() { ProductId = 1, Quantity = 1, };

            var order =
                new Order()
                {
                    CustomerId = 1,
                    Address = "台北市",
                    OrderDetails =
                        new List<OrderDetail>()
                        {
                            orderDetail,
                        },
                };

            this.customerRepository.Find(order.CustomerId).Returns(new Customer());
            this.productRepository.Find(orderDetail.ProductId).Returns((Product)null);

            var actual = this.service.Create(order);

            Assert.AreEqual(ServiceResultType.Fail, actual.ResultType);

            this.repository.DidNotReceive().Create(Arg.Any<Order>());
        }
    }
}