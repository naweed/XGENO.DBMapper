using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace XGENO.DBMapper
{
    public class MultiReader : IDisposable
    {
        private int _currTableCount;
        private DataSet _dsData;

        internal MultiReader(DataSet _fullDataSet)
        {
            _dsData = _fullDataSet;
            _currTableCount = 0;
        }

        public List<T> Read<T>()
        {
            List<T> _lstObjects = new List<T>();

            _currTableCount++;

            DataTable _dtTable = _dsData.Tables[_currTableCount-1];

            //Get Properties List
            var _dbFields = Reflector.GetObjectProperties<T>();

            //Map to DB Output
            foreach (DBField _field in _dbFields)
            {
                if (_dtTable.Columns.Contains(_field.ColumnName))
                {
                    _field.IsMapped = true;
                }
            }



            foreach (DataRow _dRow in _dtTable.Rows)
            {
                T _obj = System.Activator.CreateInstance<T>();

                //Set each Parameter value
                foreach (DBField _field in _dbFields.Where(_f => _f.IsMapped == true))
                {
                    Reflector.SetObjectValue(_obj, _field.PropSetMethod, _dRow[_field.ColumnName], _field.PropRuntimeType);
                }

                _lstObjects.Add(_obj);
            }

            return _lstObjects;
        }

        public void Dispose()
        {            
        }
    }
}
