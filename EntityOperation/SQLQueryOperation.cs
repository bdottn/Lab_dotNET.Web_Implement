using EntityOperation.Protocol;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EntityOperation
{
    sealed class SQLQueryOperation<TModel>
        : ISQLQueryOperation<TModel>
        where TModel : class
    {
        public List<Expression<Func<TModel, object>>> IncludeExpressions { get; private set; }

        public Expression<Func<TModel, bool>> QueryExpression { get; private set; }

        public Func<IQueryable<TModel>, IOrderedQueryable<TModel>> OrderBy { get; private set; }

        public int? PageSize { get; private set; }

        public int? PageIndex { get; private set; }

        public void Include(Expression<Func<TModel, object>> includeExpression)
        {
            if (this.IncludeExpressions == null)
            {
                this.IncludeExpressions = new List<Expression<Func<TModel, object>>>();
            }

            this.IncludeExpressions.Add(includeExpression);
        }

        public void And(Expression<Func<TModel, bool>> queryExpression)
        {
            if (this.QueryExpression == null)
            {
                this.QueryExpression = queryExpression;
            }
            else
            {
                this.QueryExpression = this.QueryExpression.And(queryExpression);
            }
        }

        public void Sort(Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy)
        {
            this.OrderBy = orderBy;
        }

        public void Paging(int pageSize, int pageIndex)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        public void Reset()
        {
            this.QueryExpression = null;
            this.OrderBy = null;
            this.PageIndex = null;
            this.PageSize = null;
        }
    }
}