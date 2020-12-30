using EntityOperation.Protocol;
using Operation.Model;
using Service.Model;
using Service.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    sealed class OrderService : IOrderService
    {
        #region 建構式注入

        private readonly ISQLRepository<Customer> customerRepository;
        private readonly ISQLRepository<Order> repository;
        private readonly ISQLQueryOperation<Order> queryOperation;
        private readonly ISQLRepository<Product> productRepository;

        public OrderService(
            ISQLRepository<Customer> customerRepository,
            ISQLRepository<Order> repository,
            ISQLQueryOperation<Order> queryOperation,
            ISQLRepository<Product> productRepository)
        {
            this.customerRepository = customerRepository;
            this.repository = repository;
            this.queryOperation = queryOperation;
            this.productRepository = productRepository;
        }

        #endregion

        public ServiceResult<Order> Create(Order order)
        {
            if (order.OrderDetails == null || order.OrderDetails.Count == 0)
            {
                return
                    new ServiceResult<Order>()
                    {
                        ResultType = ServiceResultType.Fail,
                        Message = "訂單中沒有商品明細",
                    };
            }

            var customer = this.customerRepository.Find(order.CustomerId);

            if (customer == null)
            {
                return
                    new ServiceResult<Order>()
                    {
                        ResultType = ServiceResultType.Fail,
                        Message = "資料庫不存在客戶資料",
                    };
            }

            foreach (var orderDetail in order.OrderDetails)
            {
                var product = this.productRepository.Find(orderDetail.ProductId);

                if (product == null)
                {
                    return
                        new ServiceResult<Order>()
                        {
                            ResultType = ServiceResultType.Fail,
                            Message = "資料庫不存在產品資料",
                        };
                }
            }

            order.CreatedTime = DateTime.Now;
            order.LatestModifiedTime = DateTime.Now;

            var validationResult = this.repository.Validate(order);

            if (string.IsNullOrEmpty(validationResult) == false)
            {
                return
                    new ServiceResult<Order>()
                    {
                        ResultType = ServiceResultType.Fail,
                        Message = "屬性驗證錯誤：" + validationResult,
                    };
            }

            this.repository.Create(order);

            return
                new ServiceResult<Order>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = order,
                };
        }

        public ServiceResult<Order> Search(int orderId)
        {
            this.queryOperation.Reset();

            this.queryOperation.And(o => o.Id == orderId);

            this.queryOperation.Include(o => o.Customer);
            this.queryOperation.Include(o => o.OrderDetails);
            this.queryOperation.Include(o => o.OrderDetails.Select(od => od.Product));

            var order = this.repository.Query(this.queryOperation).SingleOrDefault();

            return
                new ServiceResult<Order>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = order,
                };
        }

        public ServiceResult<List<Order>> SearchCustomerOrder(int customerId)
        {
            this.queryOperation.Reset();

            this.queryOperation.And(o => o.CustomerId == customerId);

            this.queryOperation.Include(o => o.Customer);
            this.queryOperation.Include(o => o.OrderDetails);
            this.queryOperation.Include(o => o.OrderDetails.Select(od => od.Product));

            var orders = this.repository.Query(this.queryOperation);

            return
                new ServiceResult<List<Order>>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = orders,
                };
        }

        public ServiceResult<Order> SearchCustomerOrder(int customerId, int orderId)
        {
            this.queryOperation.Reset();

            this.queryOperation.And(o => o.CustomerId == customerId);
            this.queryOperation.And(o => o.Id == orderId);

            this.queryOperation.Include(o => o.Customer);
            this.queryOperation.Include(o => o.OrderDetails);
            this.queryOperation.Include(o => o.OrderDetails.Select(od => od.Product));

            var order = this.repository.Query(this.queryOperation).SingleOrDefault();

            return
                new ServiceResult<Order>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = order,
                };
        }
    }
}