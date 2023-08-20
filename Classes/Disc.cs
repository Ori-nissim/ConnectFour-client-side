using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallsGame_315903518
{
    public class Disc
    {

        public int Column { get; set; }
        public int Row { get; set; }
        public int Color { get; set; }
        public int Size { get; set; }
        public bool isMoving { get; set; }
        public Point Position { get; set; }
        public int discStopPosition { get; set; }
        public Pen Pen { get; set; }
        public Rectangle rectangle { get; set; }
         public Brush Brush { get; set; }

        public void Move(int height, int width)
        {
			if (Math.Abs(discStopPosition - Position.Y ) < 60 )//בדיקת מרחק גדול 
            {
				Position = new Point(Position.X, Position.Y + 3);

			}
			else if (Position.Y != height - (Size) ) 
            {
               // Console.WriteLine("Position Y: " + Position.Y + ", stop position: " + discStopPosition );
                Position = new Point(Position.X, Position.Y + 15);

            }
            else
            {
				isMoving = false;
            }
           
        }


    }
}
