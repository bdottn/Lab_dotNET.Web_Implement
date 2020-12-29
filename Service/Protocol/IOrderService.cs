using Operation.Model;
using Service.Model;

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
    }
}