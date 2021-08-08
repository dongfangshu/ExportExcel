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
        private static TableDock dock;
        private static Dictionary<Type, BaseContainer> TableMap = new Dictionary<Type, BaseContainer>();
        public static void Init()
        {
            dock = new TableDock();
        }
        public TableDock()
        {
            Type[] AllTypes= this.GetType().Assembly.GetTypes();
            string basePath = System.Environment.CurrentDirectory;
            foreach (var Type in AllTypes)
            {
                if (Type.BaseType != typeof(BaseContainer))
                    continue;
                var atts= Type.GetCustomAttributes(typeof(TableAttribute),false);
                TableAttribute att= atts[0] as TableAttribute;
                Type tableType = att.table;

                byte[] data= File.ReadAllBytes(Setting.Instance.ClientBytesPath+"/"+tableType.Name+".bytes");
                BaseContainer container= Activator.CreateInstance(Type) as BaseContainer;
                // = new BaseContainer();
                container.LoadData(data);
                TableMap.Add(tableType,container);
            }
        }
        public static T GetTable<T>(int id) where T:BaseTable
        {
            Type tableType= typeof(T);
            BaseContainer container= TableMap[tableType];
            return container.GetData<T>(id);
        }
    }
}
