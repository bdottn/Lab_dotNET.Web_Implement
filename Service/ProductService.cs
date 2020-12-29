using EntityOperation.Protocol;
using Operation.Model;
using Service.Model;
using Service.Protocol;
using System;
using System.Collections.Generic;

namespace Service
{
    sealed class ProductService : IProductService
    {
        #region 建構式注入

        private readonly ISQLRepository<Product> repository;
        private readonly ISQLQueryOperation<Product> queryOperation;

        public ProductService(ISQLRepository<Product> repository, ISQLQueryOperation<Product> queryOperation)
        {
            this.repository = repository;
            this.queryOperation = queryOperation;
        }

        #endregion

        public ServiceResult<Product> Create(Product product)
        {
            product.CreatedTime = DateTime.Now;
            product.LatestModifiedTime = DateTime.Now;

            var validationResult = this.repository.Validate(product);

            if (string.IsNullOrEmpty(validationResult) == false)
            {
                return
                    new ServiceResult<Product>()
                    {
                        ResultType = ServiceResultType.Fail,
                        Message = "屬性驗證錯誤：" + validationResult,
                    };
            }

            this.repository.Create(product);

            return
                new ServiceResult<Product>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = product,
                };
        }

        public ServiceResult<List<Product>> Search(string keyWord)
        {
            this.queryOperation.Reset();

            this.queryOperation.And(c => c.Name.Contains(keyWord));

            var products = this.repository.Query(this.queryOperation);

            return
                new ServiceResult<List<Product>>()
                {
                    ResultType = ServiceResultType.Success,
                    Value = products,
                };
        }

        public ServiceResult<Product> Find(int productId)
        {
            var model = this.GetSQLProduct(productId);

            if (model == null)
            {
                return
                    new ServiceResult<Product>()
                    {
                        ResultType = ServiceResultType.Warning,
                        Message = "資料庫不存在產品資料",
                    };
            }

            return
               new ServiceResult<Product>()
               {
                   ResultType = ServiceResultType.Success,
                   Value = model,
               };
        }

        public ServiceResult<Product> Update(int productId, Product product)
        {
            var model = this.GetSQLProduct(productId);

            if (model == null)
            {
                return
                    new ServiceResult<Product>()
                    {
                        ResultType = ServiceResultType.Fail,
                        Message = "資料庫不存在產品資料",
                    };
            }

            model.Name = product.Name;
            model.Price = product.Price;
            model.LatestModifiedTime = DateTime.Now;

            var validationResult = this.repository.Validate(model);

            if (string.IsNullOrEmpty(validationResult) == false)
            {
                return
                    new ServiceResult<Product>()
                    {
                        ResultType = ServiceResultType.Fail,
                        Message = "屬性驗證錯誤：" + validationResult,
                    };
            }

            this.repository.Update(model);

            return
               new ServiceResult<Product>()
               {
                   ResultType = ServiceResultType.Success,
                   Value = model,
               };
        }

        public ServiceResult Delete(int productId)
        {
            var model = this.GetSQLProduct(productId);

            if (model == null)
            {
                return
                    new ServiceResult()
                    {
                        ResultType = ServiceResultType.Warning,
                        Message = "資料庫不存在產品資料",
                    };
            }

            this.repository.Delete(model);

            return
               new ServiceResult()
               {
                   ResultType = ServiceResultType.Success,
               };
        }

        private Product GetSQLProduct(int productId)
        {
            return this.repository.Find(productId);
        }
    }
}