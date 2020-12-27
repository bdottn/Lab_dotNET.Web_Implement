namespace Service.Model
{
    /// <summary>
    /// Service 執行結果類型
    /// </summary>
    public enum ServiceResultType
    {
        /// <summary>
        /// 成功
        /// <para>一切順利完成</para>
        /// </summary>
        Success,

        /// <summary>
        /// 警告
        /// <para>有點小問題，但不影響作業進行</para>
        /// </summary>
        Warning,

        /// <summary>
        /// 失敗
        /// <para>遺漏特定資訊，無法繼續作業</para>
        /// </summary>
        Fail,

        /// <summary>
        /// 例外
        /// <para>麻煩很大，碰到未預期的狀況</para>
        /// </summary>
        Exception,
    }
}