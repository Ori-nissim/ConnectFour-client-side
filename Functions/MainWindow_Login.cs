
using ConnectedFour.Models;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace BallsGame_315903518
{
    public partial class MainWindow : Form
    {
        private async void logInUser()
        {
            bool isUserLoggedIn = false;
            do
            {
                isUserLoggedIn = await promptUserLogin();
            }
            while (!isUserLoggedIn);

        }

        private async Task<bool> promptUserLogin()
        {
            DialogResult dialogResult = DialogResult.None;
            do
            {
                dialogResult = InputBox("Welcome To Connect Four", "Please Log In To Play\nEnter your ID:\n", ref id);
                if (dialogResult == DialogResult.Cancel) // User cancels --> close the program
                {
                    System.Environment.Exit(1);

                }

            } while (id == ""); // do not allow empty IDs

            // Check if exists in database
            Player currentPlayer = null;
            try
            {
                currentPlayer = await GetPlayerAsync(id);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
                return false;
            }

            if (currentPlayer == null)
            {
                DialogResult dr = MessageBox.Show(
                   "User Not found, Try again",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error
                   );

                return false;
            }
            await Console.Out.WriteLineAsync(currentPlayer.ToString());

            while (currentPlayer == null) ;


            // Greet user
            WellcomeUser wellcomeUser = new WellcomeUser(currentPlayer);
            wellcomeUser.ShowDialog();

            //initialize game parameters
            currentGame.PlayerID = currentPlayer.Id;
            currentGame.StartTime = DateTime.Now;
            stopwatch.Start();

            name = currentPlayer.Name;
            lblHello.Text = "Hello, " + name;
            firstClickOnPlus = false;
            return true;

        }
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;
            label.SetBounds(36, 36, 372, 13);
            textBox.SetBounds(36, 86, 700, 20);
            buttonOk.SetBounds(228, 160, 160, 60);
            buttonCancel.SetBounds(400, 160, 160, 60);
            label.AutoSize = true;
            form.ClientSize = new Size(796, 307);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;
            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

      
    }
}