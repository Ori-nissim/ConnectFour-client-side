using ConnectedFour.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BallsGame_315903518
{
    public partial class WellcomeUser : Form
    {
        public WellcomeUser(Player player)
        {
            InitializeComponent();
            lblWelcome.Text = "Welcome " + player.Name + "!";
            lblCountry.Text = player.Country;
            lblID.Text = player.Id.ToString();
            lblPhoneNumber.Text = player.Phone;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
