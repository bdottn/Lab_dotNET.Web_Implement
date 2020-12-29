using System.Collections.Generic;

namespace View.Model
{
    /// <summary>
    /// 訂單資料
    /// </summary>
    public sealed class OrderData
    {
        /// <summary>
        /// 客戶序號
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 訂單地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 訂單明細集合
        /// </summary>
        public List<OrderDetailData> OrderDetails { get; set; }
    }
}