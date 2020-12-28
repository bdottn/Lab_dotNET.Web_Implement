using EntityOperation.Protocol;
using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Operation.Model;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EntityOperation.Test
{
    [TestClass]
    public sealed class SQLQueryOperationTests
    {
        private ISQLQueryOperation<Customer> queryOperation;

        [TestInitialize]
        public void TestInitialize()
        {
            this.queryOperation = new SQLQueryOperation<Customer>();
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void EntityOperation_Query_And_預期QueryExpression一樣()
        {
            Expression<Func<Customer, bool>> expected = c => c.Id == 1;

            this.queryOperation.And(c => c.Id == 1);

            var actual = this.queryOperation.QueryExpression;

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void EntityOperation_Query_And_多個_預期QueryExpression一樣()
        {
            Expression<Func<Customer, bool>> expected = c =>
                c.Id == 1 &&
                c.Name == "ABC" &&
                (
                    c.CreatedTime >= new DateTime(2000, 01, 01) ||
                    c.LatestModifiedTime >= new DateTime(1999, 01, 01)
                );

            this.queryOperation.And(c => c.Id == 1);
            this.queryOperation.And(c => c.Name == "ABC");
            this.queryOperation.And(c =>
                c.CreatedTime >= new DateTime(2000, 01, 01) ||
                c.LatestModifiedTime >= new DateTime(1999, 01, 01));

            var actual = this.queryOperation.QueryExpression;

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void EntityOperation_Query_Sort_預期OrderBy一樣()
        {
            Func<IQueryable<Customer>, IOrderedQueryable<Customer>> expected =
                c => c.OrderBy(cu => cu.LatestModifiedTime);

            this.queryOperation.Sort(expected);

            var actual = this.queryOperation.OrderBy;

            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void EntityOperation_Query_Paging_預期一樣()
        {
            var pageSize = 7;
            var pageIndex = 4;

            this.queryOperation.Paging(pageSize, pageIndex);

            var actualPageSize = this.queryOperation.PageSize;
            var actualPageIndex = this.queryOperation.PageIndex;

            Assert.AreEqual(pageSize, actualPageSize);
            Assert.AreEqual(pageIndex, actualPageIndex);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void EntityOperation_Query_Reset_預期null()
        {
            this.queryOperation.And(c => c.Id == 1);
            this.queryOperation.Sort(c => c.OrderBy(cu => cu.LatestModifiedTime));
            this.queryOperation.Paging(3, 9);

            Assert.IsNotNull(queryOperation.QueryExpression);
            Assert.IsNotNull(queryOperation.OrderBy);
            Assert.IsNotNull(queryOperation.PageSize);
            Assert.IsNotNull(queryOperation.PageIndex);

            this.queryOperation.Reset();

            Assert.IsNull(queryOperation.QueryExpression);
            Assert.IsNull(queryOperation.OrderBy);
            Assert.IsNull(queryOperation.PageSize);
            Assert.IsNull(queryOperation.PageIndex);
        }
    }
}