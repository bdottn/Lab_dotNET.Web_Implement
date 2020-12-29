using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Operation.Model;
using ExpectedObjects;

namespace Model.Mapper.Test.OperationToView
{
    using AutoMapper;
    using Model.Mapper.OperationToView;
    using System.Collections.Generic;
    using View.Model;

    [TestClass]
    public sealed class OrderMapToOrderInfoTests
    {
        private IMapper mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            var config =
                new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new OrderMapToOrderInfo());
                    cfg.AddProfile(new OrderDetailMapToOrderDetailInfo());
                });

            this.mapper = new Mapper(config);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mapper = null;
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Mapper_OrderMapToOrderInfo_設定檢查()
        {
            // 所有屬性皆有設定對應
            this.mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Mapper_OrderMapToOrderInfo_預期得到OrderInfo()
        {
            var order =
                new Order()
                {
                    Id = 99999,
                    CustomerId = 82,
                    Address = "tt",
                    CreatedTime = new DateTime(2020, 12, 27),
                    LatestModifiedTime = new DateTime(2020, 12, 28),
                    OrderDetails = null,
                };

            var expected =
                new OrderInfo()
                {
                    Id = order.Id,
                    Address = order.Address,
                    CreatedTime = order.CreatedTime,
                    LatestModifiedTime = order.LatestModifiedTime,
                    OrderDetails = new List<OrderDetailInfo>(),
                };

            var actual = this.mapper.Map<OrderInfo>(order);

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Mapper_OrderMapToOrderInfo_OrderDetailInfo_預期得到OrderInfo_OrderDetailInfo()
        {
            var order =
                new Order()
                {
                    Id = 3,
                    CustomerId = 82,
                    Address = "tt",
                    CreatedTime = new DateTime(2020, 12, 27),
                    LatestModifiedTime = new DateTime(2020, 12, 28),
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            Id = 3,
                            OrderId = 2,
                            ProductId = 7,
                            Quantity = 10,
                        },
                    },
                };

            var expected =
                new OrderInfo()
                {
                    Id = order.Id,
                    Address = order.Address,
                    CreatedTime = order.CreatedTime,
                    LatestModifiedTime = order.LatestModifiedTime,
                    OrderDetails = new List<OrderDetailInfo>()
                    {
                        new OrderDetailInfo()
                        {
                            Id = 3,
                            Quantity = 10,
                        }
                    },
                };

            var actual = this.mapper.Map<OrderInfo>(order);

            expected.ToExpectedObject().ShouldEqual(actual);
        }
    }
}
