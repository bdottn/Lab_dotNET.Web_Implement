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
    }
}