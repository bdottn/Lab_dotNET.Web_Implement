using System;
using System.Linq;
using System.Linq.Expressions;

namespace EntityOperation.Protocol
{
    /// <summary>
    /// 資料庫查詢操作介面
    /// </summary>
    /// <typeparam name="TModel">資料庫物件型別</typeparam>
    public interface ISQLQueryOperation<TModel>
        where TModel : class
    {
        /// <summary>
        /// 查詢表達式
        /// </summary>
        Expression<Func<TModel, bool>> QueryExpression { get; }

        /// <summary>
        /// 排序表達式
        /// </summary>
        Func<IQueryable<TModel>, IOrderedQueryable<TModel>> OrderBy { get; }

        /// <summary>
        /// 分頁筆數
        /// </summary>
        int? PageSize { get; }

        /// <summary>
        /// 分頁頁次
        /// </summary>
        int? PageIndex { get; }

        /// <summary>
        /// 增加查詢表達式
        /// <para>在現有查詢表達式上增加查詢表達式</para>
        /// </summary>
        /// <param name="queryExpression">查詢表達式</param>
        void And(Expression<Func<TModel, bool>> queryExpression);

        /// <summary>
        /// 排序表達式
        /// </summary>
        /// <param name="orderBy">排序表達式</param>
        void Sort(Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy);

        /// <summary>
        /// 分頁
        /// </summary>
        /// <param name="pageSize">分頁筆數</param>
        /// <param name="pageIndex">分頁頁次</param>
        void Paging(int pageSize, int pageIndex);

        /// <summary>
        /// 重置
        /// </summary>
        void Reset();
    }
}