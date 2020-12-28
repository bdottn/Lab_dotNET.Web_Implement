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
        ServiceResult<Customer> Create(Customer customer);

        /// <summary>
        /// 查詢客戶資料（關鍵字：姓名）
        /// </summary>
        /// <param name="keyWord">姓名關鍵字</param>
        ServiceResult<List<Customer>> Search(string keyWord);

        /// <summary>
        /// 查詢客戶資料（主索引）
        /// </summary>
        /// <param name="customerId">客戶資料（主索引）</param>
        ServiceResult<Customer> Find(int customerId);

        /// <summary>
        /// 更新客戶資料
        /// </summary>
        /// <param name="customerId">客戶資料（主索引）</param>
        /// <param name="customer">客戶資料</param>
        ServiceResult<Customer> Update(int customerId, Customer customer);

        /// <summary>
        /// 刪除客戶資料
        /// </summary>
        /// <param name="customerId">客戶資料（主索引）</param>
        ServiceResult Delete(int customerId);
    }
}