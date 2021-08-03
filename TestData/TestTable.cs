using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigTable
{
    /// <summary>
    /// 测试表
    /// </summary>
    class TestTable : BaseTable
    {
        /// <summary>
        /// 字段描述
        /// </summary>
        public int t_value;
        private int m_t_name;
        public string t_name 
        {
            get {
                if (m_t_name == 0)
                    return "";
                LanguageTable language= TableDock.GetTable<LanguageTable, int>(m_t_name);
                if (language != null)
                    return language.t_txt;
                return "";
            }
        }
        public override void LoadData(byte[] data, ref int offset)
        {
            
        }
    }
}
