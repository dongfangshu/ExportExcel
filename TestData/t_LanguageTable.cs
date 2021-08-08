using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigTable
{
    class t_LanguageTable : BaseTable
    {
        public int t_id;
        public string t_str;
        public override void LoadData(byte[] data, ref int offset)
        {
            t_id = BytesBuffer.ReadIntBytes(data,ref offset);
            t_str = BytesBuffer.ReadStringBytes(data,ref offset);
        }
    }
}
