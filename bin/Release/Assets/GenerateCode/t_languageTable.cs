using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigTable
{
   public class t_languageTable: BaseTable
    {
        ///<summary>测试表格</summary>
        ///<summary></summary>
        
             public int t_id;
        
             public string t_str;
        

        public override void LoadData(byte[] data, ref int offset)
        {
         
             t_id=BytesBuffer.ReadIntBytes(data,ref offset);
             t_str=BytesBuffer.ReadStringBytes(data,ref offset);
          
        }
        
    }
}
