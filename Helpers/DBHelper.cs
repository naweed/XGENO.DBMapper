using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace XGENO.DBMapper
{
    internal static class DBHelper
    {
        public static SqlCommand GetCommand(SqlConnection _dbConn, string _sqlStmt, bool _isStoredProc, params SqlParameter [] _params)
        {
            SqlCommand _dbCommand = new SqlCommand();
            _dbCommand.CommandTimeout = 0;

            _dbCommand.Connection = _dbConn;


            _dbCommand.CommandType = _isStoredProc ? CommandType.StoredProcedure : CommandType.Text;
            _dbCommand.CommandText = _sqlStmt;

            if (_params != null && _params.Count() != 0)
            {
                foreach (SqlParameter _dbParam in _params)
                {
                    _dbParam.ParameterName = _dbParam.ParameterName.StartsWith("@") ? _dbParam.ParameterName : "@" + _dbParam.ParameterName;

                    _dbCommand.Parameters.Add(_dbParam);
                }
            }

            return _dbCommand;
        }

        public static DataSet GetDataSet(SqlConnection _dbConn, string _sqlStmt, bool _isStoredProc, params SqlParameter [] _params)
        {
            DataSet _dsData = new DataSet();

            SqlDataAdapter _dbAdapter;
            SqlCommand _dbCommand = GetCommand(_dbConn, _sqlStmt, _isStoredProc, _params);

            try
            {
                _dbAdapter = new SqlDataAdapter(_dbCommand);
                _dbAdapter.Fill(_dsData);
            }
            catch
            {
            }
            
            return _dsData;
        }
    }
}
