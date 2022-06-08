using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigTable
{
   public class t_languageTable: BaseTable
    {
        ///<summary>测试表格</summary>
        ///<summary>语言包</summary>
        
             ///<summary>语言包ID</summary>
             public int t_id;
        
             ///<summary>文本</summary>
             public string t_str;
        
             ///<summary></summary>
             public long t_uid;
        

        public override void LoadData(byte[] data, ref int offset)
        {
         
             t_id=BytesBuffer.ReadIntBytes(data,ref offset);
             t_str=BytesBuffer.ReadStringBytes(data,ref offset);
             t_uid=BytesBuffer.ReadLongBytes(data,ref offset);
          
        }
        
    }
}
