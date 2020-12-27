using EntityOperation.Model;
using System.Data.Entity;

namespace EntityOperation
{
    sealed class LabContext : DbContext
    {
        public LabContext()
            : base(@"Server=.;Database=〖.NET：Lab〗Web_Implement;Integrated Security=True;")
        {
        }

        /// <summary>
        /// 客戶
        /// </summary>
        public DbSet<Customer> Customers { get; set; }
    }
}