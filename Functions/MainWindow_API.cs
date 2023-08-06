using BallsGame_315903518.Classes;
using ConnectedFour.Models;
using System;
using System.Collections.Generic;
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
        static async Task<Uri> CreateGameAsync(Game game)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/Games", game);
            response.EnsureSuccessStatusCode();
            await Console.Out.WriteLineAsync(response.ToString());
            // return URI of the created resource.
            return response.Headers.Location;
        }

        private async void getMoveFromServer()
        {

            try
            {
                // make sure the move from the server is a column that has enough space
                int move = -1;
                do
                {
                    move = await GetMoveAsync(NEW_MOVE_PATH);
                    await Console.Out.WriteLineAsync(move.ToString());
                }
                while (isColumnFull(move));

                // play the valid move
                await Task.Delay(3000);
                insertDiscEventHandler(move);
                await Task.Delay(1000);
                blockButtons(false);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        static async Task<int> GetMoveAsync(string path)
        {
            int move = 0;
            HttpResponseMessage response = await client.GetAsync(path);
            await Console.Out.WriteLineAsync(response.ToString());

            if (response.IsSuccessStatusCode)
            {
                move = await response.Content.ReadAsAsync<int>();
            }
            return move;
        }


        private async Task<IEnumerable<Player>> ShowAllPlayersFromServer()
        {
            string path = "api/Players"; // Ensure the correct controller name

            IEnumerable<Player> players = null;

            HttpResponseMessage response = await client.GetAsync(path);
            await Console.Out.WriteLineAsync(response.ToString());


            if (response.IsSuccessStatusCode)
            {
                players = await response.Content.ReadAsAsync<IEnumerable<Player>>();
            }
            return players;
        }


        static async Task<Player> GetPlayerAsync(string id)
        {
            string path = "api/Players/" + id; // Ensure the correct controller name

            Player player = null;
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                player = await response.Content.ReadAsAsync<Player>();
            }
            return player;
        }

       
    }
}

