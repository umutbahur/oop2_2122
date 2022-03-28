using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace preLab1
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }



        private void LoginMenuB_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Login().Show();
        }

        public void getSettings()
        {
            radioButton1.Checked = Properties.Settings.Default.D_Easy;
            radioButton2.Checked = Properties.Settings.Default.D_Normal;
            radioButton3.Checked = Properties.Settings.Default.D_Hard;
            radioButton4.Checked = Properties.Settings.Default.D_Custom;
            checkBox1.Checked = Properties.Settings.Default.S_Triangle;
            checkBox2.Checked = Properties.Settings.Default.S_Round;
            checkBox3.Checked = Properties.Settings.Default.S_Square;
            textBox1.Text = Properties.Settings.Default.Custom_X.ToString();
            textBox2.Text = Properties.Settings.Default.Custom_Y.ToString();

            Properties.Settings.Default.Save();
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }

        public void TextBoxCheck()
        {
            textBox1.Visible = radioButton4.Checked;
            textBox2.Visible = radioButton4.Checked;
            label2.Visible = radioButton4.Checked;
            label4.Visible = radioButton4.Checked;
            label5.Visible = radioButton4.Checked;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SaveSettings();
            getSettings();
            new Login().Show();
            this.Hide();
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.D_Easy = radioButton1.Checked;
            TextBoxCheck();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.D_Normal = radioButton2.Checked;
            TextBoxCheck();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.D_Hard = radioButton3.Checked;
            TextBoxCheck();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.D_Custom = radioButton4.Checked;
            TextBoxCheck();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.S_Triangle = checkBox1.Checked;
            TextBoxCheck();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.S_Round = checkBox2.Checked;
            TextBoxCheck();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.S_Square = checkBox3.Checked;
            TextBoxCheck();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "0";
            }
            Properties.Settings.Default.Custom_X = Convert.ToInt32(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "0";
            }
            Properties.Settings.Default.Custom_Y = Convert.ToInt32(textBox2.Text);
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            getSettings();
        }
    }
}

