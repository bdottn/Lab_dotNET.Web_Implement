using EntityOperation.Protocol;
using Operation.Model;
using Service.Model;
using Service.Protocol;
using System;
using System.Collections.Generic;

namespace Service
{
    sealed class CustomerService : ICustomerService
    {
        #region 建構式注入

        private readonly ISQLRepository<Customer> repository;
        private readonly ISQLQueryOperation<Customer> queryOperation;

        public CustomerService(ISQLRepository<Customer> repository, ISQLQueryOperation<Customer> queryOperation)
        {
            this.repository = repository;
            this.queryOperation = queryOperation;
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

        public ServiceResult<List<Customer>> Search(string keyWord)
        {
            this.queryOperation.Reset();

            this.queryOperation.And(c => c.Name.Contains(keyWord));

            var customers = this.repository.Query(this.queryOperation);

            return
                new ServiceResult<List<Customer>>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = customers,
                };
        }

        public ServiceResult<Customer> Find(int customerId)
        {
            var model = this.GetSQLCustomer(customerId);

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

        public ServiceResult<Customer> Update(int customerId, Customer customer)
        {
            var model = this.GetSQLCustomer(customerId);

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

        public ServiceResult Delete(int customerId)
        {
            var model = this.GetSQLCustomer(customerId);

            if (model == null)
            {
                return
                    new ServiceResult()
                    {
                        ResultType = ServiceResultType.Warning,
                        Message = "資料庫不存在客戶資料",
                    };
            }

            this.repository.Delete(model);

            return
               new ServiceResult()
               {
                   ResultType = ServiceResultType.Success,
               };
        }

        private Customer GetSQLCustomer(int customerId)
        {
            return this.repository.Find(customerId);
        }
    }
}