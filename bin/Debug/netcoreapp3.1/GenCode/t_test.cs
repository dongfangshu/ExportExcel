using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigTable
{
   public class t_test: BaseTable
    {
        ///<summary>测试表格</summary>
        ///<summary>测试表格</summary>
        
             ///<summary>表ID</summary>
             public int t_id;
        
             ///<summary>字段说明</summary>
             public string t_text;
        
             ///<summary>数值</summary>
             public int t_value;
        
            private int m_t_int;
            ///<summary>桥接</summary>
            public string t_int
            {
                get{
                if(m_t_int==0)
                   return "";
                 t_languageTable languageTable=TableDock.Instanced.GetTable<t_languageTable>(m_t_int);
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
