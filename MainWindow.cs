using BallsGame_315903518.Classes;
using ConnectedFour.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
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
        // constants paths
        public string NEW_MOVE_PATH = "api/Players/move";
        public bool isFromRecording = false;
        public Game currentGame = new Game();

        public DiscColor currentColor = DiscColor.Red;
        public bool firstClickOnPlus = true;
        public bool isGameActive = true; // change to false after we add authentication
        public string id = string.Empty; 
        public string name = string.Empty;

        internal System.Windows.Forms.Timer timer;
        internal Stopwatch stopwatch = new Stopwatch();

        internal Disc[] discsArray = new Disc[100]; //all discs
        internal int discCounter = 0;

        internal int[,] discs = new int[6, 7];// rectangle matrix 
        internal int[] takenSpotsInColumn = new int[7];// מערך שמחזיק כמה מקומות תפוסים כל עמודה 
        internal LinkedList<int> moves = new LinkedList<int>();// מחזיק את הצעדים

        // WEB API 
        internal static HttpClient client = new HttpClient();


        public MainWindow()
        {
            InitializeComponent();

            // Set Up connection to server
            client.BaseAddress = new Uri("https://localhost:7015/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            logInUser();

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
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 20; // Update interval in milliseconds
            timer.Tick += Timer_Tick;


        }
    }
}
