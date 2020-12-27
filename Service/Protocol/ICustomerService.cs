using EntityOperation.Model;
using Service.Model;

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
        /// <param name="customer">客戶</param>
        /// <returns>Service 執行結果（回傳值）</returns>
        ServiceResult<Customer> Create(Customer customer);
    }
}