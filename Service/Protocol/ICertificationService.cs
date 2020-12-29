using Service.Model;

namespace Service.Protocol
{
    /// <summary>
    /// 憑證服務介面
    /// </summary>
    public interface ICertificationService
    {
        /// <summary>
        /// 認證
        /// </summary>
        /// <param name="credential">憑證</param>
        /// <returns>
        /// <para>認證成功：【ResultType】Success</para>
        /// <para>憑證失敗：【ResultType】Fail；【Message】失敗訊息</para>
        /// <para>發生例外：【ResultType】Exception；【Message】例外訊息</para>
        /// </returns>
        ServiceResult Authenticate(string credential);
    }
}