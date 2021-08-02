using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    }
}
