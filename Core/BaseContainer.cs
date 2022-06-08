using System.Collections.Generic;
using System.Text;

namespace ConfigTable
{
    internal abstract class BaseContainer
    {
        public bool IsLoad=false;
        internal abstract T GetData<T>(int id) where T : BaseTable;
        internal abstract void LoadData(byte[] data);
        internal abstract List<T> GetList<T>() where T : BaseTable;
        internal abstract Dictionary<int, TTable> GetDictionary<TTable>() where TTable : BaseTable;
    }
}
