using Operation.Model.CustomerAttribute;
using System;
using System.ComponentModel.DataAnnotations;

namespace Operation.Model
{
    /// <summary>
    /// 產品
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 產品序號
        /// </summary>
        [Required]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 產品名稱
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// 產品價格
        /// </summary>
        [Required]
        public decimal Price { get; set; }

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
    }
}