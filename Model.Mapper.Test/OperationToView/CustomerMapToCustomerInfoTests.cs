using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Operation.Model;
using System;
using View.Model;

namespace Model.Mapper.Test.OperationToView
{
    using AutoMapper;
    using Model.Mapper.OperationToView;

    [TestClass]
    public sealed class CustomerMapToCustomerInfoTests
    {
        private IMapper mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            var config =
                new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new CustomerMapToCustomerInfo());
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
        public void Mapper_CustomerMapToCustomerInfo_設定檢查()
        {
            // 所有屬性皆有設定對應
            this.mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Mapper_CustomerMapToCustomerInfo_預期得到CustomerInfo()
        {
            var customer =
                new Customer()
                {
                    Id = 99999,
                    Name = "NAME",
                    Phone = "PPOONNEE",
                    CreatedTime = new DateTime(2020, 12, 27),
                    LatestModifiedTime = new DateTime(2020, 12, 28),
                };

            var expected =
                new CustomerInfo()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    CreatedTime = customer.CreatedTime,
                    LatestModifiedTime = customer.LatestModifiedTime,
                };

            var actual = this.mapper.Map<CustomerInfo>(customer);

            expected.ToExpectedObject().ShouldEqual(actual);
        }
    }
}