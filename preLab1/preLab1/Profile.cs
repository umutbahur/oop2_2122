using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace preLab1
{
    public partial class Profile : Form
    {
        public Profile()
        {
            InitializeComponent();
        }

        private void Profile_Load(object sender, EventArgs e)
        {

            SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\oop2_2122(1)\preLab1\preLab1\Database1.mdf;Integrated Security=True");
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("select * from Tablo where Username='" + Properties.Settings.Default.Username + "'", cn); // to check if there is already the same username
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblUsernameShow.Text = dr.GetValue(1).ToString();
                txtName.Text = dr.GetValue(3).ToString();
                txtPhoneNumber.Text = dr.GetValue(4).ToString();
                txtAddress.Text = dr.GetValue(5).ToString();
                txtCity.Text = dr.GetValue(6).ToString();
                txtCountry.Text = dr.GetValue(7).ToString();
                txtMail.Text = dr.GetValue(8).ToString();
            }

            cn.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\oop2_2122(1)\preLab1\preLab1\Database1.mdf;Integrated Security=True");
            cn.Open();

            string hashedData = ComputeSha256Hash(txtPassword.Text);

          
            SqlCommand cmd2 = new SqlCommand();

            cmd2 = new SqlCommand("select * from Tablo where Password='" + hashedData + "'", (SqlConnection)cn);

            SqlDataReader dr2;

            dr2 = cmd2.ExecuteReader();


            if (dr2.Read())// if exist
            {
                dr2.Close();

                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("update Tablo  set NameSurname='" + txtName.Text + "', PhoneNumber='" + txtPhoneNumber.Text + "', Address='" + txtAddress.Text + "', City='" + txtCity.Text +
                    "', Country='" + txtCountry.Text + "', Email='" + txtMail.Text + "' where Username='" + Properties.Settings.Default.Username + "'", cn);

                cmd.ExecuteNonQuery();

            }

            else
            {
                MessageBox.Show("Password is incorrect!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            new Game_Screen().Show();
            this.Hide();
        }
    }
}
