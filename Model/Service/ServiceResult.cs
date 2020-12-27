namespace Service.Model
{
    /// <summary>
    /// Service 執行結果
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// Service 執行結果類型
        /// </summary>
        public ServiceResultType ResultType { get; set; }

        /// <summary>
        /// 相關訊息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// Service 執行結果（回傳值）
    /// </summary>
    /// <typeparam name="TValue">回傳值型別</typeparam>
    public sealed class ServiceResult<TValue> : ServiceResult
    {
        /// <summary>
        /// 回傳值
        /// </summary>
        public TValue Value { get; set; }
    }
}