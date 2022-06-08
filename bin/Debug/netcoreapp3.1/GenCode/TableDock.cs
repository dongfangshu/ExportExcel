using ConfigTable;
using ExportExcel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConfigTable
{
    public  class TableDock
    {
        private static TableDock ins;
        public static TableDock Instanced{get{ if (ins == null) { ins = new TableDock(); } return ins;}}
        private static Dictionary<Type, BaseContainer> TableMap = new Dictionary<Type, BaseContainer>();
        public TableDock()
        {
            
             TableMap.Add(typeof(t_test),new t_testContainer());
            
             TableMap.Add(typeof(t_languageTable),new t_languageTableContainer());
            
             TableMap.Add(typeof(t_item),new t_itemContainer());
            

        }

        internal T GetTable<T>(int m_t_int) where T : BaseTable
        {
            Type t = typeof(T);
            TableMap.TryGetValue(t,out BaseContainer container);
            if (!container.IsLoad)
            {
                var data = File.ReadAllBytes(Setting.Instance.ClientBytesPath+"/"+t.Name+ ".bytes");
                container.LoadData(data);
                container.IsLoad = true;
            }
            var table = container.GetData<T>(m_t_int);
            return table;
        }

        internal List<T> GetTableList<T>() where T : BaseTable
        {
            Type t = typeof(T);
            TableMap.TryGetValue(t, out BaseContainer container);
            if (!container.IsLoad)
            {
                var data = File.ReadAllBytes(Setting.Instance.ClientBytesPath + "/" + t.Name + ".bytes");
                container.LoadData(data);
                container.IsLoad = true;
            }
            var table = container.GetList<T>();
            return table;
        }
        internal Dictionary<int,T> GetTableDictionary<T>() where T : BaseTable
        {
            Type t = typeof(T);
            TableMap.TryGetValue(t, out BaseContainer container);
            if (!container.IsLoad)
            {
                var data = File.ReadAllBytes(Setting.Instance.ClientBytesPath + "/" + t.Name + ".bytes");
                container.LoadData(data);
                container.IsLoad = true;
            }
            var table = container.GetDictionary<T>();
            return table;
        }
    }
}