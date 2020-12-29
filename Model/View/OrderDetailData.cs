namespace View.Model
{
    /// <summary>
    /// 訂單明細資料
    /// </summary>
    public sealed class OrderDetailData
    {
        /// <summary>
        /// 產品序號
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 產品數量
        /// </summary>
        public int Quantity { get; set; }
    }
}