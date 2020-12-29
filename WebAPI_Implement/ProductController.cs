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
    /// 產品控制器
    /// </summary>
    #region Swagger 通用設定
    [SwaggerResponseRemoveDefaults]
    [SwaggerResponse(HttpStatusCode.Forbidden, Description = "執行失敗。", Type = typeof(string))]
    [SwaggerResponse(HttpStatusCode.Conflict, Description = "執行錯誤。", Type = typeof(string))]
    [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "伺服器嚴重錯誤。", Type = typeof(string))]
    #endregion
    [Authorization]
    public sealed class ProductController : ApiController
    {
        #region 建構式注入

        private readonly IProductService service;
        private readonly IMapper mapper;

        /// <summary>
        /// 產品控制器
        /// </summary>
        public ProductController(IProductService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        #endregion

        /// <summary>
        /// 建立產品資料
        /// </summary>
        /// <param name="product">產品資料</param>
        [Route("products")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Description = "建立成功。", Type = typeof(ProductInfo))]
        public IHttpActionResult Create([FromBody]ProductData product)
        {
            var model = this.mapper.Map<Product>(product);

            var result = this.service.Create(model);

            if (result.ResultType == ServiceResultType.Success)
            {
                var receipt = this.mapper.Map<ProductInfo>(result.Value);

                return this.Content(HttpStatusCode.Created, receipt);
            }
            else
            {
                var statusCode = result.ResultType.ToHttpStatusCode();

                return this.Content(statusCode, result.Message);
            }
        }

        /// <summary>
        /// 搜尋產品資料
        /// </summary>
        /// <param name="productId">產品序號</param>
        [Route("products/{productId:int}")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "搜尋成功。", Type = typeof(ProductInfo))]
        public IHttpActionResult Find(int productId)
        {
            var result = this.service.Find(productId);

            if (result.ResultType == ServiceResultType.Success)
            {
                var receipt = this.mapper.Map<ProductInfo>(result.Value);

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
        /// 搜尋產品資料
        /// </summary>
        /// <param name="productName">產品姓名</param>
        [Route("products/{productName}")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "搜尋成功。", Type = typeof(ProductInfo))]
        public IHttpActionResult SearchProductName(string productName)
        {
            var result = this.service.Search(productName);

            if (result.ResultType == ServiceResultType.Success)
            {
                var receipt = this.mapper.Map<List<ProductInfo>>(result.Value);

                return this.Content(HttpStatusCode.OK, receipt);
            }
            else
            {
                var statusCode = result.ResultType.ToHttpStatusCode();

                return this.Content(statusCode, result.Message);
            }
        }

        /// <summary>
        /// 更新產品資料
        /// </summary>
        /// <param name="productId">產品序號</param>
        /// <param name="product">產品資料</param>
        [Route("products/{productId:int}")]
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, Description = "更新成功。", Type = typeof(ProductInfo))]
        public IHttpActionResult Update(int productId, [FromBody]ProductData product)
        {
            var model = this.mapper.Map<Product>(product);

            var result = this.service.Update(productId, model);

            if (result.ResultType == ServiceResultType.Success)
            {
                var receipt = this.mapper.Map<ProductInfo>(result.Value);

                return this.Content(HttpStatusCode.OK, receipt);
            }
            else
            {
                var statusCode = result.ResultType.ToHttpStatusCode();

                return this.Content(statusCode, result.Message);
            }
        }

        /// <summary>
        /// 刪除產品資料
        /// </summary>
        /// <param name="productId">產品序號</param>
        [Route("products/{productId:int}")]
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK, Description = "刪除成功。")]
        public IHttpActionResult Delete(int productId)
        {
            var result = this.service.Delete(productId);

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