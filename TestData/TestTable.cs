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
                t_LanguageTable language= TableDock.GetTable<t_LanguageTable>(m_t_name);
                if (language != null)
                    return language.t_str;
                return "";
            }
        }
        public override void LoadData(byte[] data, ref int offset)
        {
            
        }
    }
}
