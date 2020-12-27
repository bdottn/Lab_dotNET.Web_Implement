using System;

namespace View.Model
{
    /// <summary>
    /// 客戶資訊
    /// </summary>
    public sealed class CustomerInfo
    {
        /// <summary>
        /// 客戶序號
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 客戶名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 客戶電話
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 最後更新時間
        /// </summary>
        public DateTime LatestModifiedTime { get; set; }
    }
}