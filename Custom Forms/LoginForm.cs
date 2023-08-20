using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Windows.Forms;

namespace BallsGame_315903518.Custom_Forms
{
    public partial class LoginForm : Form
    {
        public string userID;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            userID = userInput.Text;
            this.DialogResult = DialogResult.OK; 
            this.Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    this.DialogResult = DialogResult.Cancel;
            //}
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				VisitLink();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Unable to open link that was clicked.");
			}
		}

		private void VisitLink()
		{
			// Change the color of the link text by setting LinkVisited
			// to true.
			linkLabel1.LinkVisited = true;
			//Call the Process.Start method to open the default browser
			//with a URL:
			System.Diagnostics.Process.Start("https://localhost:7015/Players/Create");
		}
	
	}
}
