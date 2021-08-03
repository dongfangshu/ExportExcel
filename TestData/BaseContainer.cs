using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigTable
{
   public abstract class BaseContainer
    {

        public abstract T GetData<T,K>(K key) where T:BaseTable;
    }
}
