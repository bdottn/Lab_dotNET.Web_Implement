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
    }
}