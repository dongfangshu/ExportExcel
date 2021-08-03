using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigTable
{
   public abstract class BaseTable
    {
      public abstract void LoadData(byte[] data,ref int offset);
    }
}
