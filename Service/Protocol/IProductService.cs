using Operation.Model;
using Service.Model;
using System.Collections.Generic;

namespace Service.Protocol
{
    /// <summary>
    /// 產品服務介面
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// 建立產品資料
        /// </summary>
        /// <param name="product">產品資料</param>
        /// <returns>
        /// <para>建立成功：【ResultType】Success；【回傳值】產品資料</para>
        /// <para>邏輯錯誤：【ResultType】Fail；【Message】失敗訊息</para>
        /// <para>發生例外：【ResultType】Exception；【Message】例外訊息</para>
        /// </returns>
        ServiceResult<Product> Create(Product product);

        /// <summary>
        /// 查詢產品資料（關鍵字：姓名）
        /// </summary>
        /// <param name="keyWord">姓名關鍵字</param>
        /// <returns>
        /// <para>查詢成功：【ResultType】Success；【回傳值】產品資料集合</para>
        /// <para>發生例外：【ResultType】Exception；【Message】例外訊息</para>
        /// </returns>
        ServiceResult<List<Product>> Search(string keyWord);

        /// <summary>
        /// 查詢產品資料（主索引）
        /// </summary>
        /// <param name="productId">產品資料（主索引）</param>
        /// <returns>
        /// <para>查詢成功：【ResultType】Success；【回傳值】產品資料</para>
        /// <para>查詢警告：【ResultType】Warning；【Message】警告訊息</para>
        /// <para>發生例外：【ResultType】Exception；【Message】例外訊息</para>
        /// </returns>
        ServiceResult<Product> Find(int productId);

        /// <summary>
        /// 更新產品資料
        /// </summary>
        /// <param name="productId">產品資料（主索引）</param>
        /// <param name="product">產品資料</param>
        /// <returns>
        /// <para>更新成功：【ResultType】Success；【回傳值】產品資料</para>
        /// <para>邏輯錯誤：【ResultType】Fail；【Message】失敗訊息</para>
        /// <para>發生例外：【ResultType】Exception；【Message】例外訊息</para>
        /// </returns>
        ServiceResult<Product> Update(int productId, Product product);

        /// <summary>
        /// 刪除產品資料
        /// </summary>
        /// <param name="productId">產品資料（主索引）</param>
        /// <returns>
        /// <para>刪除成功：【ResultType】Success</para>
        /// <para>刪除警告：【ResultType】Warning；【Message】警告訊息</para>
        /// <para>發生例外：【ResultType】Exception；【Message】例外訊息</para>
        /// </returns>
        ServiceResult Delete(int productId);
    }
}