using Operation.Model;

namespace EntityOperation.Protocol
{
    /// <summary>
    /// 設定檔介面
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// MSSQL 連線字串
        /// </summary>
        string MSSQLConnectionString { get; }

        /// <summary>
        /// 可接受的系統介接端口憑證
        /// </summary>
        /// <returns>WebAPI 憑證</returns>
        WebAPICredential AcceptedCredential { get; }
    }
}