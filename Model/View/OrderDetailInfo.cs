namespace View.Model
{
    /// <summary>
    /// 訂單明細資訊
    /// </summary>
    public sealed class OrderDetailInfo
    {
        /// <summary>
        /// 訂單明細序號
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 產品資訊
        /// </summary>
        public ProductInfo Product { get; set; }

        /// <summary>
        /// 產品數量
        /// </summary>
        public int Quantity { get; set; }
    }
}