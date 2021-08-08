using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Table
{
   public class t_languageTable
    {
        ///<summary>测试表格</summary>
        ///<summary></summary>
        
             public int t_id;
        
             public string t_str;
        

        public void LoadData(byte[] data, ref int offset)
        {
         
             t_id=BytesBuffer.ReadIntBytes(data,offset);
             t_str=BytesBuffer.ReadStringBytes(data,offset);
          
        }
        
    }
}
