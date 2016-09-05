using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
//using Oracle.DataAccess.Client;

namespace DataAccessTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable factoryDt = System.Data.Common.DbProviderFactories.GetFactoryClasses();
            this.dataGridView2.DataSource = factoryDt;
            this.txtConn.Text = "sqlite";
            this.txtSQL.Text = "select * from news";

        }

        private void btnExecSql_Click(object sender, EventArgs e)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings[this.txtConn.Text].ConnectionString;
                string prname = ConfigurationManager.ConnectionStrings[this.txtConn.Text].ProviderName;
                Vic.Data.DataAccess dataAccess = new Vic.Data.DataAccess(conn, prname);
                DataTable dt = dataAccess.QueryTable(this.txtSQL.Text);
                this.dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }        
    }
}
