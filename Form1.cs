//Made by Ewan Peterson ICS3U

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dodge
{
    public partial class Form1 : Form
    {
        //setup rectangle lists
        Rectangle player = new Rectangle(10, 185, 30, 30);
        int playerSpeed = 6;

        List<Rectangle> leftObstacles = new List<Rectangle>();

        List<Rectangle> rightObstacles = new List<Rectangle>();

        List<Rectangle> centreObstacles = new List<Rectangle>();

        //setup variables
        int obstacleSpeed = 8;
        int centreObstacleSpeed = 5;
        int newObstacleTimer = 0;
        int newCentreObstacleTimer = 0;

        bool leftDown = false;
        bool rightDown = false;
        bool upDown = false;
        bool downDown = false;

        //setup brushes
        SolidBrush playerBrush = new SolidBrush(Color.BlueViolet);
        SolidBrush obstacleBrush = new SolidBrush(Color.White);

        public Form1()
        {
            leftObstacles.Add(new Rectangle(200, 0, 20, 50));
            rightObstacles.Add(new Rectangle(400, 0, 20, 50));
            centreObstacles.Add(new Rectangle(300, 400, 20, 200));
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;
                case Keys.Up:
                    upDown = true;
                    break;
                case Keys.Down:
                    downDown = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break;
                case Keys.Up:
                    upDown = false;
                    break;
                case Keys.Down:
                    downDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move player
            if (leftDown == true && player.X > 0)
            {
                player.X -= playerSpeed;
            }
            if (rightDown == true && player.X < this.Width - player.Width)
            {
                player.X += playerSpeed;
            }
            if (upDown == true && player.Y > 0)
            {
                player.Y -= playerSpeed;
            }
            if (downDown == true && player.Y < this.Height - player.Height)
            {
                player.Y += playerSpeed;
            }

            //move obstacles

            for (int i = 0; i < leftObstacles.Count; i++)
            {
                int leftY = leftObstacles[i].Y + obstacleSpeed;
                leftObstacles[i] = new Rectangle(200, leftY, 20, 50);

                int rightY = rightObstacles[i].Y + obstacleSpeed;
                rightObstacles[i] = new Rectangle(400, rightY, 20, 50);
            }

            for (int i = 0; i < centreObstacles.Count; i++)
            {
                int centreY = centreObstacles[i].Y - +centreObstacleSpeed;
                centreObstacles[i] = new Rectangle(300, centreY, 20, 200);
            }

            //new obstacles
            newObstacleTimer++;
            newCentreObstacleTimer++;

            if (newObstacleTimer == 15)
            {
                leftObstacles.Add(new Rectangle(200, 0, 20, 50));
                rightObstacles.Add(new Rectangle(400, 0, 20, 50));

                newObstacleTimer = 0;
            }

            if(newCentreObstacleTimer == 50)
            {
                centreObstacles.Add(new Rectangle(300, 400, 20, 200));
                newCentreObstacleTimer = 0;
            }

            //remove obstacles
            if (leftObstacles[0].Y > this.Height)
            {
                leftObstacles.Remove(leftObstacles[0]);
            }
                
            if(rightObstacles[0].Y > this.Height)
            {
                rightObstacles.Remove(rightObstacles[0]);
            }

            if(centreObstacles[0].Y < -200)
            {
                centreObstacles.Remove(centreObstacles[0]);
            }

            //check for collision with obstacle
            for(int i = 0; i < leftObstacles.Count; i++)
            {
                if (player.IntersectsWith(leftObstacles[i]))
                {
                    gameTimer.Enabled = false;
                }

                if(player.IntersectsWith(rightObstacles[i]))
                {
                    gameTimer.Enabled = false;
                }
            }

            for (int i = 0; i < centreObstacles.Count; i++)
            {
                if(player.IntersectsWith(centreObstacles[i]))
                {
                    gameTimer.Enabled = false;
                }
            }

            //check for player reaching end
            if (player.X > this.Width - player.Width)
            {
                gameTimer.Enabled = false;
            }
                Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //draw player
            e.Graphics.FillRectangle(playerBrush, player);
            for(int i = 0; i < leftObstacles.Count; i++)
            {
                e.Graphics.FillRectangle(obstacleBrush, leftObstacles[i]);
                e.Graphics.FillRectangle(obstacleBrush, rightObstacles[i]);
            }
            
            for(int i = 0; i < centreObstacles.Count; i++)
            {
                e.Graphics.FillRectangle(obstacleBrush, centreObstacles[i]);
            }
        }
    }

        
    
}
