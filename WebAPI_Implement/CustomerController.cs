using AutoMapper;
using Operation.Model;
using Service.Model;
using Service.Protocol;
using System;
using System.Net;
using System.Web.Http;
using View.Model;

namespace WebAPI_Implement
{
    /// <summary>
    /// 客戶控制器
    /// </summary>
    public sealed class CustomerController : ApiController
    {
        #region 建構式注入

        private readonly ICustomerService service;
        private readonly IMapper mapper;

        public CustomerController(ICustomerService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        #endregion

        [Route("customers")]
        [HttpPost]
        public IHttpActionResult Create([FromBody]CustomerData customer)
        {
            var model = this.mapper.Map<Customer>(customer);

            var result = this.service.Create(model);

            if (result.ResultType == ServiceResultType.Success)
            {
                var receipt = this.mapper.Map<CustomerInfo>(result.Value);

                return this.Content(HttpStatusCode.Created, receipt);
            }
            else if (result.ResultType == ServiceResultType.Fail)
            {
                return this.Content(HttpStatusCode.Forbidden, result.Message);
            }
            else if (result.ResultType == ServiceResultType.Exception)
            {
                return this.Content(HttpStatusCode.Conflict, result.Message);
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}