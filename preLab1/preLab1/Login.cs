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
        }

        private void settingsMenu_Click(object sender, EventArgs e)
        {
            new Settings().Show();
            this.Hide();
        }
    }
}
    
