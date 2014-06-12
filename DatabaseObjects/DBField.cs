using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace XGENO.DBMapper
{
    internal class DBField
    {
        public string PropName { get; set; }
        internal Type PropRuntimeType { get; set; }

        public string ColumnName { get; set; }

        internal MethodInfo PropSetMethod { get; set; }

        public bool IsMapped { get; set; }

        public DBField()
        {
            IsMapped = false;
        }

    }
}
