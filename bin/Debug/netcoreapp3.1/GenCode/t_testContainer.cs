using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace ConfigTable
{
    internal class t_testContainer: BaseContainer
    {
        private string TableFile = typeof(t_test).Name;
        List<t_test> TableList = new List<t_test>();
        Dictionary<int, t_test> TableMap = new Dictionary<int, t_test>();
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
#if UNITY_EDITOR
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
#endif
            while (OffSize<data.Length)
            {
                t_test table = new t_test();
                table.LoadData(data,ref OffSize);
                TableMap.Add(table.t_id,table);
                TableList.Add(table);
            }
            //IsLoad = true;
#if UNITY_EDITOR
            stopwatch.Stop();
            Console.WriteLine($"初始化{TableFile}耗时{stopwatch.ElapsedMilliseconds}");
#endif
        }

        internal override Dictionary<int, TTable> GetDictionary<TTable>()
        {
            return TableMap as Dictionary<int, TTable>;
        }

        internal override List<T> GetList<T>()
        {
            return TableList as List<T>;
        }
    }
}
