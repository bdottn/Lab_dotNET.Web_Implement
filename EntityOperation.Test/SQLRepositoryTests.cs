using EntityOperation.Protocol;
using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Operation.Model;
using System;
using System.Linq;
using System.Transactions;

namespace EntityOperation.Test
{
    [TestClass]
    public sealed class SQLRepositoryTests
    {
        private TransactionScope trans;
        private LabContext context;

        private ISQLRepository<Customer> repository;

        [TestInitialize]
        public void TestInitialize()
        {
            // 建立資料自動還原使用綁定交易
            this.trans = new TransactionScope(TransactionScopeOption.Required);
            this.context = new LabContext();

            this.repository = new SQLRepository<Customer>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (this.trans != null)
            {
                this.trans.Dispose();
            }

            if (this.context != null)
            {
                this.context.Dispose();
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void EntityOperation_Create_預期資料庫會有寫入值()
        {
            var customer =
                new Customer()
                {
                    Id = 1,
                    Name = "CustomerName_1",
                    Phone = "0800000123",
                    CreatedTime = new DateTime(2020, 12, 27),
                    LatestModifiedTime = new DateTime(2020, 12, 27),
                };

            var checkModel = this.GetSQLCustomer(customer);

            Assert.IsNull(checkModel);



            this.repository.Create(customer);

            var actual = this.GetSQLCustomer(customer);

            customer.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void EntityOperation_Find_預期資料庫會有值()
        {
            var customer =
                new Customer()
                {
                    Id = 2,
                    Name = "CustomerName_2",
                    Phone = "339911",
                    CreatedTime = new DateTime(2020, 12, 26),
                    LatestModifiedTime = new DateTime(2020, 12, 27),
                };

            this.context.Customers.Add(customer);
            this.context.SaveChanges();

            var checkModel = this.GetSQLCustomer(customer);

            Assert.IsNotNull(checkModel);



            var actual = this.repository.Find(customer.Id);

            customer.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void EntityOperation_Update_預期資料庫會被更新成輸入值()
        {
            var customer =
                new Customer()
                {
                    Id = 3,
                    Name = "CustomerName_3",
                    Phone = "339911",
                    CreatedTime = new DateTime(2020, 12, 26),
                    LatestModifiedTime = new DateTime(2020, 12, 27),
                };

            this.context.Customers.Add(customer);
            this.context.SaveChanges();

            var checkModel = this.GetSQLCustomer(customer);

            Assert.IsNotNull(checkModel);



            var expected =
                new Customer()
                {
                    Id = customer.Id,
                    Name = "CustomerName_Update",
                    Phone = null,
                    CreatedTime = new DateTime(2020, 12, 30),
                    LatestModifiedTime = new DateTime(2020, 12, 30),
                };

            this.repository.Update(expected);

            var actual = this.GetSQLCustomer(expected);

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void EntityOperation_Delete_預期資料庫會沒有值()
        {
            var customer =
                new Customer()
                {
                    Id = 4,
                    Name = "CustomerName_4",
                    Phone = "339911",
                    CreatedTime = new DateTime(2020, 12, 26),
                    LatestModifiedTime = new DateTime(2020, 12, 27),
                };

            this.context.Customers.Add(customer);
            this.context.SaveChanges();

            var checkModel = this.GetSQLCustomer(customer);

            Assert.IsNotNull(checkModel);



            this.repository.Delete(customer);

            var actual = this.GetSQLCustomer(customer);

            Assert.IsNull(actual);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void EntityOperation_Validate_預期驗證訊息不為空()
        {
            var customer =
                new Customer()
                {
                    Id = 4,
                };

            var actual = this.repository.Validate(customer);

            Assert.IsFalse(string.IsNullOrEmpty(actual));
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void EntityOperation_Validate_預期驗證訊息為空()
        {
            var customer =
                new Customer()
                {
                    Id = 8,
                    Name = "ABC",
                    CreatedTime = new DateTime(2020, 12, 27),
                    LatestModifiedTime = new DateTime(2020, 12, 27),
                };

            var actual = this.repository.Validate(customer);

            Assert.IsTrue(string.IsNullOrEmpty(actual));
        }

        private Customer GetSQLCustomer(Customer customer)
        {
            var model =
                this.context.Customers.AsNoTracking().SingleOrDefault(c =>
                    c.Id == customer.Id &&
                    c.Name == customer.Name &&
                    c.Phone == customer.Phone &&
                    c.CreatedTime == customer.CreatedTime &&
                    c.LatestModifiedTime == customer.LatestModifiedTime);

            return model;
        }
    }
}