using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace XGENO.DBMapper
{
    public static class ConnectionExtentions
    {
        public static List<T> Query<T>(this SqlConnection _dbConn, string _sqlString, params SqlParameter [] _params)
        {
            List<T> _lstObjects;

            using (MultiReader _multiReader = _dbConn.MultiQuery(_sqlString, _params))
            {
                _lstObjects = _multiReader.Read<T>();
            }

            return _lstObjects;
        }

        public static MultiReader MultiQuery(this SqlConnection _dbConn, string _sqlString, params SqlParameter [] _params)
        {
            MultiReader _multiReader = new MultiReader(DBHelper.GetDataSet(_dbConn, _sqlString, false, _params));

            return _multiReader;
        }

        public static List<T> SPQuery<T>(this SqlConnection _dbConn, string _sqlString, params SqlParameter [] _params)
        {
            List<T> _lstObjects;

            using (MultiReader _multiReader = _dbConn.SPMultiQuery(_sqlString, _params))
            {
                _lstObjects = _multiReader.Read<T>();
            }

            return _lstObjects;
        }

        public static MultiReader SPMultiQuery(this SqlConnection _dbConn, string _sqlString, params SqlParameter [] _params)
        {
            MultiReader _multiReader = new MultiReader(DBHelper.GetDataSet(_dbConn, _sqlString, true, _params));

            return _multiReader;
        }

        public static void SPNonQuery(this SqlConnection _dbConn, string _sqlString, params SqlParameter[] _params)
        {
            using (MultiReader _multiReader = _dbConn.SPMultiQuery(_sqlString, _params))
            {
            }
        }
       
    }
}
