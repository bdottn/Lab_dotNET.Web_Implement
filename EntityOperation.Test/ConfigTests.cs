using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}