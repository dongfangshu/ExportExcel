using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigTable
{
    internal class TableAttribute:Attribute
    {
        public Type table;
        public TableAttribute(Type tableType)
        {
            table = tableType;
        }
    }
}
