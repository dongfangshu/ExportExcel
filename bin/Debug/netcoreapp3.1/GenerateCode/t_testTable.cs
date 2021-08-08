using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Table
{
   public class t_testTable
    {
        ///<summary>测试表格</summary>
        ///<summary></summary>
        
             public int t_id;
        
             public string t_text;
        
             public int t_value;
        
            private int m_t_int;
            public string t_int
            {
                get{
                if(m_t_int==0)
                   return "";
                 t_languageTable languageTable=Config.Bean<t_languageTable,int>(m_t_int);
                 if(bean!=null)
                    return languageTable.t_str;
                    }
             }

        public void LoadData(byte[] data, ref int offset)
        {
         
             t_id=BytesBuffer.ReadIntBytes(data,offset);
             t_text=BytesBuffer.ReadStringBytes(data,offset);
          
             t_value=BytesBuffer.ReadIntBytes(data,offset);
             m_t_int=BytesBuffer.ReadIntBytes(data,offset);
        }
        
    }
}
