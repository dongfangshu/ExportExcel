using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigTable
{
    internal  class BaseContainer
    {
        internal virtual T GetData<T>(int id) where T:BaseTable { return null; }
        internal virtual void LoadData(byte[] data) { }
    }
}
