using Operation.Model;
using Service.Model;
using System.Collections.Generic;

namespace Service.Protocol
{
    /// <summary>
    /// 客戶服務介面
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// 建立客戶資料
        /// </summary>
        /// <param name="customer">客戶資料</param>
        /// <returns>
        /// <para>建立成功：【ResultType】Success；【回傳值】客戶資料</para>
        /// <para>邏輯錯誤：【ResultType】Fail；【Message】失敗訊息</para>
        /// <para>發生例外：【ResultType】Exception；【Message】例外訊息</para>
        /// </returns>
        ServiceResult<Customer> Create(Customer customer);

        /// <summary>
        /// 查詢客戶資料（關鍵字：姓名）
        /// </summary>
        /// <param name="keyWord">姓名關鍵字</param>
        /// <returns>
        /// <para>查詢成功：【ResultType】Success；【回傳值】客戶資料集合</para>
        /// <para>發生例外：【ResultType】Exception；【Message】例外訊息</para>
        /// </returns>
        ServiceResult<List<Customer>> Search(string keyWord);

        /// <summary>
        /// 查詢客戶資料（主索引）
        /// </summary>
        /// <param name="customerId">客戶資料（主索引）</param>
        /// <returns>
        /// <para>查詢成功：【ResultType】Success；【回傳值】客戶資料</para>
        /// <para>查詢警告：【ResultType】Warning；【Message】警告訊息</para>
        /// <para>發生例外：【ResultType】Exception；【Message】例外訊息</para>
        /// </returns>
        ServiceResult<Customer> Find(int customerId);

        /// <summary>
        /// 更新客戶資料
        /// </summary>
        /// <param name="customerId">客戶資料（主索引）</param>
        /// <param name="customer">客戶資料</param>
        /// <returns>
        /// <para>更新成功：【ResultType】Success；【回傳值】客戶資料</para>
        /// <para>邏輯錯誤：【ResultType】Fail；【Message】失敗訊息</para>
        /// <para>發生例外：【ResultType】Exception；【Message】例外訊息</para>
        /// </returns>
        ServiceResult<Customer> Update(int customerId, Customer customer);

        /// <summary>
        /// 刪除客戶資料
        /// </summary>
        /// <param name="customerId">客戶資料（主索引）</param>
        /// <returns>
        /// <para>刪除成功：【ResultType】Success</para>
        /// <para>刪除警告：【ResultType】Warning；【Message】警告訊息</para>
        /// <para>發生例外：【ResultType】Exception；【Message】例外訊息</para>
        /// </returns>
        ServiceResult Delete(int customerId);
    }
}