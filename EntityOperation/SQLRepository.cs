using EntityOperation.Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace EntityOperation
{
    sealed class SQLRepository<TModel>
        : ISQLRepository<TModel>
        where TModel : class
    {
        public void Create(TModel model)
        {
            using (var context = this.GetContext())
            {
                context.Set<TModel>().Add(model);
                context.SaveChanges();
            }
        }

        public List<TModel> Query(ISQLQueryOperation<TModel> queryOperation)
        {
            using (var context = this.GetContext())
            {
                IQueryable<TModel> query = context.Set<TModel>().AsNoTracking();

                if (queryOperation.QueryExpression != null)
                {
                    query = query.Where(queryOperation.QueryExpression);
                }

                if (queryOperation.OrderBy != null)
                {
                    query = queryOperation.OrderBy(query);

                    if (queryOperation.PageIndex.HasValue && queryOperation.PageSize.HasValue)
                    {
                        query = query.Skip((queryOperation.PageIndex.Value - 1) * queryOperation.PageSize.Value);
                        query = query.Take(queryOperation.PageSize.Value);
                    }
                }

                return query.ToList();
            }
        }

        public TModel Find(params object[] keyValues)
        {
            using (var context = this.GetContext())
            {
                return context.Set<TModel>().Find(keyValues);
            }
        }

        public void Update(TModel model)
        {
            using (var context = this.GetContext())
            {
                context.Entry(model).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(TModel model)
        {
            using (var context = this.GetContext())
            {
                context.Entry(model).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public string Validate(TModel model)
        {
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(model, new ValidationContext(model), validationResults, true);

            return string.Join(Environment.NewLine, validationResults);
        }

        private LabContext GetContext()
        {
            return new LabContext();
        }
    }
}