using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


    public class Logger
    {
        
        public static System.Windows.Forms.TextBox TextBox;
        public static void Log(string txt)
        {
            if (!string.IsNullOrEmpty(txt))
            {
                TextBox.AppendText(txt+"\n");
            }
        }
        public static void Err(string txt)
        {
            DialogResult result = MessageBox.Show(txt, "错误", MessageBoxButtons.OK);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
            //
        }
    }

