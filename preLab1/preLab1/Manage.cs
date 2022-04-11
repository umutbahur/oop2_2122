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
    public partial class Manage : Form
    {
        public Manage()
        {
            InitializeComponent();
        }

        private void Manage_Load(object sender, EventArgs e)
        {
           
            SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\oop2_2122(1)\preLab1\preLab1\Database1.mdf;Integrated Security=True");
            cn.Open();

            SqlCommand cmd2 = new SqlCommand();
            cmd2 = new SqlCommand("select * from Tablo where IsAdmin='1' AND Username='" + Properties.Settings.Default.Username + "'", (SqlConnection)cn);
            SqlDataReader dr2;
            dr2 = cmd2.ExecuteReader();
            if (!dr2.Read())// if exist
            {
                dr2.Close();
                MessageBox.Show("You Are Not Admin! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                new Game_Screen().Show();
                this.Close();
            }



            

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            new Game_Screen().Show();
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\oop2_2122(1)\preLab1\preLab1\Database1.mdf;Integrated Security=True");
            cn.Open();

       
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("update Tablo  set NameSurname='" + txtName.Text + "', PhoneNumber='" + txtPhoneNumber.Text + "', Address='" + txtAddress.Text + "', City='" + txtCity.Text +
                    "', Country='" + txtCountry.Text + "', Email='" + txtMail.Text + "' where Username='" + Properties.Settings.Default.Username + "'", cn);

                cmd.ExecuteNonQuery();

            ClearText();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != string.Empty && txtUsername.Text != string.Empty && txtName.Text != string.Empty && txtPhoneNumber.Text != string.Empty && txtAddress.Text != string.Empty && txtCity.Text != string.Empty && txtCountry.Text != string.Empty && txtMail.Text != string.Empty)
            {
                SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\oop2_2122(1)\preLab1\preLab1\Database1.mdf;Integrated Security=True");
                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("select * from Tablo where Username='" + txtUsername.Text + "'", cn); // to check if there is already the same username
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.Read())// if exist
                {
                    dr.Close();
                    MessageBox.Show("Username Already exist please try another ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else // if not insert into Table
                {
                    string hashedData = ComputeSha256Hash(txtPassword.Text);
                    dr.Close();
                    cmd = new SqlCommand("insert into Tablo values(@Username,@Password,@NameSurname,@PhoneNumber,@Address,@City,@Country,@Email,@IsAdmin)", cn);
                    cmd.Parameters.AddWithValue("Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("Password", hashedData);
                    cmd.Parameters.AddWithValue("NameSurname", txtName.Text);
                    cmd.Parameters.AddWithValue("PhoneNumber", txtPhoneNumber.Text);
                    cmd.Parameters.AddWithValue("Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("City", txtCity.Text);
                    cmd.Parameters.AddWithValue("Country", txtCountry.Text);
                    cmd.Parameters.AddWithValue("Email", txtMail.Text);
                    cmd.Parameters.AddWithValue("IsAdmin", 0);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("The new user is added", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\oop2_2122(1)\preLab1\preLab1\Database1.mdf;Integrated Security=True");
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("select * from Tablo where Username='" + txtUsername.Text + "'", cn); // to check if there is already the same username
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
               
                txtName.Text = dr.GetValue(3).ToString();
                txtPhoneNumber.Text = dr.GetValue(4).ToString();
                txtAddress.Text = dr.GetValue(5).ToString();
                txtCity.Text = dr.GetValue(6).ToString();
                txtCountry.Text = dr.GetValue(7).ToString();
                txtMail.Text = dr.GetValue(8).ToString();

                dr.Close();
            }

            else
            {
                MessageBox.Show("User not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cn.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\oop2_2122(1)\preLab1\preLab1\Database1.mdf;Integrated Security=True");
            cn.Open();


            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("delete from Tablo where Username='" + txtUsername.Text + "'", cn);

            cmd.ExecuteNonQuery();

            MessageBox.Show("User is deleted!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ClearText();
           
        }

        public void ClearText()
        {
            txtUsername.Clear();
            txtPhoneNumber.Clear();
            txtPassword.Clear();
            txtName.Clear();
            txtMail.Clear();
            txtCountry.Clear();
            txtCity.Clear();
            txtAddress.Clear();
            txtList.Clear();

        }

        private void btnList_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\oop2_2122(1)\preLab1\preLab1\Database1.mdf;Integrated Security=True");
            cn.Open();


            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("select Username from Tablo ", cn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            StringBuilder tmp = new StringBuilder();


            while (dr.Read()) 
            {

                tmp.AppendLine(dr.GetValue(0).ToString());

            }

            txtList.Text = tmp.ToString();
        }
    }
}
