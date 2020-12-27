using Operation.Model;
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
        /// <param name="customer">客戶資料</param>
        ServiceResult<Customer> Create(Customer customer);

        /// <summary>
        /// 查詢客戶資料（主索引）
        /// </summary>
        /// <param name="customer">客戶資料</param>
        ServiceResult<Customer> Find(Customer customer);

        /// <summary>
        /// 更新客戶資料
        /// </summary>
        /// <param name="customer">客戶資料</param>
        ServiceResult<Customer> Update(Customer customer);

        /// <summary>
        /// 刪除客戶資料
        /// </summary>
        /// <param name="customer">客戶資料</param>
        ServiceResult Delete(Customer customer);
    }
}