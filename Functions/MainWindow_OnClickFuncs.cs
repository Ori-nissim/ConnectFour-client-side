using BallsGame_315903518.Classes;
using ConnectedFour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BallsGame_315903518
{
    public partial class MainWindow : Form
    {
        
       
        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // ENTER KEY Pressed
                btnPlus_click(sender, e);

            if (e.KeyCode == Keys.S)  // S KEY Pressed
                toolStripLabel1_Click(sender, e); // stop the last ball
            if (e.KeyCode == Keys.E)  // E KEY Pressed
                btnExit_Click(sender, e); // Save and exit


        }

        private void button8_Click(object sender, EventArgs e)
        {
            string movesString = "1,2,3,4,5,3,2,4,5";

            int[] moves = GetStrAsIntArray(movesString);
            playGameRecording(moves);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            insertDiscEventHandler(1);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            insertDiscEventHandler(2);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            insertDiscEventHandler(3);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            insertDiscEventHandler(4);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            insertDiscEventHandler(5);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            insertDiscEventHandler(6);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            insertDiscEventHandler(7);

        }

        private void btnPlus_click(object sender, EventArgs e)
        {

            if (firstClickOnPlus)
            {
                DialogResult dialogName = InputBox("Enter your name", "Name:", ref name);
                lblHello.Text = "Hello, " + name;
                firstClickOnPlus = false;
            }
            else
            {
                if (isGameActive == false)
                {
                    isGameActive = true;
                }

                //createNewBall();
                timer.Enabled = true;
                timer.Start();
            }


        }


        // MINUS button
        private async void toolStripButton2_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                IEnumerable<Player> players = await ShowAllPlayersFromServer();
                await Console.Out.WriteLineAsync("Players list:\n" + players.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void btnExit_Click(object sender, EventArgs e)
        {

            ShowExitDialog();

        }
        private void btnEndGame_Click(object sender, EventArgs e)
        {
            saveGameAndReset();
        }
        private async void btnAbout_Click(object sender, EventArgs e)
        {
            

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {

        }


        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {

        }


        private void MainWindow_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void lblballsCouinter_Click(object sender, EventArgs e)
        {

        }
    }
}
