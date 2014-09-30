using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zmiq
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();
        public Form1()
        {
            InitializeComponent();

            new Settings();

            timer.Interval = 1000 / Settings.speed;
            timer.Tick += updateScreen;
            timer.Start();

            StartGame();
        }
        private void StartGame()
        {
            gameoverlabel.Visible = false;
            new Settings();

            Snake.Clear();
            Circle head = new Circle();
            head.x = 10;
            head.y = 5;
            Snake.Add(head);
            score.Text = Settings.score.ToString();
            generateFood();
        }
        private void generateFood()
        {
            int maxXpos = snake_cage.Size.Width / Settings.widht;
            int maxYpos = snake_cage.Size.Height / Settings.height;
            Random rand = new Random();
            food = new Circle();
            food.x = rand.Next(0, maxXpos);
            food.y = rand.Next(0, maxYpos);
        }
        private void updateScreen(object sender, EventArgs e)
        {
            if (Settings.gameOver)
            {
                if (Input.KeyPressed(Keys.Enter))
                    StartGame();
            }
            else
            {
                if (Settings.direction != Direction.right && Input.KeyPressed(Keys.Left))
                    Settings.direction = Direction.left;
                else if (Settings.direction != Direction.left && Input.KeyPressed(Keys.Right))
                    Settings.direction = Direction.right;
                else if (Settings.direction != Direction.up && Input.KeyPressed(Keys.Down))
                    Settings.direction = Direction.down;
                else if (Settings.direction != Direction.down && Input.KeyPressed(Keys.Up))
                    Settings.direction = Direction.up;
                MovePlayer();
            }
            snake_cage.Invalidate();
        }

        private void snake_cage_Paint(object sender, PaintEventArgs e)
        {
            Graphics cage = e.Graphics;
            Brush colour;
            if(Settings.gameOver == false)
            {
                for(int i=0; i<Snake.Count; i++)
                {
                    if (i == 0) colour = Brushes.Black;
                    else colour = Brushes.Green;
                    cage.FillEllipse(colour, 
                        new Rectangle(Snake[i].x * Settings.widht,
                                      Snake[i].y * Settings.height,
                                      Settings.widht, Settings.height));
                    for (int j = 0; j < Snake.Count; j++)
                    {
                        if (food.x == Snake[j].x && food.y == Snake[j].y)
                            generateFood();
                        else
                        cage.FillEllipse(Brushes.Red,
                            new Rectangle(food.x * Settings.widht,
                                          food.y * Settings.height,
                                          Settings.widht, Settings.height));
                    }
                }
            }
            else
            {
                string gameOver = "palqk, umrq \ntochki: " + Settings.score + "\nnatisni entar za vtora proba ;)";
                gameoverlabel.Text = gameOver;
                gameoverlabel.Visible = true;
            }

        }
        private void MovePlayer()
        {
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Settings.direction)
                    {
                        case Direction.right:
                            Snake[i].x++;
                            break;
                        case Direction.left:
                            Snake[i].x--;
                            break;
                        case Direction.up:
                            Snake[i].y--;
                            break;
                        case Direction.down:
                            Snake[i].y++;
                            break;
                    }
                    int maxXpos = snake_cage.Size.Width / Settings.widht;
                    int maxYpos = snake_cage.Size.Height / Settings.height;
                    if (Snake[0].x >= maxXpos || Snake[0].y >= maxYpos || Snake[0].x < 0 || Snake[0].y < 0) die();
                    for(int j=1; j<Snake.Count; j++)
                    {
                        if (Snake[0].x == Snake[j].x && Snake[0].y == Snake[j].y) die();
                    }
                    if (Snake[0].x == food.x && Snake[0].y == food.y) eat();

                }
                else
                {
                    Snake[i].x = Snake[i - 1].x;
                    Snake[i].y = Snake[i - 1].y;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }
        private void die()
        {
            Settings.gameOver = true;
        }
        private void eat()
        {
            Circle nov = new Circle();
            nov.x = Snake[Snake.Count-1].x;
            nov.y = Snake[Snake.Count-1].y;
            Snake.Add(nov);
            Settings.score += Settings.points_per_circle;
            score.Text = Settings.score.ToString();
            generateFood();
        }
    }
}
