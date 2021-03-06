﻿using Operation.Model.CustomerAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Operation.Model
{
    /// <summary>
    /// 客戶資料
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// 客戶序號
        /// </summary>
        [Required]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 客戶名稱
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// 客戶電話
        /// </summary>
        [MaxLength(24)]
        public string Phone { get; set; }

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
        /// 訂單資料集合
        /// </summary>
        public List<Order> Orders { get; set; }
    }
}