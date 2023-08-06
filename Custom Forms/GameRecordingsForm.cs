using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BallsGame_315903518
{
    public partial class GameRecordingsForm : Form
    {
        private int currentPlayerID;
        public event EventHandler<string> MovesSelected;

        public GameRecordingsForm(int id)
        {
            currentPlayerID = id;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void GamescomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private List<int> GetGamesForPlayer(int playerId)
        {
            string connectionString = "Data Source=LAPTOP-1HADQVHU\\SQLEXPRESS01;Initial Catalog=ConnectFourDB;Integrated Security=True";

            List<int> gamesList = new List<int>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id FROM GameTable WHERE PlayerID = @PlayerID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PlayerID", playerId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        int gameId = reader.GetInt32(0);
                        gamesList.Add(gameId);
                    }
                }

                reader.Close();
            }

            return gamesList;
        }
        private string GetSelectedGame(int gameId)
        {
            string connectionString = "Data Source=LAPTOP-1HADQVHU\\SQLEXPRESS01;Initial Catalog=ConnectFourDB;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Moves FROM GameTable WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", gameId);

                connection.Open();
                string moves = command.ExecuteScalar() as string;
                // The ExecuteScalar method returns the first column of the first row, cast it to string.

                return moves;
            }
        }

        private void GameRecordingsForm_Load(object sender, EventArgs e)
        {
            List<int> games = GetGamesForPlayer(currentPlayerID);
            foreach (int gameName in games)
            {
                GamescomboBox.Items.Add(gameName);
            }
        }

        private void OkBtnRecordingForm_Click(object sender, EventArgs e)
        {

            // When the OK button is clicked, close the form
            int selectedGameId = (int)GamescomboBox.SelectedItem;
            string moves = GetSelectedGame(selectedGameId);
            Console.WriteLine(moves);

            // Trigger the MovesSelected event and pass the moves string
            MovesSelected?.Invoke(this, moves);
            this.Close();
            
        }
    }
}
