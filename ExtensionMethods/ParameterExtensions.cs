using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace XGENO.DBMapper
{
    public static class ParameterExtentions
    {
        public static SqlParameter CreateSqlParam(this string _paramName, object _paramValue)
        {
            SqlParameter _sqlParam = new SqlParameter();

            _sqlParam.ParameterName = _paramName.StartsWith("@") ? _paramName : "@" + _paramName;
            _sqlParam.IsNullable = true;
            _sqlParam.Value = (_paramValue == null ? DBNull.Value : _paramValue);

            return _sqlParam;
        }

    }
}
