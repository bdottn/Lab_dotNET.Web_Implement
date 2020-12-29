using System;

namespace View.Model
{
    /// <summary>
    /// 產品資訊
    /// </summary>
    public sealed class ProductInfo
    {
        /// <summary>
        /// 產品序號
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 產品名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 產品價格
        /// </summary>
        public decimal Price { get; set; }

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