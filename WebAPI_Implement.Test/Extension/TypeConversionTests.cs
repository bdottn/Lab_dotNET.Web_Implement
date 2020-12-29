using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Model;
using System;
using System.Net;
using WebAPI_Implement.Extension;

namespace WebAPI_Implement.Test.Extension
{
    [TestClass]
    public sealed class TypeConversionTests
    {
        [TestMethod]
        [TestCategory("Unit")]
        public void Controller_ToHttpStatusCode_預期得到Forbidden()
        {
            var resultType = ServiceResultType.Fail;

            var actual = resultType.ToHttpStatusCode();

            Assert.AreEqual(HttpStatusCode.Forbidden, actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Controller_ToHttpStatusCode_預期得到Conflict()
        {
            var resultType = ServiceResultType.Exception;

            var actual = resultType.ToHttpStatusCode();

            Assert.AreEqual(HttpStatusCode.Conflict, actual);
        }

        [TestMethod]
        [TestCategory("Unit")]
        [TestCategory("Exception")]
        [ExpectedException(typeof(NotSupportedException))]
        public void Controller_ToHttpStatusCode_預期得到NotSupportedException()
        {
            var resultType = ServiceResultType.Success;

            resultType.ToHttpStatusCode();
        }
    }
}