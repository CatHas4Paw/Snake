using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class Snake : Form
    {
        private Random _rand = new Random();

        private int _rows = 30, _cols = 30, _stepSize = 20, _score;
        private bool _gameOver = false;
        private string _dir;

        private bool _AI = false;
        private Point[] _hameltonien;
        private int pos = 0;
        int array_size;

        private List<Piece> _snake = new List<Piece>();

        private Piece _food = new Piece();

        public Snake()
        {
            InitializeComponent();
            _Init();
        }
        private void _Init()
        {
            //These variables are needed to start a new game with Enter
            _score = 0;
            lblScore.Text = "Score: " + _score;
            _snake.Clear();
            _gameOver = false;

            //Increase Speed if _AI is true
            if (_AI)
                timer1.Interval = 1;
            else
                timer1.Interval = 100;

            //Initialization
            pbCanvas.Size = new Size(_cols * _stepSize, _rows * _stepSize);
            fillHameltonien();
            _SnakeStartPoint();
            _StartDirection();
            _FoodPos();
            timer1.Start();
        }
        private void fillHameltonien()
        {
            array_size = _cols * _rows;
            _hameltonien = new Point[array_size];
            //so nicht ganz!
            int k = 0;
            //x_achse => reihen = _rows
            for (int i = 0; i < _cols; i++)
            {
                if(0 == i%2)
                {
                    //_hameltonien[i] = new Point(i,0);
                    for (int j = 1; j < _rows; j++)
                    {
                        _hameltonien[k] = new Point(i, j);
                        k++;
                    }
                }
                else
                {
                    //_hameltonien[i] = new Point(i,0);
                    for (int j = _rows-1; j > 0; j--)
                    {
                        _hameltonien[k] = new Point(i, j);
                        k++;
                    }
                }
                //k++;
                lblMessage.Text += "\n";
            }
            for (int i = _cols-1; i <= -1; i--)
            {
                _hameltonien[k] = new Point(i, _cols-1);
                k++;
            }
            for (int i = _rows-1; i >= 0; i--)
            {
                _hameltonien[k] = new Point(i, 0);
                k++;
            }
        }
        private void _SnakeStartPoint()
        {
            Piece head;
            if (_AI)
            { head = new Piece { X = 1, Y = 0 }; }//Start at the middle
            else
            { head = new Piece { X = _cols * _stepSize / 2, Y = _rows * _stepSize / 2 }; } //Start at the middle
            
            _snake.Add(head);
        }
        private void _StartDirection()
        {
            string[] directions = { "w", "a", "s", "d" };
            int index = _rand.Next(directions.Length);       //Random direction at start
            _dir = directions[index];
        }
        private void _FoodPos()
        {
            _food = new Piece { X = _rand.Next(_cols) * _stepSize, Y = _rand.Next(_rows) * _stepSize }; //New random food pos
            //Check if new Random pos is on Snake
            for (int i = 0; i < _snake.Count; i++)
            {
                if (_food.Location == _snake[i].Location)   //If new Apple Pos is on Snake
                    _FoodPos();                             //Try again
            }
        }
        private void _timer1_Tick(object sender, EventArgs e)    //Update screen and the speed of Snake
        {
            if (!_GameOver(new Point(_snake[0].X, _snake[0].Y)))
            {
                _MoveSnake();
            }
            pbCanvas.Invalidate(); //Redraw PictureBox calling the method pbCanvas_Paint
        }
        private void _MoveSnake() 
        {
            for (int i = _snake.Count - 1; i >= 0 ; i--)
            {
                if(i==0)    //i==0 => Head
                {
                    //detection motion direction
                    if(_AI)
                    {
                        _snake[0].X = _hameltonien[pos].X * _stepSize;
                        _snake[0].Y = _hameltonien[pos].Y * _stepSize;

                        pos++;
                        if (pos > array_size-1)
                            pos = 0;
                    }
                    else
                    {
                        switch (_dir)
                        {
                            case "w":
                                _snake[i].Y -= _stepSize;
                                break;
                            case "s":
                                _snake[i].Y += _stepSize;
                                break;
                            case "a":
                                _snake[i].X -= _stepSize;
                                break;
                            case "d":
                                _snake[i].X += _stepSize;
                                break;
                        }

                        var breite = (_rows * _stepSize);
                        var höhe = (_cols * _stepSize);

                        _snake[i].X += breite;
                        _snake[i].X = _snake[i].X % (_rows * _stepSize);

                        _snake[i].Y += höhe;
                        _snake[i].Y = _snake[i].Y % (_cols * _stepSize);
                    }

                    //Detect collision with body
                    for (int j = 1; j < _snake.Count; j++)
                    {
                        if (_snake[i].X == _snake[j].X &&
                            _snake[i].Y == _snake[j].Y)
                            _Die();
                    }
                    //Detect collision with food piece
                    if (_CollisionFood(_snake[0].Location))
                    {
                        //Add piece to body
                        Piece piece = new Piece
                        {
                            X = _snake[_snake.Count - 1].X,
                            Y = _snake[_snake.Count - 1].Y
                        };
                        _snake.Add(piece);

                        //Update score
                        _score += 1;
                        lblScore.Text = "Score: " + _score.ToString();

                        //new food is needed
                        _FoodPos();
                    }
                }
                else
                {
                    //Move body
                    _snake[i].X = _snake[i - 1].X;  //Go through list and apply position from 2nd piece to 3rd piece
                    _snake[i].Y = _snake[i - 1].Y;  //and so on
                }
            }
        }
        private void _pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            if(_gameOver == false)
            {
                //Draw gamefied
                for (int i = 0; i < _snake.Count; i++)
                {
                    //Draw snake
                    Brush snakeColor;
                    if (i == 0)
                        snakeColor = Brushes.Black; //Head
                    else
                        snakeColor = Brushes.ForestGreen; //Rest of the body

                    canvas.FillRectangle(snakeColor, new Rectangle(_snake[i].X,
                                                                   _snake[i].Y,
                                                                   _stepSize, _stepSize)); //Set the color of the pieces in the list

                    //Draw food
                    canvas.FillEllipse(Brushes.Red, new Rectangle(_food.X, _food.Y, _stepSize, _stepSize));
                }
            }
        }
        private void _Snake_KeyDown(object sender, KeyEventArgs e) //Detect the keyboard inputs
        {
            switch (e.KeyCode)
            {
                //Playing with wasd
                #region wasd 
                case Keys.W:
                    if (_dir != "s")
                        _dir = "w";
                    break;

                case Keys.S:
                    if (_dir != "w")
                        _dir = "s";
                    break;

                case Keys.A:
                    if (_dir != "d")
                        _dir = "a";
                    break;

                case Keys.D:
                    if (_dir != "a")
                        _dir = "d";
                    break;
                #endregion wasd
                //Playing with arrows
                #region Arrows
                case Keys.Up:
                    if (_dir != "s")
                        _dir = "w";
                    break;

                case Keys.Down:
                    if (_dir != "w")
                        _dir = "s";
                    break;

                case Keys.Left:
                    if (_dir != "d")
                        _dir = "a";
                    break;

                case Keys.Right:
                    if (_dir != "a")
                        _dir = "d";
                    break;
                #endregion Arrows

                //Enter for new Game
                case Keys.Enter:
                    if(_gameOver == true)
                        _Init();
                    break;
            }
        }
        private bool _CollisionFood(Point pos)
        {
            return pos == _food.Location;
        }
        private bool _GameOver(Point pos)
        {
            return pos.X < 0 || pos.Y < 0 || pos.X > (_cols-1) * _stepSize || pos.Y > (_rows-1) * _stepSize;
        }
        private void _Die()
        {
            _gameOver = true;
            timer1.Stop();
            lblMessage.Text = "Game Over! \nDein Score ist " + _score + "\n\nDrücke auf Enter \num noch eine \nRunde zu spielen.";
        }
    }

    public class Piece
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point Location { get { return new Point(X, Y); } }

        public Piece()
        {
            X = 0;
            Y = 0;
        }
    }
}
