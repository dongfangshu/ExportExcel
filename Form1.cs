using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConfigTable;
using ExportExcel.Helper;

namespace ExportExcel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            base.Load += OnApplicationLoad; ;
        }

        private void OnApplicationLoad(object sender, EventArgs e)
        {
            Logger.TextBox = textBox1;
            Setting.Init("Configs/Config.xml");
            
        }
        private void ClientAllClick(object sender, EventArgs e)
        {
            Logger.Log("客户端导出所有配置表");
            ExportHelper.ExportAll();
        }

        private void ClientCheckClick(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            TableDock.Init();
            t_LanguageTable languageTable= TableDock.GetTable<t_LanguageTable>(1);
            t_LanguageTable languageTable1= TableDock.GetTable<t_LanguageTable>(2);
            t_LanguageTable languageTable2= TableDock.GetTable<t_LanguageTable>(3);
            t_LanguageTable languageTable3= TableDock.GetTable<t_LanguageTable>(4);
            t_LanguageTable languageTable4= TableDock.GetTable<t_LanguageTable>(5);
            t_LanguageTable languageTable5= TableDock.GetTable<t_LanguageTable>(6);
        }
    }
}
