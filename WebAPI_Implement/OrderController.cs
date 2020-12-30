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
    /// 訂單控制器
    /// </summary>
    #region Swagger 通用設定
    [SwaggerResponseRemoveDefaults]
    [SwaggerResponse(HttpStatusCode.Forbidden, Description = "執行失敗。", Type = typeof(string))]
    [SwaggerResponse(HttpStatusCode.Conflict, Description = "執行錯誤。", Type = typeof(string))]
    [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "伺服器嚴重錯誤。", Type = typeof(string))]
    #endregion
    [Authorization]
    public sealed class OrderController : ApiController
    {
        #region 建構式注入

        private readonly IOrderService service;
        private readonly IMapper mapper;

        /// <summary>
        /// 訂單控制器
        /// </summary>
        public OrderController(IOrderService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        #endregion

        /// <summary>
        /// 建立訂單資料
        /// </summary>
        /// <param name="order">訂單資料</param>
        [Route("orders")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Description = "建立成功。", Type = typeof(OrderInfo))]
        public IHttpActionResult Create([FromBody]OrderData order)
        {
            var model = this.mapper.Map<Order>(order);

            var result = this.service.Create(model);

            if (result.ResultType == ServiceResultType.Success)
            {
                var receipt = this.mapper.Map<OrderInfo>(result.Value);

                return this.Content(HttpStatusCode.Created, receipt);
            }
            else
            {
                var statusCode = result.ResultType.ToHttpStatusCode();

                return this.Content(statusCode, result.Message);
            }
        }

        /// <summary>
        /// 建立訂單資料
        /// </summary>
        /// <param name="customerId">客戶序號</param>
        /// <param name="order">訂單資料</param>
        [Route("customers/{customerId:int}/orders")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Description = "建立成功。", Type = typeof(OrderInfo))]
        public IHttpActionResult Create(int customerId, [FromBody]OrderData order)
        {
            order.CustomerId = customerId;

            return this.Create(order);
        }

        /// <summary>
        /// 搜尋訂單資料
        /// </summary>
        /// <param name="orderId">訂單序號</param>
        [Route("orders/{orderId:int}")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "搜尋成功。", Type = typeof(OrderInfo))]
        public IHttpActionResult Search(int orderId)
        {
            var result = this.service.Search(orderId);

            if (result.ResultType == ServiceResultType.Success)
            {
                var receipt = this.mapper.Map<OrderInfo>(result.Value);

                return this.Content(HttpStatusCode.OK, receipt);
            }
            else
            {
                var statusCode = result.ResultType.ToHttpStatusCode();

                return this.Content(statusCode, result.Message);
            }
        }

        /// <summary>
        /// 搜尋訂單資料
        /// </summary>
        /// <param name="customerId">客戶序號</param>
        [Route("customers/{customerId:int}/orders")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "搜尋成功。", Type = typeof(List<OrderInfo>))]
        public IHttpActionResult SearchCustomerOrder(int customerId)
        {
            var result = this.service.SearchCustomerOrder(customerId);

            if (result.ResultType == ServiceResultType.Success)
            {
                var receipt = this.mapper.Map<List<OrderInfo>>(result.Value);

                return this.Content(HttpStatusCode.OK, receipt);
            }
            else
            {
                var statusCode = result.ResultType.ToHttpStatusCode();

                return this.Content(statusCode, result.Message);
            }
        }

        /// <summary>
        /// 搜尋訂單資料
        /// </summary>
        /// <param name="customerId">客戶序號</param>
        /// <param name="orderId">訂單序號</param>
        [Route("customers/{customerId:int}/orders/{orderId:int}")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "搜尋成功。", Type = typeof(OrderInfo))]
        public IHttpActionResult SearchCustomerOrder(int customerId, int orderId)
        {
            var result = this.service.SearchCustomerOrder(customerId, orderId);

            if (result.ResultType == ServiceResultType.Success)
            {
                var receipt = this.mapper.Map<OrderInfo>(result.Value);

                return this.Content(HttpStatusCode.OK, receipt);
            }
            else
            {
                var statusCode = result.ResultType.ToHttpStatusCode();

                return this.Content(statusCode, result.Message);
            }
        }
    }
}