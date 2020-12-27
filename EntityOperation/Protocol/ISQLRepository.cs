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
    }
}