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
    public partial class databaseView : Form
    {
        public databaseView()
        {
            InitializeComponent();
        }

        private void databaseView_Load(object sender, EventArgs e)
        {
            // Retrieve data from the database table
            DataTable table = GetDataFromDatabase();

            // Set the data source for the DataGridView control
            dataGridView1.DataSource = table;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }


        private DataTable GetDataFromDatabase()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Ori Nissim\\source\\repos\\BallsGame_315903518\\playersDB.mdf\";Integrated Security=True";

            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM TblPlayers"; 

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(table);
            }

            return table;
        }

        private DataTable getPlayerWithLongestTime()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Ori Nissim\\source\\repos\\BallsGame_315903518\\playersDB.mdf\";Integrated Security=True";

            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP 1 * FROM TblPlayers ORDER BY Length DESC";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(table);
            }

            return table;
        }

        private DataTable getMaxTimeForEachPlayer()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Ori Nissim\\source\\repos\\BallsGame_315903518\\playersDB.mdf\";Integrated Security=True";

            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Name, MAX(Length) AS LongestGame FROM TblPlayers GROUP BY Name";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(table);
            }

            return table;
        }
        private void Q1_Click(object sender, EventArgs e)
        {
            // Retrieve data from the database table
            DataTable table = getPlayerWithLongestTime();

            // Set the data source for the DataGridView control
            dataGridView1.DataSource = table;

        }

        private void All_Click(object sender, EventArgs e)
        {
            // Retrieve data from the database table
            DataTable table = GetDataFromDatabase();

            // Set the data source for the DataGridView control
            dataGridView1.DataSource = table;
        }

        private void Q2_Click(object sender, EventArgs e)
        {
            // Retrieve data from the database table
            DataTable table = getMaxTimeForEachPlayer();

            // Set the data source for the DataGridView control
            dataGridView1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
