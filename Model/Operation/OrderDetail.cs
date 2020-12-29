using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Operation.Model
{
    /// <summary>
    /// 訂單明細
    /// </summary>
    public class OrderDetail
    {
        /// <summary>
        /// 訂單明細序號
        /// </summary>
        [Required]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 訂單序號
        /// </summary>
        [Required]
        public int OrderId { get; set; }

        /// <summary>
        /// 訂單資料
        /// </summary>
        [ForeignKey(nameof(OrderId))]
        [JsonIgnore]
        public Order Order { get; set; }

        /// <summary>
        /// 產品序號
        /// </summary>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// 產品資料
        /// </summary>
        [ForeignKey(nameof(ProductId))]
        [JsonIgnore]
        public Product Product { get; set; }

        /// <summary>
        /// 產品數量
        /// </summary>
        [Required]
        public int Quantity { get; set; }
    }
}