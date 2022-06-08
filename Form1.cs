using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            base.Load += OnApplicationLoad;
        }

        private void OnApplicationLoad(object sender, EventArgs e)
        {
            textBox1.Text = "";
            Logger.TextBox = textBox1;
            Setting.Init("Configs/Config.xml");
            var files = Directory.GetFiles(Setting.Instance.TablePath,"*.xlsx");
            checkedListBox1.Items.AddRange(files);
        }
        private void ClientAllClick(object sender, EventArgs e)
        {
            Logger.Log("客户端导出所有配置表");
            ExportHelper.ClearPath();
            ExportHelper.ExportAll();
        }

        private void ClientCheckClick(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex==-1)
            {
                Logger.Log("未选中");
                return;
            }
            var files = Directory.GetFiles(Setting.Instance.TablePath, "*.xlsx");
            var selectFile = files[checkedListBox1.SelectedIndex];
            ExportHelper.Export(new List<string>() { selectFile });
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //var table = TableDock.Instanced.GetTable<t_languageTable>(1);
            //var list = TableDock.Instanced.GetTableList<t_languageTable>();
            //for (int i = 0; i < list.Count; i++)
            //{
            //    Logger.Log($"ID:{list[i].t_id},Str:{list[i].t_str}");
            //}
            //Logger.Log(table.t_str);
        }
    }
}
