using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class Snake : Form
    {
        private Random _rand = new Random();

        private int _rows = 10, _cols = 10, _stepSize = 10, _score;
        private bool _gameOver = false;
        private string _dir;

        private bool _AI = true;
        private Point[] _hameltonien = new Point[100];
        private int pos = 0;

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

            //Initialization
            fillHameltonien();
            _SnakeStartPoint();
            _StartDirection();
            _FoodPos();
            timer1.Start();
        }
        private void fillHameltonien()
        {
            _hameltonien[0] = new Point(0, 0);
            _hameltonien[1] = new Point(0, 1);
            _hameltonien[2] = new Point(0, 2);
            _hameltonien[3] = new Point(0, 3);
            _hameltonien[4] = new Point(0, 4);
            _hameltonien[5] = new Point(0, 5);
            _hameltonien[6] = new Point(0, 6);
            _hameltonien[7] = new Point(0, 7);
            _hameltonien[8] = new Point(0, 8);
            _hameltonien[9] = new Point(0, 9);

            _hameltonien[10] = new Point(1, 9);
            _hameltonien[11] = new Point(1, 8);
            _hameltonien[12] = new Point(1, 7);
            _hameltonien[13] = new Point(1, 6);
            _hameltonien[14] = new Point(1, 5);
            _hameltonien[15] = new Point(1, 4);
            _hameltonien[16] = new Point(1, 3);
            _hameltonien[17] = new Point(1, 2);
            _hameltonien[18] = new Point(1, 1);
            _hameltonien[19] = new Point(2, 1);

            _hameltonien[20] = new Point(2, 2);
            _hameltonien[21] = new Point(2, 3);
            _hameltonien[22] = new Point(2, 4);
            _hameltonien[23] = new Point(2, 5);
            _hameltonien[24] = new Point(2, 6);
            _hameltonien[25] = new Point(2, 7);
            _hameltonien[26] = new Point(2, 8);
            _hameltonien[27] = new Point(2, 9);
            _hameltonien[28] = new Point(3, 9);
            _hameltonien[29] = new Point(3, 8);

            _hameltonien[30] = new Point(3, 7);
            _hameltonien[31] = new Point(3, 6);
            _hameltonien[32] = new Point(3, 5);
            _hameltonien[33] = new Point(3, 4);
            _hameltonien[34] = new Point(3, 3);
            _hameltonien[35] = new Point(3, 2);
            _hameltonien[36] = new Point(3, 1);
            _hameltonien[37] = new Point(4, 1);
            _hameltonien[38] = new Point(4, 2);
            _hameltonien[39] = new Point(4, 3);
                                         
            _hameltonien[40] = new Point(4, 4);
            _hameltonien[41] = new Point(4, 5);
            _hameltonien[42] = new Point(4, 6);
            _hameltonien[43] = new Point(4, 7);
            _hameltonien[44] = new Point(4, 8);
            _hameltonien[45] = new Point(4, 9);
            _hameltonien[46] = new Point(5, 9);
            _hameltonien[47] = new Point(5, 8);
            _hameltonien[48] = new Point(5, 7);
            _hameltonien[49] = new Point(5, 6);
                                         
            _hameltonien[50] = new Point(5, 5);
            _hameltonien[51] = new Point(5, 4);
            _hameltonien[52] = new Point(5, 3);
            _hameltonien[53] = new Point(5, 2);
            _hameltonien[54] = new Point(5, 1);
            _hameltonien[55] = new Point(6, 1);
            _hameltonien[56] = new Point(6, 2);
            _hameltonien[57] = new Point(6, 3);
            _hameltonien[58] = new Point(6, 4);
            _hameltonien[59] = new Point(6, 5);

            _hameltonien[60] = new Point(6, 6);
            _hameltonien[61] = new Point(6, 7);
            _hameltonien[62] = new Point(6, 8);
            _hameltonien[63] = new Point(6, 9);
            _hameltonien[64] = new Point(7, 9);
            _hameltonien[65] = new Point(7, 8);
            _hameltonien[66] = new Point(7, 7);
            _hameltonien[67] = new Point(7, 6);
            _hameltonien[68] = new Point(7, 5);
            _hameltonien[69] = new Point(7, 4);

            _hameltonien[70] = new Point(7, 3);
            _hameltonien[71] = new Point(7, 2);
            _hameltonien[72] = new Point(7, 1);
            _hameltonien[73] = new Point(8, 1);
            _hameltonien[74] = new Point(8, 2);
            _hameltonien[75] = new Point(8, 3);
            _hameltonien[76] = new Point(8, 4);
            _hameltonien[77] = new Point(8, 5);
            _hameltonien[78] = new Point(8, 6);
            _hameltonien[79] = new Point(8, 7);

            _hameltonien[80] = new Point(8, 8);
            _hameltonien[81] = new Point(8, 9);
            _hameltonien[82] = new Point(9, 9);
            _hameltonien[83] = new Point(9, 8);
            _hameltonien[84] = new Point(9, 7);
            _hameltonien[85] = new Point(9, 6);
            _hameltonien[86] = new Point(9, 5);
            _hameltonien[87] = new Point(9, 4);
            _hameltonien[88] = new Point(9, 3);
            _hameltonien[89] = new Point(9, 2);
                                         
            _hameltonien[90] = new Point(9, 1);
            _hameltonien[91] = new Point(9, 0);
            _hameltonien[92] = new Point(8, 0);
            _hameltonien[93] = new Point(7, 0);
            _hameltonien[94] = new Point(6, 0);
            _hameltonien[95] = new Point(5, 0);
            _hameltonien[96] = new Point(4, 0);
            _hameltonien[97] = new Point(3, 0);
            _hameltonien[98] = new Point(2, 0);
            _hameltonien[99] = new Point(1, 0);
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
            for (int i = 0; i < _snake.Count; i++)
            {
                if (_food.Location == _snake[i].Location)
                    _FoodPos();
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
                        if (pos > 99)
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
