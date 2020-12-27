using Service.Model;
using System;
using System.Net;

namespace WebAPI_Implement.Extension
{
    /// <summary>
    /// 型別轉換（擴充類別）
    /// </summary>
    static class TypeConversion
    {
        /// <summary>
        /// 轉換針對 HTTP 所定義的狀態碼值
        /// </summary>
        /// <param name="resultType">執行結果類型</param>
        /// <returns>針對 HTTP 所定義的狀態碼值</returns>
        internal static HttpStatusCode ToHttpStatusCode(this ServiceResultType resultType)
        {
            if (resultType == ServiceResultType.Fail)
            {
                return HttpStatusCode.Forbidden;
            }
            else if (resultType == ServiceResultType.Exception)
            {
                return HttpStatusCode.Conflict;
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}