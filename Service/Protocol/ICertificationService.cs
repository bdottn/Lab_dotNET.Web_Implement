using Service.Model;

namespace Service.Protocol
{
    /// <summary>
    /// 認證服務介面
    /// </summary>
    public interface ICertificationService
    {
        /// <summary>
        /// 驗證憑證字串
        /// </summary>
        /// <param name="credential">憑證字串</param>
        ServiceResult Authenticate(string credential);
    }
}