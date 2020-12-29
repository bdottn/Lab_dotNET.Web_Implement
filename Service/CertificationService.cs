using EntityOperation.Protocol;
using Service.Model;
using Service.Protocol;
using System;
using System.Text;

namespace Service
{
    sealed class CertificationService : ICertificationService
    {
        #region 建構式注入

        private readonly IConfig config;

        public CertificationService(IConfig config)
        {
            this.config = config;
        }

        #endregion

        public ServiceResult Authenticate(string credential)
        {
            var acceptedCredential = this.config.AcceptedCredential;

            var authorization = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{acceptedCredential.Key}:{acceptedCredential.Value}"));

            if (authorization.Equals(credential))
            {
                return
                    new ServiceResult()
                    {
                        ResultType = ServiceResultType.Success,
                    };
            }
            else
            {
                return
                    new ServiceResult()
                    {
                        ResultType = ServiceResultType.Fail,
                        Message = "憑證失敗！",
                    };
            }
        }
    }
}