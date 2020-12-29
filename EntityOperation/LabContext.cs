using Operation.Model;
using System.Data.Entity;

namespace EntityOperation
{
    sealed class LabContext : DbContext
    {
        public LabContext()
            : base(Config.Instance.MSSQLConnectionString)
        {
        }

        /// <summary>
        /// 客戶
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// 訂單
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// 訂單明細
        /// </summary>
        public DbSet<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// 產品
        /// </summary>
        public DbSet<Product> Products { get; set; }
    }
}