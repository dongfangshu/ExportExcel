using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigTable
{
   public class t_item: BaseTable
    {
        ///<summary>表格2</summary>
        ///<summary>道具表</summary>
        
             ///<summary>道具ID</summary>
             public int t_Id;
        
             ///<summary>道具图标</summary>
             public string t_icon;
        

        public override void LoadData(byte[] data, ref int offset)
        {
         
             t_Id=BytesBuffer.ReadIntBytes(data,ref offset);
             t_icon=BytesBuffer.ReadStringBytes(data,ref offset);
        }
        
    }
}
