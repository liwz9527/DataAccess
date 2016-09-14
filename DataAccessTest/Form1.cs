using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Vic.Data;
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

                List<UserInfo> users = new List<UserInfo>();
                UserInfo u;
                u = new UserInfo();
                u.Id = 1;
                u.Name = "A";
                u.Sex = 1;
                u.Email = "a.mail";
                users.Add(u);

                u = new UserInfo();
                u.Id = 2;
                u.Name = "B";
                u.Sex = 1;
                u.Email = "b.mail";
                users.Add(u);

                u = new UserInfo();
                u.Id = 3;
                u.Name = "C";
                u.Sex = 1;
                u.Email = "c.mail";
                users.Add(u);

                dataAccess.Add(users);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }        
    }

    [Table("userinfo1")]
    class UserInfo
    {
        [Identity]
        public int Id { get; set; }

        [Field("xb")]
        public int Sex { get; set; }

        [Primarykey]
        [Field("xm")]
        public string Name { get; set; }

        [Field("yx")]
        public string Email { get; set; }
    }
}
