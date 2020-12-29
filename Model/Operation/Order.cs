using Operation.Model.CustomerAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Operation.Model
{
    /// <summary>
    /// 訂單資料
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 訂單序號
        /// </summary>
        [Required]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 客戶序號
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 客戶資料
        /// </summary>
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        /// <summary>
        /// 訂單地址
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Address { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [Required]
        [MSSQLDateTime]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 最後更新時間
        /// </summary>
        [Required]
        [MSSQLDateTime]
        public DateTime LatestModifiedTime { get; set; }

        /// <summary>
        /// 訂單明細集合
        /// </summary>
        public List<OrderDetail> OrderDetails { get; set; }
    }
}