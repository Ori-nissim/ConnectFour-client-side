using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BallsGame_315903518
{
    public enum DiscColor
    {
        Red = 1,
        Yellow = 2
    }
    public partial class MainWindow : Form
    {
        DiscColor currentColor = DiscColor.Red;
        bool firstClickOnPlus = true;
        bool isGameActive = true; // change to false after we add authentication
        string name = string.Empty;

        private Timer timer;
        Stopwatch stopwatch = new Stopwatch();

        private Disc[] discsArray = new Disc[100];
        private int discCounter = 0;

        private int[,] discs = new int[6, 7];
        private int[] takenSpotsInColumn = new int[7];
        public MainWindow()
        {
            InitializeComponent();
            Console.Write("Hello");
            //initialize matrix
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 7; j++)
                {
                    discs.SetValue(0, i, j);
                }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            timer = new Timer();
            timer.Interval = 20; // Update interval in milliseconds
            timer.Tick += Timer_Tick;
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }
        private void Timer_Tick(object sender, EventArgs e)
        {

            for (int i = 0; i < discCounter; i++)
            {
                if (discsArray[i].isMoving)
                {
                    discsArray[i].Move(ClientRectangle.Height, ClientRectangle.Width);

                    if(discsArray[i].Position.Y<0 || discsArray[i].Position.Y + discsArray[i].Size - discsArray[i].discStopPosition > pictureBox1.Height-675)
                    {
                        discsArray[i].isMoving=false;
                    }    
                }
               
            }

            

            pictureBox1.Invoke(new Action(() =>
            {
                pictureBox1.Refresh();
            }));
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
                    stopwatch.Start();
                }

                //createNewBall();
                timer.Enabled = true;
                timer.Start();
            }


        }



        // MINUS button
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (discCounter >= 1)
            {

                discsArray[discCounter - 1].Pen.Dispose();
                discsArray[discCounter - 1].Brush.Dispose();

                discsArray[discCounter - 1] = null;
                discCounter--;
                lblballsCouinter.Text = "Number of Balls: " + discCounter;

            }
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

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // ENTER KEY Pressed
                btnPlus_click(sender, e);

            if (e.KeyCode == Keys.S)  // S KEY Pressed
                toolStripLabel1_Click(sender, e); // stop the last ball
            if (e.KeyCode == Keys.E)  // E KEY Pressed
                btnExit_Click(sender, e); // Save and exit


        }

        private void btnDatabase_Click(object sender, EventArgs e)
        {
            databaseView newForm = new databaseView();
            newForm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            ShowExitDialog();

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

        public void savePlayerDetails(string playerName, int length)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Ori Nissim\\source\\repos\\BallsGame_315903518\\playersDB.mdf\";Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO TblPlayers (Name, Length) VALUES (@Name, @Length); SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", playerName);
                command.Parameters.AddWithValue("@Length", length);

                connection.Open();
                int newPlayerId = Convert.ToInt32(command.ExecuteScalar());

                // The newPlayerId variable now holds the automatically generated ID for the newly inserted player
                // You can use it as needed
            }
        }

        private void btnEndGame_Click(object sender, EventArgs e)
        {
            saveGameAndReset();
        }

        public void saveGameAndReset()
        {
            stopwatch.Stop();

            discCounter = 0;
            discsArray = new Disc[100];


            discs = new int[6, 7];
            takenSpotsInColumn = new int[7];
            //savePlayerDetails(name, stopwatch.Elapsed.Seconds);
            resetCurrentColor();
            lblballsCouinter.Text = "Turn number: " + discCounter;

            stopwatch.Reset();
            isGameActive = true;

            //name = "Guest";
            //lblHello.Text = "Hello, " + name;

        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            for (int i = 0; i < discCounter; i++)
            {

                e.Graphics.DrawEllipse(discsArray[i].Pen, discsArray[i].Position.X, discsArray[i].Position.Y, discsArray[i].Size, discsArray[i].Size);
                e.Graphics.FillEllipse(discsArray[i].Brush, new Rectangle(discsArray[i].Position, new Size(discsArray[i].Size, discsArray[i].Size)));

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void createNewBall()
        {
            Disc ball = new Disc();

            ball.Position = new Point(30, 0);
            ball.isMoving = true;

            Color randomColor = Color.FromArgb(255, 0, 0);

            ball.Pen = new Pen(randomColor);
            ball.Brush = new SolidBrush(randomColor);

            ball.Size = 30;

            // rectangle for filling the ball with color
            ball.rectangle = new Rectangle(ball.Position, new Size(ball.Size, ball.Size));

            discsArray[discCounter++] = ball;
            lblballsCouinter.Text = "Turn number: " + discCounter;
        }

        private void createNewBall(int location, int discStopPosition, DiscColor color)
        {
            Disc ball = new Disc();
            ball.discStopPosition = discStopPosition;
            ball.Position = new Point(52+100*location, 0);
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
        private int calculatDiscHeight(int[,] discs,int takenSlotsNumber)
        {
            int newPosition = 6 - takenSlotsNumber;
            return newPosition * 100;
        }
        private void InsertDisc(int[,]discs,int col, DiscColor color)
        {
            for (int i = 5; i >= 0; i--)
                if ((int)discs.GetValue( i, col) == 0)
                {
                    discs.SetValue(color, i, col);
                    return;
                }
                
        }
        private void insertDiscEventHandler(int location)
        {
            if (!isGameActive) return;

            if (takenSpotsInColumn[location - 1] == 6)
            {
                DialogResult dr = MessageBox.Show(
                    "The column is full !!!",
                    "Invalid move", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );

                return;
            }

            int discStopPosition = calculatDiscHeight(discs, takenSpotsInColumn[location - 1]);
            takenSpotsInColumn[location - 1]++; // update remaining spots

            createNewBall(location, discStopPosition, currentColor); // Draws the disc on the screen
            InsertDisc(discs, location - 1, currentColor); // updates the matrix in the background so we can keep track of all positions

            // Now check for win - if 4 discs has been inserted already
            if (discCounter >=4)
            {
                if (CheckForWin(discs, (int)currentColor))  // returns True if the current player has won
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
            timer.Enabled = true;
            timer.Start();
            

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

        private void label1_Click(object sender, EventArgs e)
        {

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

    }
}