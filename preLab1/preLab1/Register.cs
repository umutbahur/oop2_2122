using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace preLab1
{
    public partial class Register : Form
    {

        public object cn;

        public Register()
        {
            InitializeComponent();

        }


        private void btnRegister_Click(object sender, EventArgs e)
        {

            if (txtPassword.Text != string.Empty && txtUsername.Text != string.Empty && txtName.Text != string.Empty && txtPhone.Text != string.Empty && txtAddress.Text != string.Empty && txtCity.Text != string.Empty && txtCountry.Text != string.Empty && txtEmail.Text != string.Empty)
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
                    cmd.Parameters.AddWithValue("PhoneNumber", txtPhone.Text);
                    cmd.Parameters.AddWithValue("Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("City", txtCity.Text);
                    cmd.Parameters.AddWithValue("Country", txtCountry.Text);
                    cmd.Parameters.AddWithValue("Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("IsAdmin", 0);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Your Account is created . Please login now.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               
            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {

            SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\oop2_2122(1)\preLab1\preLab1\Database1.mdf;Integrated Security=True");
            cn.Open();


        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Hide();
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

    }
}
