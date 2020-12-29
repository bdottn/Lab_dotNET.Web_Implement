using System;
using System.Collections.Generic;

namespace View.Model
{
    /// <summary>
    /// 訂單資訊
    /// </summary>
    public sealed class OrderInfo
    {
        /// <summary>
        /// 訂單序號
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 客戶資訊
        /// </summary>
        public CustomerInfo Customer { get; set; }

        /// <summary>
        /// 訂單地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 最後更新時間
        /// </summary>
        public DateTime LatestModifiedTime { get; set; }

        /// <summary>
        /// 訂單明細集合
        /// </summary>
        public List<OrderDetailInfo> OrderDetails { get; set; }
    }
}