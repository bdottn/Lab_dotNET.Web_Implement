using System;
using System.ComponentModel.DataAnnotations;

namespace Operation.Model.CustomerAttribute
{
    /// <summary>
    /// MSSQL 時間驗證屬性
    /// </summary>
    sealed class MSSQLDateTimeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var dateTime = Convert.ToDateTime(value);

            if (dateTime < new DateTime(1753, 1, 1))
            {
                // MSSQL 時間欄位最小只能存入 1753-01-01
                return false;
            }

            return true;
        }
    }
}