using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigTable
{
   public class t_testTable: BaseTable
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
                 t_languageTable languageTable=TableDock.GetTable<t_languageTable>(m_t_int);
                 if(languageTable!=null)
				 {
					return languageTable.t_str;
				 }
				 return "";
				}
			}

        public override void LoadData(byte[] data, ref int offset)
        {
         
             t_id=BytesBuffer.ReadIntBytes(data,ref offset);
             t_text=BytesBuffer.ReadStringBytes(data,ref offset);
          
             t_value=BytesBuffer.ReadIntBytes(data,ref offset);
             m_t_int=BytesBuffer.ReadIntBytes(data,ref offset);
        }
        
    }
}
