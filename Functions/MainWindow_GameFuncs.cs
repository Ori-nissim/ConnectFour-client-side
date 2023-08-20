using BallsGame_315903518;
using BallsGame_315903518.Classes;
using ConnectedFour.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BallsGame_315903518
{
    public partial class MainWindow : Form
    {
      
        

        private void blockButtons(bool Value)
        {

            button1.Visible = !Value;
            button2.Visible = !Value;
            button3.Visible = !Value;
            button4.Visible = !Value;
            button5.Visible = !Value;
            button6.Visible = !Value;
            button7.Visible = !Value;


        }

        private void changeCurrentColor()
        {
            if (currentColor == DiscColor.Red)
            {
                currentColor = DiscColor.Yellow;
                lblPlayerColor.Text = "Yellow (Computer)";
                lblPlayerColor.ForeColor = Color.YellowGreen;



            }
            else
            {
                currentColor = DiscColor.Red;
                if (name == string.Empty) name = "You";
                lblPlayerColor.Text = "Red (" + name + ")";
                lblPlayerColor.ForeColor = Color.Red;

            }
        }

        private void resetCurrentColor()
        {
            currentColor = DiscColor.Red;
            if (name == string.Empty) name = "You";
            lblPlayerColor.Text = "Red (" + name + ")";
            lblPlayerColor.ForeColor = Color.Red;
            Console.WriteLine("Color resetted");

        }


        private bool CheckForWin(int[,] board, int playerColor)
        {
            // Check horizontally
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row, col] == playerColor &&
                        board[row, col + 1] == playerColor &&
                        board[row, col + 2] == playerColor &&
                        board[row, col + 3] == playerColor)
                    {
                        return true;
                    }
                }
            }

            // Check vertically
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (board[row, col] == playerColor &&
                        board[row + 1, col] == playerColor &&
                        board[row + 2, col] == playerColor &&
                        board[row + 3, col] == playerColor)
                    {
                        return true;
                    }
                }
            }

            // Check diagonally (from top-left to bottom-right)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row, col] == playerColor &&
                        board[row + 1, col + 1] == playerColor &&
                        board[row + 2, col + 2] == playerColor &&
                        board[row + 3, col + 3] == playerColor)
                    {
                        return true;
                    }
                }
            }

            // Check diagonally (from top-right to bottom-left)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 3; col < 7; col++)
                {
                    if (board[row, col] == playerColor &&
                        board[row + 1, col - 1] == playerColor &&
                        board[row + 2, col - 2] == playerColor &&
                        board[row + 3, col - 3] == playerColor)
                    {
                        return true;
                    }
                }
            }

            return false; // No win found
        }

        private void PrintArray(int[,] array)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);

            Console.WriteLine();
            Console.WriteLine("Turn Number " + discCounter);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{array[i, j]} ");
                }
                Console.WriteLine();
            }
        }


        private int[] GetStrAsIntArray(string movesString)
        {
            String[] values = movesString.Split(',');
            return Array.ConvertAll(values, s => int.Parse(s));

        }

        // GameID | String Moves  

        private async void playGameRecording(int[] moves)
        {
            isFromRecording = true;
            for (int i = 0; i < moves.Length; i++)
            {
                insertDiscEventHandler(moves[i]);
                await Task.Delay(2000);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            for (int i = 0; i < discCounter; i++)
            {
                if (discsArray[i].isMoving)
                {
                    discsArray[i].Move(ClientRectangle.Height, ClientRectangle.Width);

                    if (discsArray[i].Position.Y < 0 || discsArray[i].Position.Y + discsArray[i].Size - discsArray[i].discStopPosition > pictureBox1.Height - 675) //675

					{
                        discsArray[i].isMoving = false;
                    }
                }

            }



            pictureBox1.Invoke(new Action(() =>
            {
                pictureBox1.Refresh();
            }));
        }

       

       
        private void GameRecordingForm_MovesSelected(object sender, string moves)
        {
            int[] movesArray = moves.Split(',').Select(int.Parse).ToArray();
            
            for (int i =0; i < movesArray.Length; i ++ )
            Console.WriteLine("$$$: " + movesArray[i]);

            playGameRecording(movesArray);
        }
        private void btnDatabase_Click(object sender, EventArgs e)
        {
            // Create an instance of the GameRecordingForm
            using (var gameRecordingForm = new GameRecordingsForm(Convert.ToInt32(id)))
            {
                // Subscribe to the MovesSelected event
                gameRecordingForm.MovesSelected += GameRecordingForm_MovesSelected;

                // Show the form as a dialog
                gameRecordingForm.ShowDialog();

                // Unsubscribe from the event (optional, to avoid memory leaks)
                gameRecordingForm.MovesSelected -= GameRecordingForm_MovesSelected;
            }
        }
        

        private void ShowExitDialog()
        {


            // Display a message box with an icon, text, and three buttons.
            DialogResult result = MessageBox.Show(
                "Are you sure you wanna do that??",
                "Think about it",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
                );

            // Process the user's response.
            switch (result)
            {
                case DialogResult.Yes:
                    saveGameAndReset();
                    this.Close();
                    break;

                case DialogResult.No:
                    // continue
                    break;


            }
        }

        public void savePlayerDetails(string moves, int id)
        {
            string connectionString = "Data Source=LAPTOP-1HADQVHU\\SQLEXPRESS01;Initial Catalog=ConnectFourDB;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO GameTable (Moves, PlayerID) VALUES (@Moves, @PlayerID) ;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Moves", moves);
                command.Parameters.AddWithValue("@PlayerID", id);

                connection.Open();
                int newPlayerId = Convert.ToInt32(command.ExecuteScalar());

                // The newPlayerId variable now holds the automatically generated ID for the newly inserted player
                // You can use it as needed
            }
        }

     

        public void saveGameAndReset()
        {
            stopwatch.Stop();

            currentGame.Duration = stopwatch.ElapsedMilliseconds / 1000;
            currentGame.Moves = GetMovesAsString();

            discCounter = 0;
            discsArray = new Disc[100];
            discs = new int[6, 7];
            takenSpotsInColumn = new int[7];

            if (!isFromRecording)
            {
                // Save game in server DB
                saveGameInServer(currentGame);

                // Save game recording in local DB
                savePlayerDetails(GetMovesAsString(), Convert.ToInt32(id));  // converts the linked list of moves to this format - "1,3,4,5,4" - String
            }

            resetCurrentColor();
            lblballsCouinter.Text = "Turn number: " + discCounter;

            stopwatch.Reset();
            isGameActive = true;
            currentGame.StartTime = DateTime.Now;

            // give information to the user
            if (isFromRecording)
            {
                DialogResult dr = MessageBox.Show(
                    "You Can Start a New Game",
                    "Game Reseted",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None
                    );
            }
            else
            {
                DialogResult dr = MessageBox.Show(
                    "You Can Start a New Game",
                    "Game Reseted and Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None
                    );
            }

            isFromRecording = false;

        }

        private async void saveGameInServer(Game game)
        {
            try
            {
                var url = await CreateGameAsync(game);
                Console.WriteLine($"Created at {url}");

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }


        }

        private string GetMovesAsString()
        {
            return String.Join(",", moves);
        }

      

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            for (int i = 0; i < discCounter; i++)
            {

                e.Graphics.DrawEllipse(discsArray[i].Pen, discsArray[i].Position.X, discsArray[i].Position.Y, discsArray[i].Size, discsArray[i].Size);
                e.Graphics.FillEllipse(discsArray[i].Brush, new Rectangle(discsArray[i].Position, new Size(discsArray[i].Size, discsArray[i].Size)));

            }
        }

        private void createNewBall(int location, int discStopPosition, DiscColor color)
        {
            Disc ball = new Disc();
            ball.discStopPosition = discStopPosition;
            ball.Position = new Point(52 + 100 * location, 0);
            ball.isMoving = true;
            Color discColor;

            if (color == DiscColor.Red)
                discColor = Color.FromArgb(255, 0, 0);

            else // yellow
                discColor = Color.FromArgb(255, 255, 0);


            ball.Pen = new Pen(discColor);
            ball.Brush = new SolidBrush(discColor);

            ball.Size = 85;

            // rectangle for filling the ball with color
            ball.rectangle = new Rectangle(ball.Position, new Size(ball.Size, ball.Size));

            discsArray[discCounter++] = ball;
            lblballsCouinter.Text = "Turn number: " + discCounter;
        }
        private int calculatDiscHeight(int[,] discs, int takenSlotsNumber)
        {
            int newPosition = 6 - takenSlotsNumber;
            return newPosition * 100;
        }
        private void InsertDisc(int[,] discs, int col, DiscColor color)
        {
            for (int i = 5; i >= 0; i--)
                if ((int)discs.GetValue(i, col) == 0)
                {
                    discs.SetValue(color, i, col);
                    return;
                }

        }
        private async void insertDiscEventHandler(int location)
        {
            if (!isGameActive) return;

            if (isColumnFull(location))
            {
                DialogResult dr = MessageBox.Show(
                    "The column is full !!!",
                    "Invalid move",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );

                return;
            }

            // Insert move to game recording
            if (!isFromRecording) SaveMove(location);//save to link list


            int discStopPosition = calculatDiscHeight(discs, takenSpotsInColumn[location - 1]);
            takenSpotsInColumn[location - 1]++; // update remaining spots

            createNewBall(location, discStopPosition, currentColor); // Draws the disc on the screen
            InsertDisc(discs, location - 1, currentColor); // updates the matrix in the background so we can keep track of all positions

            // Now check for win - if 4 discs has been inserted already
            if (discCounter >= 4)
            {
                if (CheckForWin(discs, (int)currentColor) && isFromRecording == false)  // returns True if the current player has won
                {
                    isGameActive = false; // disable actions

                    DialogResult result = MessageBox.Show(
                        currentColor.ToString() + " Player won! \nDo you want to play again?",
                        "We have a Winner!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.None
                        );


                    // Process the user's response.
                    switch (result)
                    {
                        case DialogResult.Yes:
                            saveGameAndReset();
                            return;

                        case DialogResult.No:
                            // continue
                            return;


                    }


                }

            }

            PrintArray(discs);
            changeCurrentColor();

            if (currentColor == DiscColor.Yellow && !isFromRecording)
            {
                Console.WriteLine("Function runs");
                blockButtons(true);
                getMoveFromServer();


            }

            timer.Enabled = true;
            timer.Start();


        }

        private bool isColumnFull(int location)
        {
            return takenSpotsInColumn[location - 1] == 6;
        }

        private void SaveMove(int location)
        {
            moves.AddLast(location);
        }

       
    }
}
