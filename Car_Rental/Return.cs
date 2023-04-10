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
    public partial class Return : Form
    {
        public Return()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString);
        private void populate()
        {
            Con.Open();
            string query = "SELECT * FROM Rental";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void populateRet()
        {
            Con.Open();
            string query = "SELECT * FROM ReturnC";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ReturnDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void  Deleteonreturn()
        {
            int rentId;
            rentId = Convert.ToInt32(RentDGV.SelectedRows[0].Cells[0].Value.ToString());
            Con.Open();
            string query = "DELETE FROM Rental WHERE RentId=" + rentId + ";";
            SqlCommand cmd = new SqlCommand(query, Con);

            cmd.ExecuteNonQuery();

            //MessageBox.Show("Rental Deleted Successfully");
            Con.Close();
            //UpdateonRentDelete();
            populate();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void Return_Load(object sender, EventArgs e)
        {
            populate();
            populateRet();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        private void RentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CarIdTb.Text= RentDGV.SelectedRows[0].Cells[1].Value.ToString();
            CustNameTb.Text = RentDGV.SelectedRows[0].Cells[2].Value.ToString();
            ReturnDate.Text = RentDGV.SelectedRows[0].Cells[4].Value.ToString();
            DateTime d1 = ReturnDate.Value.Date;
            DateTime d2 = DateTime.Now; 
            TimeSpan t = d2 - d1; 
            int NrOfDays = Convert.ToInt32(t.TotalDays);
            if(NrOfDays <= 0 )
            {
                DelayTb.Text = "No Delay";
                FineTb.Text = "0";
            }else
            {
                DelayTb.Text = "" + NrOfDays;
                FineTb.Text = "" + (NrOfDays * 250);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IdTb.Text == "" || CustNameTb.Text == "" || FineTb.Text == "" || DelayTb.Text =="")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "INSERT INTO ReturnC VALUES(" + IdTb.Text + ", '" + CarIdTb.Text + "', '" + CustNameTb.Text + "','" + ReturnDate.Value.ToString("yyyy-MM-dd") + "', '" + DelayTb.Text + "','" + FineTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Car Returned");
                    Con.Close();
                    //UpdateonRent();
                    populateRet();
                    Deleteonreturn();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void ReturnDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
