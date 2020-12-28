using System.Collections.Generic;

namespace EntityOperation.Protocol
{
    /// <summary>
    /// 資料庫物件操作介面
    /// </summary>
    /// <typeparam name="TModel">資料庫物件型別</typeparam>
    public interface ISQLRepository<TModel>
        where TModel : class
    {
        /// <summary>
        /// 建立資料庫物件
        /// </summary>
        /// <param name="model">資料庫物件</param>
        void Create(TModel model);

        /// <summary>
        /// 查詢資料庫物件（資料庫查詢操作物件）
        /// </summary>
        /// <param name="queryOperation">資料庫查詢操作物件</param>
        /// <returns>資料庫物件集合</returns>
        List<TModel> Query(ISQLQueryOperation<TModel> queryOperation);

        /// <summary>
        /// 查詢資料庫物件（主索引）
        /// </summary>
        /// <param name="keyValues">主索引值</param>
        /// <returns>資料庫物件</returns>
        TModel Find(params object[] keyValues);

        /// <summary>
        /// 更新資料庫物件
        /// </summary>
        /// <param name="model">資料庫物件</param>
        void Update(TModel model);

        /// <summary>
        /// 刪除資料庫物件
        /// </summary>
        /// <param name="model">資料庫物件</param>
        void Delete(TModel model);

        /// <summary>
        /// 驗證資料庫物件
        /// </summary>
        /// <param name="model">資料庫物件</param>
        /// <returns>
        /// 驗證錯誤訊息
        /// <para>訊息為空值：驗證結果無錯誤</para>
        /// </returns>
        string Validate(TModel model);
    }
}