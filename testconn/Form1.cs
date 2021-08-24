using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace testconn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// 数据库链接字符串
        /// demo: string dbConn = "Data Source=127.0.0.1;Initial Catalog=OrBitXIScadaDB;Persist Security Info=True;User ID=sa;Password=123456";



        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestConn_Click(object sender, EventArgs e)
        {

            if (txtDataBase.Text == "" || txtIp.Text == "" || txtUserName.Text == "" || txtUserPwd.Text == "")
            {
                MessageBox.Show("请输入正确的信息！");
                return;
            }

            string dbConn = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";

            dbConn = string.Format(dbConn, txtIp.Text, txtDataBase.Text, txtUserName.Text, txtUserPwd.Text);

            SqlConnection m_SqlConn = new SqlConnection(dbConn);

            try
            {
                m_SqlConn.Open();

                if (m_SqlConn.State == ConnectionState.Open)
                {
                    MessageBox.Show("连接成功");
                }
                else
                {

                    MessageBox.Show("连接失败"+ "  "+ m_SqlConn.State.ToString());
                }    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="log"></param>
        public void LogError(string log)
        {
            string sFilePath = "D:\\Logs\\" + DateTime.Now.ToString("yyyyMM");
            string sFileName = "log_" + DateTime.Now.ToString("dd") + ".log";
            sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
            if (!Directory.Exists(sFilePath))//验证路径是否存在
            {
                Directory.CreateDirectory(sFilePath);
                //不存在则创建
            }
            FileStream fs;
            StreamWriter sw;
            if (File.Exists(sFileName))
            //验证文件是否存在，有则追加，无则创建
            {
                fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
            }
            sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "   ---   " + log);
            sw.Close();
            fs.Close();

        }

    }
}
