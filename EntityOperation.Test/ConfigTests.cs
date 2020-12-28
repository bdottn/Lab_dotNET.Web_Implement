using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Operation.Model;

namespace EntityOperation.Test
{
    [TestClass]
    public sealed class ConfigTests
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void EntityOperation_MSSQLConnectionString_預期得到設定檔的連線字串()
        {
            var expected = @"Server=.;Database=〖.NET：Lab〗Web_Implement;Integrated Security=True;";

            var actual = Config.Instance.MSSQLConnectionString;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void EntityOperation_AcceptedCredential_預期得到設定檔的Credential()
        {
            var expected =
                new WebAPICredential()
                {
                    Key = "ABC",
                    Value = "456",
                };

            var actual = Config.Instance.AcceptedCredential;

            expected.ToExpectedObject().ShouldEqual(actual);
        }
    }
}