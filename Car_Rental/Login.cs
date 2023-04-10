using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Car_Rental
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username, upass;
            string query = "SELECT * FROM UserTb WHERE Uname='" + Uname.Text + "' AND Upass= '" + PassTb.Text + "'";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                username = Uname.Text;
                upass = PassTb.Text; 
                MainForm mainForm= new MainForm();
                mainForm.Show();
                this.Hide();
            }else
            {
                MessageBox.Show("Wrong Username or Password");
            }
            Con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Uname.Text = "";
            PassTb.Text = "";
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
