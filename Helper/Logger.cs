using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


public class Logger
{

    public static System.Windows.Forms.TextBox TextBox;
    static StringBuilder sb = new StringBuilder();
    public static void Log(string txt)
    {
        if (!string.IsNullOrEmpty(txt))
        {
            sb.AppendLine(txt);
            //TextBox.AppendText(txt + "\r\n");
            TextBox.Text = sb.ToString();
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

