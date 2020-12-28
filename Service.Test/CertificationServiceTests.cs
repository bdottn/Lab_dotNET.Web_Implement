using EntityOperation.Protocol;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Operation.Model;
using Service.Model;
using Service.Protocol;
using System;
using System.Text;

namespace Service.Test
{
    [TestClass]
    public sealed class CertificationServiceTests
    {
        private IConfig config;

        private ICertificationService service;

        [TestInitialize]
        public void TestInitialize()
        {
            this.config = Substitute.For<IConfig>();

            this.service = new CertificationService(this.config);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_Authenticate_憑證驗證成功_預期回傳success()
        {
            var acceptedCredential =
                new WebAPICredential()
                {
                    Key = "ccsa2#@!$@",
                    Value = "!@$#%SDSgswr",
                };

            this.config.AcceptedCredential.Returns(acceptedCredential);

            var credential = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{acceptedCredential.Key}:{acceptedCredential.Value}"));

            var actual = this.service.Authenticate(credential);

            Assert.AreEqual(ServiceResultType.Success, actual.ResultType);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void Service_Authenticate_憑證驗證失敗_預期回傳fail()
        {
            var acceptedCredential =
                new WebAPICredential()
                {
                    Key = "ccsa2#@!$@",
                    Value = "!@$#%SDSgswr",
                };

            this.config.AcceptedCredential.Returns(acceptedCredential);

            var credential = "abcdefgh";

            var actual = this.service.Authenticate(credential);

            Assert.AreEqual(ServiceResultType.Fail, actual.ResultType);
            Assert.IsNotNull(actual.Message);
        }
    }
}