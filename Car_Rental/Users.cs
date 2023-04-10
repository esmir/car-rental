using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Car_Rental
{
    
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }
        
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);
        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
      
        private void populate()
        {
            Con.Open();
            string query = "SELECT * FROM UserTb";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(Uid.Text == "" || Uname.Text == "" || Upass.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "INSERT INTO UserTb VALUES("+ Uid.Text+", '"+ Uname.Text+"', '"+ Upass.Text+"')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    
                    cmd.ExecuteNonQuery();
                    
                    MessageBox.Show("User Successfully Added");
                    Uid.Text = ""; Uname.Text = ""; Upass.Text = "";
                    Con.Close();
                    populate();

                } catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }
        private void Users_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Uid.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "DELETE FROM UserTb WHERE Id=" + Uid.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("User Deleted Successfully");
                    Uid.Text = ""; Uname.Text = ""; Upass.Text = "";
                    Con.Close();
                    populate();

                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Uid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            Uname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            Upass.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Uid.Text == "" || Uname.Text == "" || Upass.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "UPDATE UserTb SET Uname = '"+Uname.Text+"', Upass='"+Upass.Text+"' WHERE Id= "+Uid.Text+";";
                    SqlCommand cmd = new SqlCommand(query, Con);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("User Successfully Updated");
                    Uid.Text = ""; Uname.Text = ""; Upass.Text = "";
                    Con.Close();
                    populate();

                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }
    }
}
