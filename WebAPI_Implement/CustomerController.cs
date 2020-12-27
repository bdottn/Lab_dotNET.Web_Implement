using Operation.Model;
using Service.Model;
using Service.Protocol;
using System;
using System.Net;
using System.Web.Http;

namespace WebAPI_Implement
{
    /// <summary>
    /// 客戶控制器
    /// </summary>
    public sealed class CustomerController : ApiController
    {
        private readonly ICustomerService service;

        public CustomerController(ICustomerService service)
        {
            this.service = service;
        }

        [Route("customers")]
        [HttpPost]
        public IHttpActionResult Create([FromBody]Customer customer)
        {
            var result = this.service.Create(customer);

            if (result.ResultType == ServiceResultType.Success)
            {
                return this.Content(HttpStatusCode.Created, result.Value);
            }
            else if (result.ResultType == ServiceResultType.Warning)
            {
                return this.Content(HttpStatusCode.OK, result.Value);
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