using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace ConfigTable
{
    [TableAttribute(typeof(t_languageTable))]
    internal class t_languageTableContainer: BaseContainer
    {
        private string TableFile = typeoft_languageTable.Name;
        List<t_languageTable> TableList = new List<t_languageTable>();
        Dictionary<int, t_languageTable> TableMap = new Dictionary<int, t_languageTable>();
        internal override T GetData<T>(int id)
        {
            if (TableMap.ContainsKey(id))
                return TableMap[id] as T; 
            Console.WriteLine($"{TableFile} is don't containsKey{id}");
            return null;
        }
        internal override void LoadData(byte[] data)
        {
            int OffSize = 0;
#if EDITOR
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
#endif
            while (OffSize<data.Length)
            {
                t_languageTable table = new t_languageTable();
                table.LoadData(data,ref OffSize);
                TableMap.Add(table.t_id,table);
                TableList.Add(table);
            }
            //IsLoad = true;
#if EDITOR
            stopwatch.Stop();
            Console.WriteLine($"初始化{TableFile}耗时{stopwatch.ElapsedMilliseconds}");
#endif
    }
}
