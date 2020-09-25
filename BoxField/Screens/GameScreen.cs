using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace BoxField
{
    public partial class GameScreen : UserControl
    {
        //player1 button control keys
        Boolean leftArrowDown, rightArrowDown;

        //used to draw boxes on screen
        Random randNum = new Random();
        SolidBrush boxBrush = new SolidBrush(Color.White);

        //used to draw palyer on screen
        SolidBrush playerBrush = new SolidBrush(Color.White);

        // a list to hold a column of boxes        
        List<Box> left = new List<Box>();
        List<Box> right = new List<Box>();
        int leftX = 200;
        int gap = 300;

        Box hero;
        int heroSpeed = 10;
        int heroSize = 30;
        Boolean moveRight = true;
        int patternLength = 10;


        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        public void MakeBox()
        {
            int rand = randNum.Next(1, 4);
            Color c = Color.White;

            if (rand == 1)
            {
                c = Color.Red;
            }
            else if (rand == 2)
            {
                c = Color.Yellow;
            }
            else if (rand == 3)
            {
                c = Color.Orange;
            }

            patternLength--;
            if (patternLength == 0)
            {
                moveRight = !moveRight;

                patternLength = randNum.Next(3, 8);
            }

            if (moveRight)
            {
                leftX += 7;
            }
            else
            {
                leftX -= 7;
            }

            // set game start values
            Box newBox = new Box(leftX, 0, 20, c);
            left.Add(newBox);

            Box newBox2 = new Box(leftX + gap - 150, 0, 20, c);
            right.Add(newBox2);
        }

        public void OnStart()
        {
            MakeBox();

            hero = new Box(this.Width / 2 - heroSize / 2, 370, heroSize);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            //TODO - update location of all boxes (drop down screen)
            foreach (Box b in left)
            {
                b.Move(5);
            }

            foreach (Box b in right)
            {
                b.Move(5);
            }

            //TODO - remove box if it has gone of screen
            if (left[0].y > this.Height)
            {
                left.RemoveAt(0);
                right.RemoveAt(0);
            }

            //TODO - add new box if it is time
            if (left[left.Count - 1].y > 21)
            {
                MakeBox();
            }

            //controling hero
            if (leftArrowDown == true)
            {
                hero.x = hero.x - heroSpeed;
            }

            if (rightArrowDown == true)
            {
                hero.x = hero.x + heroSpeed;
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //draw boxes to screen
            foreach (Box b in left)
            {
                boxBrush.Color = b.color;
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
            }

            foreach (Box b in right)
            {
                boxBrush.Color = b.color;
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
            }

            //draw character
            e.Graphics.FillRectangle(playerBrush, hero.x, hero.y, hero.size, hero.size);
        }
    }
}
