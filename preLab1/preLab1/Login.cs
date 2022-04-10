using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            if ((txtUsername.Text == "admin" && txtPassword.Text == "admin") || (txtUsername.Text == "user" && txtPassword.Text == "user"))
            {
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
    }
}
    
