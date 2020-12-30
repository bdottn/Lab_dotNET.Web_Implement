using Operation.Model;
using Service.Model;
using System.Collections.Generic;

namespace Service.Protocol
{
    /// <summary>
    /// 訂單服務介面
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// 建立訂單資料
        /// </summary>
        /// <param name="order">訂單資料</param>
        /// <returns>
        /// <para>建立成功：【ResultType】Success；【回傳值】訂單資料</para>
        /// <para>邏輯錯誤：【ResultType】Fail；【Message】失敗訊息</para>
        /// <para>發生例外：【ResultType】Exception；【Message】例外訊息</para>
        /// </returns>
        ServiceResult<Order> Create(Order order);

        /// <summary>
        /// 查詢訂單資料
        /// </summary>
        /// <param name="orderId">訂單序號</param>
        /// <returns>
        /// <para>查詢成功：【ResultType】Success；【回傳值】訂單資料</para>
        /// <para>發生例外：【ResultType】Exception；【Message】例外訊息</para>
        /// </returns>
        ServiceResult<Order> Search(int orderId);

        /// <summary>
        /// 查詢訂單資料
        /// </summary>
        /// <param name="customerId">客戶序號</param>
        /// <returns>
        /// <para>查詢成功：【ResultType】Success；【回傳值】訂單資料集合</para>
        /// <para>發生例外：【ResultType】Exception；【Message】例外訊息</para>
        /// </returns>
        ServiceResult<List<Order>> SearchCustomerOrder(int customerId);

        /// <summary>
        /// 查詢訂單資料
        /// </summary>
        /// <param name="customerId">客戶序號</param>
        /// <param name="orderId">訂單序號</param>
        /// <returns>
        /// <para>查詢成功：【ResultType】Success；【回傳值】訂單資料</para>
        /// <para>發生例外：【ResultType】Exception；【Message】例外訊息</para>
        /// </returns>
        ServiceResult<Order> SearchCustomerOrder(int customerId, int orderId);
    }
}