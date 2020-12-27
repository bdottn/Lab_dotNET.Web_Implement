using EntityOperation.Protocol;
using Operation.Model;
using Service.Model;
using Service.Protocol;
using System;

namespace Service
{
    sealed class CustomerService : ICustomerService
    {
        #region 建構式注入

        private readonly ISQLRepository<Customer> repository;

        public CustomerService(ISQLRepository<Customer> repository)
        {
            this.repository = repository;
        }

        #endregion

        public ServiceResult<Customer> Create(Customer customer)
        {
            customer.CreatedTime = DateTime.Now;
            customer.LatestModifiedTime = DateTime.Now;

            var validationResult = this.repository.Validate(customer);

            if (string.IsNullOrEmpty(validationResult) == false)
            {
                return
                    new ServiceResult<Customer>()
                    {
                        ResultType = ServiceResultType.Fail,
                        Message = "屬性驗證錯誤：" + validationResult,
                    };
            }

            this.repository.Create(customer);

            return
                new ServiceResult<Customer>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = customer,
                };
        }

        public ServiceResult<Customer> Find(Customer customer)
        {
            var model = this.GetSQLCustomer(customer);

            if (model == null)
            {
                return
                    new ServiceResult<Customer>()
                    {
                        ResultType = ServiceResultType.Warning,
                        Message = "資料庫不存在客戶資料",
                    };
            }

            return
               new ServiceResult<Customer>()
               {
                   ResultType = ServiceResultType.Success,
                   Value = model,
               };
        }

        public ServiceResult<Customer> Update(Customer customer)
        {
            var model = this.GetSQLCustomer(customer);

            if (model == null)
            {
                return
                    new ServiceResult<Customer>()
                    {
                        ResultType = ServiceResultType.Fail,
                        Message = "資料庫不存在客戶資料",
                    };
            }

            model.Name = customer.Name;
            model.Phone = customer.Phone;
            model.LatestModifiedTime = DateTime.Now;

            var validationResult = this.repository.Validate(model);

            if (string.IsNullOrEmpty(validationResult) == false)
            {
                return
                    new ServiceResult<Customer>()
                    {
                        ResultType = ServiceResultType.Fail,
                        Message = "屬性驗證錯誤：" + validationResult,
                    };
            }

            this.repository.Update(model);

            return
               new ServiceResult<Customer>()
               {
                   ResultType = ServiceResultType.Success,
                   Value = model,
               };
        }

        public ServiceResult Delete(Customer customer)
        {
            var model = this.GetSQLCustomer(customer);

            if (model == null)
            {
                return
                    new ServiceResult()
                    {
                        ResultType = ServiceResultType.Warning,
                        Message = "資料庫不存在客戶資料",
                    };
            }

            this.repository.Delete(customer);

            return
               new ServiceResult()
               {
                   ResultType = ServiceResultType.Success,
               };
        }

        private Customer GetSQLCustomer(Customer customer)
        {
            return this.repository.Find(customer.Id);
        }
    }
}