using AutoMapper;
using Operation.Model;
using Service.Model;
using Service.Protocol;
using System.Net;
using System.Web.Http;
using View.Model;
using WebAPI_Implement.Extension;

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
            else
            {
                var statusCode = result.ResultType.ToHttpStatusCode();

                return this.Content(statusCode, result.Message);
            }
        }

        [Route("customers/{customerId:int}")]
        [HttpGet]
        public IHttpActionResult Find(int customerId)
        {
            var result = this.service.Find(customerId);

            if (result.ResultType == ServiceResultType.Success)
            {
                var receipt = this.mapper.Map<CustomerInfo>(result.Value);

                return this.Content(HttpStatusCode.OK, receipt);
            }
            else if (result.ResultType == ServiceResultType.Warning)
            {
                return this.Content(HttpStatusCode.OK, result.Message);
            }
            else
            {
                var statusCode = result.ResultType.ToHttpStatusCode();

                return this.Content(statusCode, result.Message);
            }
        }

        [Route("customers/{customerId:int}")]
        [HttpPut]
        public IHttpActionResult Update(int customerId, [FromBody]CustomerData customer)
        {
            var model = this.mapper.Map<Customer>(customer);

            var result = this.service.Update(customerId, model);

            if (result.ResultType == ServiceResultType.Success)
            {
                var receipt = this.mapper.Map<CustomerInfo>(result.Value);

                return this.Content(HttpStatusCode.OK, receipt);
            }
            else
            {
                var statusCode = result.ResultType.ToHttpStatusCode();

                return this.Content(statusCode, result.Message);
            }
        }

        [Route("customers/{customerId:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int customerId)
        {
            var result = this.service.Delete(customerId);

            if (result.ResultType == ServiceResultType.Success)
            {
                return this.StatusCode(HttpStatusCode.OK);
            }
            else if (result.ResultType == ServiceResultType.Warning)
            {
                return this.Content(HttpStatusCode.OK, result.Message);
            }
            else
            {
                var statusCode = result.ResultType.ToHttpStatusCode();

                return this.Content(statusCode, result.Message);
            }
        }
    }
}