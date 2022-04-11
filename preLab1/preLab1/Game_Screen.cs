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

namespace preLab1
{
    public partial class Game_Screen : Form
    {
        public Game_Screen()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void sETTINGSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Settings().Show();
        }

        private void Game_Screen_Load(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.TopMost = true;
        }

        private void StripManage_Click(object sender, EventArgs e)
        {
            new Manage().Show();
            this.Hide();
        }

        private void pROFILEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Profile().Show();
            this.Hide();
        }
    }

}
