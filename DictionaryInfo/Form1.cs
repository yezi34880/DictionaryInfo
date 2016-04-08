using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DictionaryInfo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cboType.SelectedIndex = 0;
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            txtPath.Text = folderBrowserDialog1.SelectedPath.ToString();
        }

        private string getType(string type)
        {
            switch (type)
            {
                case "我的电脑":
                    return @"{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
                case "我的文档":
                    return @"{450D8FBA-AD25-11D0-98A8-0800361B1103}";
                case "控制面板":
                    return @"{21EC2020-3AEA-1069-A2DD-08002B30309D}";
                case "网络邻居":
                    return @"{208D2C60-3AEA-1069-A2D7-08002B30309D}";
                case "回收站":
                    return @"{645FF040-5081-101B-9F08-00AA002F954E}";
                default:
                    return @"{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            }
        }

        private void CamouFlage(string str)
        {
            try
            {
                StreamWriter sw = File.CreateText(txtPath.Text + @"\desktop.ini");
                sw.WriteLine(@"[.ShellClassInfo]");
                sw.WriteLine("CLSID=" + str);
                sw.Close();

                //设置desktop.ini为隐藏
                File.SetAttributes(txtPath.Text + @"\desktop.ini", FileAttributes.Hidden | FileAttributes.ReadOnly | FileAttributes.System | FileAttributes.Archive);
                File.SetAttributes(txtPath.Text, FileAttributes.System | FileAttributes.ReadOnly);
                MessageBox.Show("伪装成功！", "提示");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }


        }

        private void Restore()
        {
            try
            {
                FileInfo fi = new FileInfo(txtPath.Text + @"\desktop.ini");
                if (fi.Exists==false)
                {
                    MessageBox.Show("该文件夹没有被伪装！","提示");
                    return;
                }
                else
                {
                    System.Threading.Thread.Sleep(500);
                    File.SetAttributes(txtPath.Text + @"\desktop.ini", FileAttributes.Hidden);
                    fi.Delete();
                    File.SetAttributes(txtPath.Text, FileAttributes.System);
                    MessageBox.Show("还原成功！", "提示");
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtPath.Text.Trim() == "")
                return;
            if (cboType.Text!="")
            {
                string str = getType(cboType.Text);
                CamouFlage(str);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (txtPath.Text.Trim() == "")
                return;
            Restore();

        }



    }
}
