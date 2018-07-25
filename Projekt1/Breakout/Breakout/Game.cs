using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp2
{
    public class Game
    {
        private const double MS_PER_UPDATE = 8;
        private double _lag;

        private RenderWindow _window;
        private Font _font;

        private PersistenceManager _persistenceManager;

        private CircleObject _circleObject;
        private double _circleSize = 10;
        private Vector2D _circlePosition;

        private Ai _aiLeft;
        private Ai _aiRight;

        private List<RectangleObject> _bricks = new List<RectangleObject>();
        private List<RectangleObject> _bricksPrefab = new List<RectangleObject>();
        private RectangleObject _player1 = null;
        private RectangleObject _player2 = null;
        private RectangleObject _lastHit = null;
        private double _rectangleSizeX = 100;
        private double _rectangleSizeY = 10;
        private double _firstRectanglePositionY;
        private double _secondRectanglePositionY;
        private Vector2D _brickDistance = new Vector2D(20, 20);
        private Vector2D _brickPosition = new Vector2D(0, 0);
        private Vector2i _brickCount = new Vector2i(7, 5);
        private double _brickSizeX = 100;
        private double _brickSizeY = 10;

        private List<Text> _lifeImages = new List<Text>();
        private Vector2D _lifeImagesPosition = new Vector2D(10, 10);
        private Vector2D _lifeImagesDistance = Vector2D.Right() * 10;
        private double _lifeImagesSize = 60;
        
        private Text _scoreLeftText;
        private Text _scoreRightText;
        private uint _characterSizeScore = 100;
        private List<RectangleObject> _players = new List<RectangleObject>();

        private int _score1 = 0;
        private int _score2 = 0;

        private int _probabilityOfPowerUp = 3;

        private Stopwatch _stopwatch = new Stopwatch();

        private AnimationManager _animationManager = new AnimationManager();


        private SoundManager _soundManager = new SoundManager();
        private StateManager _stateManager;

        private List<PowerUp> _powerUps = new List<PowerUp>();
        private List<PowerUp> _prefabPowerUps = new List<PowerUp>();

        private Random _random = new Random();

        private String _loadedLevel;
        private int _levelID;


        public Game(RenderWindow window, Font font)
        {
            _window = window;
            _font = font;
            MessageBus.RegisterSM(_soundManager);
        }

        public void Start(Ai.Difficulty difficultyLeft, Ai.Difficulty difficultyRight)
        {
            ReInitialisePlayerAndAI(difficultyLeft, difficultyRight);
            Initialise();
        }

        public void Run(Ai.Difficulty difficultyLeft, Ai.Difficulty difficultyRight)
        { 
            if (difficultyLeft != _aiLeft.AIDifficulty || difficultyRight != _aiRight.AIDifficulty)
            {
                ReInitialisePlayerAndAI(difficultyLeft, difficultyRight);
            }
            

            
            double elapsedTime = (double)_stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000;

            _lag += elapsedTime;
            
            _stopwatch.Restart();
                
            Input(elapsedTime);
            while (_lag >= MS_PER_UPDATE)
            {
                Update(MS_PER_UPDATE);
                _lag -= MS_PER_UPDATE;
            }
            Redraw();
                                
            _stopwatch.Stop();
            
        }


        private void ReInitialisePlayerAndAI(Ai.Difficulty difficultyLeft, Ai.Difficulty difficultyRight)
        {
            _firstRectanglePositionY = Program.windowSize.Y - _rectangleSizeY / 2 - 50;
            _secondRectanglePositionY = _firstRectanglePositionY + _rectangleSizeY * 2;

            if (difficultyLeft != Ai.Difficulty.None)
            {
                _player1 = new RectangleObject(new Vector2D(_rectangleSizeX, _rectangleSizeY), 0);
                _player1.Rectangle.Position = new Vector2D(Program.windowSize.X / 2, _firstRectanglePositionY);
            }
            else
            {
                _player1 = null;
            }

            if (difficultyRight != Ai.Difficulty.None)
            {
                _player2 = new RectangleObject(new Vector2D(_rectangleSizeX, _rectangleSizeY), 3);
                if (_player1 == null)
                    _player2.Rectangle.Position = new Vector2D(Program.windowSize.X / 2, _firstRectanglePositionY);
                else
                    _player2.Rectangle.Position = new Vector2D(Program.windowSize.X / 2, _secondRectanglePositionY);

            }
            else
            {
                _player2 = null;
            }

            _aiLeft = new Ai(_player1, _circleObject, Program.windowSize, difficultyLeft);
            _aiRight = new Ai(_player2, _circleObject, Program.windowSize, difficultyRight);

            _players.Clear();
            _players.Add(_player1);
            _players.Add(_player2);
        }


        private void Initialise()
        {

            _brickPosition = new Vector2D(Program.windowSize.X / 2, Program.windowSize.Y / 4);

            _circlePosition = new Vector2D(Program.windowSize.X / 2, Program.windowSize.Y / 2);

            _lag = 0.0;
            
            _circleObject = new CircleObject(_circleSize);
            _circleObject.Circle.FillColor = Color.White;

            _levelID = 0;
            _loadedLevel = _persistenceManager.LoadLevelList()[_levelID++];
            _bricks = _persistenceManager.LoadLevel(_loadedLevel);
            _bricksPrefab = _persistenceManager.LoadLevel(_loadedLevel);

            for (int i = 0; i < _bricks.Count; i++)
            {
                _bricks[i].Rectangle.Position += _brickPosition;
                _bricksPrefab[i].Rectangle.Position += _brickPosition;
            }
            
            
            /*for (int i = 0; i < _brickCount.X; i++)
            {
                for (int j = 0; j < _brickCount.Y; j++)
                {
                    RectangleObject newBrick = new RectangleObject(new Vector2D(_brickSizeX, _brickSizeY));
                    newBrick.Rectangle.FillColor = Color.White;
                    
                    //double positionX = _brickPosition.X + i * (_brickSizeX + _brickDistance.X);
                    double positionX = i * (_brickSizeX + _brickDistance.X);
                    positionX -= (_brickCount.X / 2) * (_brickSizeX + _brickDistance.X) ;
                    //double positionY = _brickPosition.Y + j * (_brickSizeY + _brickDistance.Y);
                    double positionY = j * (_brickSizeY + _brickDistance.Y);
                    positionY -= (_brickCount.Y / 2) * (_brickSizeY + _brickDistance.Y);
                    
                    newBrick.Rectangle.Position = new Vector2D(positionX, positionY);

                    if (j % 2 == 0)
                    {
                        newBrick.Life = 0;
                    }

                    newBrick.ChangeToPrefab(RectangleObject.rectangleShapePrefabs[newBrick.Life]);

                    _bricks.Add(newBrick);
                }
            }
            _persistenceManager.SaveLevel("level1",_bricks);
            foreach (RectangleObject brick in _bricks)
            {
                brick.Rectangle.Position += _brickPosition;
            }*/


            List<State> states = new List<State>();
            states.Add(new Disco(_circleObject));
            states.Add(new Disco(_circleObject));
            states.Add(new SpeedDown(_circleObject));
            states.Add(new BiggerRectangleObject(_circleObject));
            states.Add(new BiggerRectangleObject(_circleObject));
            //_prefabPowerUps.Add(new SpeedDown(_circleObject));
            //_prefabPowerUps.Add(new BiggerRectangleObject(_circleObject));
            //_prefabPowerUps.Add(new BiggerRectangleObject(_circleObject));
            //_prefabPowerUps.Add(new Disco(_circleObject));
            //_prefabPowerUps.Add(new Disco(_circleObject));

            
            _players.Add(_player1);
            _players.Add(_player2);

            _stateManager = new StateManager(states, _players);
                

            /*PowerUp powerUp1 = new PowerUp(states[0]);
            powerUp1.StateManager = _stateManager;

            _prefabPowerUps.Add(powerUp1);*/

            for (int i = 0; i < states.Count; i++)
            {
                states[i].ID = i;
                
                PowerUp powerUp = new PowerUp(states[i]);
                powerUp.StateManager = _stateManager;

                _prefabPowerUps.Add(powerUp);
            }


            _circleObject.Circle.Position = _circlePosition;
            _circleObject.Update(0);



            _scoreLeftText = new Text(_score1.ToString(), _font, _characterSizeScore);
            _scoreLeftText.Position = new Vector2D(100, Program.windowSize.Y - _scoreLeftText.GetGlobalBounds().Height - 100);
            _scoreLeftText.OutlineThickness = 2;
            _scoreLeftText.OutlineColor = Color.White;
            _scoreLeftText.FillColor = Color.Black;

            _scoreRightText = new Text(_score2.ToString(), _font, _characterSizeScore);
            _scoreRightText.Position = new Vector2D(Program.windowSize.X - _scoreRightText.GetGlobalBounds().Width - 100, Program.windowSize.Y - _scoreRightText.GetGlobalBounds().Height - 100);


            
        }

        private void Input(double elapsedTime)
        {
            if (_player1 != null)
                RectangleMovement(_player1, Keyboard.Key.A, Keyboard.Key.D, elapsedTime);
            if (_player2 != null)
                RectangleMovement(_player2, Keyboard.Key.Left, Keyboard.Key.Right, elapsedTime);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                if (!Program.isEscapePressed)
                {
                    Program.isEscapePressed = true;
                    Program.windowState = Program.WindowState.MainMenu;
                }
            }
            else
            {
                Program.isEscapePressed = false;
            }
        }

        private void RectangleMovement(RectangleObject rs, Keyboard.Key left, Keyboard.Key right, double elapsedTime)
        {
            if (Keyboard.IsKeyPressed(left))
            {
                rs.RectangleMovementLeft(elapsedTime);
            }
            else if (Keyboard.IsKeyPressed(right))
            {
                rs.RectangleMovementRight(elapsedTime, Program.windowSize.X);
            }
        }

        private void Update(double elapsedTime)
        {

            int lifeImagesCount = _lifeImages.Count;

            for (int i = lifeImagesCount; i < Program.lifes; i++)
            {

                Text lifeImage = new Text("|", _font, (uint)_lifeImagesSize);
                MainMenu.SetTextOriginToMiddle(lifeImage);
                lifeImage.Position = _lifeImagesPosition + (_lifeImagesDistance + Vector2D.Right() * lifeImage.GetGlobalBounds().Width) * i + (Vector2D.Right() * lifeImage.GetGlobalBounds().Width) / 2;
                _lifeImages.Add(lifeImage);
            }

            bool rectangleObject1Collided = false;
            bool rectangleObject2Collided = false;

            if (_player1 != null)
            {
                rectangleObject1Collided = _player1.PongCollision(_circleObject);
            }
            if (_player2 != null)
            {
                rectangleObject2Collided = _player2.PongCollision(_circleObject);
            }

            int numberOfBrickCollisions = 0;
            List<RectangleObject> collidedBricks = new List<RectangleObject>();

            Vector2D oldCircleDirection = _circleObject.Direction;

            foreach (RectangleObject brick in _bricks)
            {
                if (brick.NewReflectionCollision(_circleObject, _animationManager))
                {
                    brick.Life--;

                    if (brick.Life >= 0)
                    {
                        brick.ChangeToPrefab(RectangleObject.rectangleShapePrefabs[brick.Life]);
                    }

                    numberOfBrickCollisions++;
                    collidedBricks.Add(brick);

                    Vector2D originalPosition = Vector2D.Zero();

                    foreach (RectangleObject brickPrefab in _bricksPrefab)
                    {
                        if (brickPrefab.ID == brick.ID)
                        {
                            originalPosition = brickPrefab.Rectangle.Position;
                        }
                    }

                    _animationManager.RemoveAnimationsToObject(brick.Rectangle);
                    
                    Vector2D animationPosition = brick.Rectangle.Position + oldCircleDirection * 5;
                    _animationManager.AddAnimation(new Animation(brick.Rectangle.Position, new Transformable[] { brick.Rectangle }, 200, 0, animationPosition));
                    _animationManager.AddAnimation(new Animation(animationPosition, new Transformable[] { brick.Rectangle }, 200, 200, originalPosition));


                }
            }

            if (numberOfBrickCollisions > 0)
            {
                if (_player1 != null && _player2 != null)
                {
                    if (_lastHit.Equals(_player1))
                    {
                        _score1 += numberOfBrickCollisions;
                    }

                    if (_lastHit.Equals(_player2))
                    {
                        _score2 += numberOfBrickCollisions;
                    }
                }
                else
                {
                    if (_player1 != null)
                    {
                        _score1++;
                    }

                    if (_player2 != null)
                    {
                        _score2++;
                    }
                }

                foreach (RectangleObject brick in collidedBricks)
                {
                    if (brick.Life < 0)
                    {
                        if (_random.Next(0, _probabilityOfPowerUp) == 0)
                        {
                            PowerUp newPowerUp = _prefabPowerUps[_random.Next(0, _prefabPowerUps.Count)].Clone();


                            double speed = 0.3f;
                            newPowerUp.Spawn(new Vector2D(brick.Rectangle.Position.X, brick.Rectangle.Position.Y), new Vector2D(0, 1), speed);

                            _powerUps.Add(newPowerUp);

                        }

                        _bricks.Remove(brick);
                    }
                }
            }
            

            if (_bricks.Count == 0 && _loadedLevel == _persistenceManager.LoadLevelList()[_persistenceManager.LoadLevelList().Count-1])
            {
                Program.windowState = Program.WindowState.GameOver;
            }else if(_bricks.Count == 0 && (rectangleObject1Collided || rectangleObject2Collided))
            {
                _loadedLevel = _persistenceManager.LoadLevelList()[_levelID++];
                _bricks = _persistenceManager.LoadLevel(_loadedLevel);
                _bricksPrefab = _persistenceManager.LoadLevel(_loadedLevel);
                for (int i = 0; i < _bricks.Count; i++)
                {
                    _bricks[i].Rectangle.Position += _brickPosition;
                    _bricksPrefab[i].Rectangle.Position += _brickPosition;
                }
            }


            List<PowerUp> usedPowerUps = new List<PowerUp>();

            foreach (PowerUp powerUp in _powerUps)
            {
                powerUp.Update(elapsedTime);

                RectangleObject collidedRectangle = null;
                if (_player1 != null)
                {
                    if (_player1.PowerUpCollision(powerUp))
                        collidedRectangle = _player1;
                }

                if (_player2 != null)
                {
                    if (_player2.PowerUpCollision(powerUp))
                        collidedRectangle = _player2;
                }
                
                if (collidedRectangle != null)
                {
                    powerUp.ExecutePowerUp(collidedRectangle);
                    usedPowerUps.Add(powerUp);
                }
                else if (powerUp.Position.X < 0 || powerUp.Position.X > Program.windowSize.X)
                {
                    usedPowerUps.Add(powerUp);
                }
            }

            foreach (PowerUp powerUp in usedPowerUps)
            {
                _powerUps.Remove(powerUp);
            }

            usedPowerUps.Clear();

            _stateManager.Update(elapsedTime);

            _circleObject.Update(elapsedTime);

            bool resetCircleObject = false;

            if (_circleObject.CheckOutOfField((int)Program.windowSize.X, (int)Program.windowSize.Y))
            {
                Program.lifes--;
                _lifeImages.RemoveAt(_lifeImages.Count - 1);
                if (Program.lifes <= 0)
                {
                    Program.windowState = Program.WindowState.GameOver;
                }

                resetCircleObject = true;
                _circleObject.ResetPosition(_circlePosition);
                _circleObject.Direction = new Vector2D(-_circleObject.Direction.X, _circleObject.Direction.Y);
                _circleObject.Update(0);
                _powerUps.Clear();
            }



            if ((rectangleObject1Collided || rectangleObject2Collided) && _player1 != null && _player2 != null)
            {
                /*Vector2D savedPos = _player1.Rectangle.Position;
                _player1.Rectangle.Position = _player2.Rectangle.Position;
                _player2.Rectangle.Position = savedPos;*/

                _animationManager.AddAnimation(new Animation(_player1.Rectangle.Position, new Transformable[] { _player2.Rectangle }, 100, 0, false));
                _animationManager.AddAnimation(new Animation(_player2.Rectangle.Position, new Transformable[] { _player1.Rectangle }, 100, 0, false));

                if (rectangleObject1Collided)
                {
                    _lastHit = _player1;
                }
                if (rectangleObject2Collided)
                {
                    _lastHit = _player2;
                }
                
            }


            
            _aiLeft.Update(elapsedTime, rectangleObject2Collided, rectangleObject1Collided, resetCircleObject);
            _aiRight.Update(elapsedTime, rectangleObject1Collided, rectangleObject2Collided, resetCircleObject);


            if (_score1 < 10)
                _scoreLeftText.DisplayedString = "0" + _score1.ToString();
            else
                _scoreLeftText.DisplayedString = _score1.ToString();

            if (_score2 < 10)
                _scoreRightText.DisplayedString = "0" + _score2.ToString();
            else
                _scoreRightText.DisplayedString = _score2.ToString();


            _animationManager.Update(elapsedTime);
        }

        private void Redraw()
        {
            _scoreRightText.Position = new Vector2D(Program.windowSize.X - _scoreRightText.GetGlobalBounds().Width - 100, Program.windowSize.Y - _scoreRightText.GetGlobalBounds().Height - 100);

            _window.Clear();
            _window.DispatchEvents();
            _window.Draw(_circleObject.Circle);
            if (_player1 != null)
            {
                _window.Draw(_player1.Rectangle);
                _window.Draw(_scoreLeftText);
            }
            if (_player2 != null)
            {
                _window.Draw(_player2.Rectangle);
                _window.Draw(_scoreRightText);
            }

            foreach (RectangleObject brick in _bricks)
            {
                _window.Draw(brick.Rectangle);
            }

            foreach (PowerUp powerUp in _powerUps)
            {
                _window.Draw(powerUp);
            }

            foreach (Text lifeImage in _lifeImages)
            {
                _window.Draw(lifeImage);
            }

            _window.Display();
        }

        public void SetPM(PersistenceManager persistenceManager)
        {
            _persistenceManager = persistenceManager;
        }

        public List<Text> GetScores()
        {
            List<Text> scores = new List<Text>();

            if (_player1 != null)
            {
                scores.Add(_scoreLeftText);
            }
            if (_player2 != null)
            {
                scores.Add(_scoreRightText);
            }

            return scores;
        }
        
    }
}