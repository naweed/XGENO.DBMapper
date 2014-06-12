using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XGENO.DBMapper
{
    [AttributeUsage(AttributeTargets.Property)]
    [Serializable]
    public sealed class ColumnAttribute : Attribute
    {
        public string Name { get; set; }

        public ColumnAttribute()
        {
        }

        public ColumnAttribute(string _columnName)
        {
            this.Name = _columnName;
        }
    }
}
