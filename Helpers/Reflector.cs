using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace XGENO.DBMapper
{
    internal static class Reflector
    {
        internal static List<DBField> GetObjectProperties<T>()
        {
            Type _objType = typeof(T);
            List<DBField> _dbFields = new List<DBField>();
            
            foreach (PropertyInfo _propInfo in _objType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                ColumnAttribute _columnValue = GetColumnAttribute<ColumnAttribute>(_propInfo);

                DBField _field = new DBField();

                _field.PropName = _propInfo.Name;
                _field.PropRuntimeType = _propInfo.PropertyType;
                _field.PropSetMethod = _propInfo.GetSetMethod();
                _field.ColumnName = (_columnValue == null ? _propInfo.Name : _columnValue.Name);

                _dbFields.Add(_field);
            }

            return _dbFields;
        }

        internal static T GetColumnAttribute<T>(PropertyInfo _propertyInfo) where T : class
        {
            T _customAttribute = default(T);

            object[] customAttributes = _propertyInfo.GetCustomAttributes(typeof(T), true);

            foreach (object attr in customAttributes)
            {
                _customAttribute = attr as T;
                if (_customAttribute != null)
                {
                    return _customAttribute;
                }
            }
            return default(T);
        }

        internal static void SetObjectValue(object _obj, MethodInfo _methodInfo, object _value, Type _valueType)
        {
            Type _nonNullableType = Nullable.GetUnderlyingType(_valueType) ?? _valueType;
            object[] _params = new object[1] { _value == DBNull.Value ? null : Convert.ChangeType(_value, _nonNullableType) };

            _methodInfo.Invoke(_obj, _params);
        }



    }
}
