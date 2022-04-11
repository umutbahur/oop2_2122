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
using System.Security.Cryptography;

namespace preLab1
{
    

    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

        }

    



        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\oop2_2122(1)\preLab1\preLab1\Database1.mdf;Integrated Security=True");
            cn.Open();

            string hashedData = ComputeSha256Hash(txtPassword.Text);
            SqlCommand cmd = new SqlCommand();

            cmd = new SqlCommand("select * from Tablo where Username='" + txtUsername.Text + "' AND Password='" + hashedData + "'", (SqlConnection)cn);

            SqlDataReader dr;

            dr = cmd.ExecuteReader();
            

            if (dr.Read())// if exist
            {
                dr.Close();
              

                new Game_Screen().Show();
                this.Hide();
                Properties.Settings.Default.Username = txtUsername.Text;
                Properties.Settings.Default.Password = txtPassword.Text;
                SaveSettings();
                
            }
            else
            {
                MessageBox.Show("The Username or Password you entered is incorrect, try again");
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();
            }

          
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtUsername;
            this.AcceptButton = button1;
            Settings settings = new Settings();
            settings.TopMost = true;
            getSettings();
        }

        private void settingsMenu_Click(object sender, EventArgs e)
        {
            new Settings().Show();
            this.Hide();
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void chk_ShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_ShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }

        public void getSettings()
        {
            txtUsername.Text = Properties.Settings.Default.Username;
            txtPassword.Text = Properties.Settings.Default.Password;

            Properties.Settings.Default.Save();
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Register().Show();
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
    
