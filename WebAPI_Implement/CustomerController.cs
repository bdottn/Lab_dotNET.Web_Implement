using AutoMapper;
using Operation.Model;
using Service.Model;
using Service.Protocol;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using View.Model;
using WebAPI_Implement.Extension;
using WebAPI_Implement.Interceptor;

namespace WebAPI_Implement
{
    /// <summary>
    /// 客戶控制器
    /// </summary>
    #region Swagger 通用設定
    [SwaggerResponseRemoveDefaults]
    [SwaggerResponse(HttpStatusCode.Forbidden, Description = "執行失敗。", Type = typeof(string))]
    [SwaggerResponse(HttpStatusCode.Conflict, Description = "執行錯誤。", Type = typeof(string))]
    [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "伺服器嚴重錯誤。", Type = typeof(string))]
    #endregion
    [Authorization]
    public sealed class CustomerController : ApiController
    {
        #region 建構式注入

        private readonly ICustomerService service;
        private readonly IMapper mapper;

        /// <summary>
        /// 客戶控制器
        /// </summary>
        public CustomerController(ICustomerService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        #endregion

        /// <summary>
        /// 建立客戶資料
        /// </summary>
        /// <param name="customer">客戶資料</param>
        [Route("customers")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Description = "建立成功。", Type = typeof(CustomerInfo))]
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

        /// <summary>
        /// 搜尋客戶資料
        /// </summary>
        /// <param name="customerId">客戶序號</param>
        [Route("customers/{customerId:int}")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "搜尋成功。", Type = typeof(CustomerInfo))]
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

        /// <summary>
        /// 搜尋客戶資料
        /// </summary>
        /// <param name="customerName">客戶姓名</param>
        [Route("customers/{customerName}")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "搜尋成功。", Type = typeof(CustomerInfo))]
        public IHttpActionResult SearchCustomerName(string customerName)
        {
            var result = this.service.Search(customerName);

            if (result.ResultType == ServiceResultType.Success)
            {
                var receipt = this.mapper.Map<List<CustomerInfo>>(result.Value);

                return this.Content(HttpStatusCode.OK, receipt);
            }
            else
            {
                var statusCode = result.ResultType.ToHttpStatusCode();

                return this.Content(statusCode, result.Message);
            }
        }

        /// <summary>
        /// 更新客戶資料
        /// </summary>
        /// <param name="customerId">客戶序號</param>
        /// <param name="customer">客戶資料</param>
        [Route("customers/{customerId:int}")]
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, Description = "更新成功。", Type = typeof(CustomerInfo))]
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

        /// <summary>
        /// 刪除客戶資料
        /// </summary>
        /// <param name="customerId">客戶序號</param>
        [Route("customers/{customerId:int}")]
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK, Description = "刪除成功。")]
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