using EntityOperation.Model;
using EntityOperation.Protocol;
using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void Create_新增Customer_預期資料庫會有寫入值()
        {
            var customer =
                new Customer()
                {
                    Id = 1,
                    Name = "CustomerName_1",
                };

            this.repository.Create(customer);

            var actual =
                this.context.Customers.AsNoTracking().Single(c =>
                    c.Id == customer.Id &&
                    c.Name == customer.Name);

            customer.ToExpectedObject().ShouldEqual(actual);
        }
    }
}