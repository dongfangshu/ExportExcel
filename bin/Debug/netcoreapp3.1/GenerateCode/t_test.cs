using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Beans
{
    public class t_testBean
    {
        ///<summary>测试表格</summary>
        ///<summary></summary>

        public int t_id;

        public string t_text;

        public int t_value;

        private int m_t_int;
        public lang t_int
        {
            get
            {
                if (m_t_int == 0)
                    return "";
                t_testBean bean = Config.Bean<t_testBean, int>(m_t_int == 0);
                if (bean != null)
                    return bean.t_ddd;
            }
        }

        public void LoadData(byte[] data, ref int offset)
        {

            t_id = read.int;
            t_text = read.string;

            t_value = read.int;
            m_t_int = read.int;
        }

    }
}
