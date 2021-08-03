using ConfigTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigTable
{
    public class TableDock
    {
        private static Dictionary<Type, BaseContainer> TableMap = new Dictionary<Type, BaseContainer>();
        public static T GetTable<T, K>(K key) where T:BaseTable
        {
            Type tableType= typeof(T);
            BaseContainer container= TableMap[tableType];
            return container.GetData<T,K>(key);
        }
    }
}
