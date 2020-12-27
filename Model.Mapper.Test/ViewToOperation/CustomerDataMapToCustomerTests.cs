using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Operation.Model;
using View.Model;

namespace Model.Mapper.Test.ViewToOperation
{
    using AutoMapper;
    using Model.Mapper.ViewToOperation;

    [TestClass]
    public sealed class CustomerDataMapToCustomerTests
    {
        private IMapper mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            var config =
                new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new CustomerDataMapToCustomer());
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
        public void Mapper_CustomerDataMapToCustomer_設定檢查()
        {
            // 所有屬性皆有設定對應
            this.mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Mapper_CustomerDataMapToCustomer_預期得到Customer()
        {
            var customerData =
                new CustomerData()
                {
                    Name = "NN",
                    Phone = "PP",
                };

            var expected =
                new Customer()
                {
                    Name = customerData.Name,
                    Phone = customerData.Phone,
                };

            var actual = this.mapper.Map<Customer>(customerData);

            expected.ToExpectedObject().ShouldEqual(actual);
        }
    }
}