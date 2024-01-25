using Maze;
using MazeHuntKill;
using MazeRecursion;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NLog;
using System;
using System.Windows.Forms;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MazeGame
{
    public enum GameState { MainMenu = 0, Playing = 1, HeightSelecting = 2, WidthSelecting = 3}
    public enum LoadState { File = 0, Recursive = 1, HuntAndKill = 2 }
    public class Game1 : Game
    {
        public GameState GameState { get; private set; }
        private LoadState _loadState;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        Texture2D wallTexture;
        Texture2D goalTexture;
        Texture2D pathTexture;

        private bool _hasDrawnMazeAndGoal = false;
        Map map;
        PlayerSprite player;
        string mapFilePath;

        private bool _isFileMode = false;
        private int _selectedWidth;
        private int _selectedHeight;
        private int _defaultWidth;
        private int _defaultHeight;
        SpriteFont font;
        public Game1()
        {
            GameState = GameState.MainMenu;
            _loadState = LoadState.File;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _selectedHeight = 5;
            _selectedWidth = 5;
            _defaultWidth = 800;
            _defaultHeight = 400;
    }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = _defaultWidth;
            _graphics.PreferredBackBufferHeight = _defaultHeight;
            _graphics.ApplyChanges();

            InputKeyInitialization();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            wallTexture = Content.Load<Texture2D>("wall");
            goalTexture = Content.Load<Texture2D>("goal");
            pathTexture = Content.Load<Texture2D>("path");
            font = Content.Load<SpriteFont>("font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            if (map != null)
            {
                if (map.Player.Position.X == map.Goal.X && map.Player.Position.Y == map.Goal.Y)
                {
                    logger.Info("Game Over");
                    GameState = GameState.MainMenu;
                    _hasDrawnMazeAndGoal = false;
                    player = null;
                    map = null;
                    _selectedHeight = 5;
                    _selectedWidth = 5;
                    _graphics.PreferredBackBufferWidth = _defaultWidth;
                    _graphics.PreferredBackBufferHeight = _defaultHeight;
                    _graphics.ApplyChanges();
                }
            }

            if (GameState == GameState.MainMenu || GameState == GameState.HeightSelecting || GameState == GameState.WidthSelecting)
{
                InputManager.Instance.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (GameState == GameState.MainMenu)
            {
                GraphicsDevice.Clear(Color.Black);

                _spriteBatch.Begin();

                _spriteBatch.DrawString(font, "MARIO MAZE MAKER", new Vector2(0, 0), Color.White);
                if (_loadState == LoadState.File)
                {
                    _spriteBatch.DrawString(font, "File Load", new Vector2(0, 60), Color.Red);
                    _spriteBatch.DrawString(font, "Recursive Creation", new Vector2(0, 120), Color.White);
                    _spriteBatch.DrawString(font, "Hunt & Kill", new Vector2(0, 180), Color.White);
                }
                else if (_loadState == LoadState.Recursive)
                {
                    _spriteBatch.DrawString(font, "File Load", new Vector2(0, 60), Color.White);
                    _spriteBatch.DrawString(font, "Recursive Creation", new Vector2(0, 120), Color.Red);
                    _spriteBatch.DrawString(font, "Hunt & Kill", new Vector2(0, 180), Color.White);
                }
                else if (_loadState == LoadState.HuntAndKill)
                {
                    _spriteBatch.DrawString(font, "File Load", new Vector2(0, 60), Color.White);
                    _spriteBatch.DrawString(font, "Recursive Creation", new Vector2(0, 120), Color.White);
                    _spriteBatch.DrawString(font, "Hunt & Kill", new Vector2(0, 180), Color.Red);
                }

                _spriteBatch.End();
            }
            else if (GameState == GameState.HeightSelecting)
            {
                GraphicsDevice.Clear(Color.Black);

                _spriteBatch.Begin();

                _spriteBatch.DrawString(font, "Select Height Up or Down", new Vector2(0, 0), Color.White);
                _spriteBatch.DrawString(font, "Enter to Continue", new Vector2(0, 60), Color.White);
                _spriteBatch.DrawString(font, _selectedHeight.ToString(), new Vector2(0, 120), Color.White);

                _spriteBatch.End();
            }
            else if (GameState == GameState.WidthSelecting)
            {
                GraphicsDevice.Clear(Color.Black);

                _spriteBatch.Begin();

                _spriteBatch.DrawString(font, "Select Width Up or Down", new Vector2(0, 0), Color.White);
                _spriteBatch.DrawString(font, "Enter to Continue", new Vector2(0, 60), Color.White);
                _spriteBatch.DrawString(font, _selectedWidth.ToString(), new Vector2(0, 120), Color.White);

                _spriteBatch.End();
            }
            else if (!_hasDrawnMazeAndGoal)
            {
                logger.Info("The map that was loaded is: " + mapFilePath);
                GraphicsDevice.Clear(Color.CornflowerBlue);
                _spriteBatch.Begin();
                for (int row = 0; row < map.MapGrid.GetLength(1); row++)
                {
                    for (int col = 0; col < map.MapGrid.GetLength(0); col++)
                    {
                        int blockType = (int)map.MapGrid[col, row];

                        Texture2D textureToDraw = (blockType == 0) ? wallTexture : pathTexture;

                        Vector2 position = new Vector2(col * textureToDraw.Width, row * textureToDraw.Height);
                        _spriteBatch.Draw(textureToDraw, position, new Rectangle(0, 0, 32, 32), Color.White);

                    }
                }
                
                Vector2 goalPosition = new Vector2(map.Goal.X * goalTexture.Width, map.Goal.Y * goalTexture.Height);
                _spriteBatch.Draw(goalTexture, goalPosition, new Rectangle(0, 0, 32, 32), Color.White);
                _spriteBatch.End();
                _hasDrawnMazeAndGoal = true;
                logger.Info("Player starts at X: " + map.Player.Position.X + " Y: " + map.Player.Position.Y);
                logger.Info("Goal is spawned at X: " + map.Goal.X + " Y: " + map.Goal.Y);
                logger.Info("Map has been fully Loaded");
            }

            base.Draw(gameTime);
        }

        private void InputKeyInitialization()
        {
            Action UpKeyAction = () =>
            {
                if (GameState == GameState.MainMenu)
                {
                    if (_loadState != LoadState.File) { _loadState--; }
                    logger.Info("Menu Selection Up");
                }
                else if (GameState == GameState.Playing)
                {
                    player.player.MoveForward();
                    player.hasDrawnPlayer = true;
                    logger.Info("Player Moved Forwards to block X: " + map.Player.Position.X + " Y: " + map.Player.Position.Y);
                }
                else if (GameState == GameState.HeightSelecting)
                {
                    _selectedHeight += 2;

                }
                else if (GameState == GameState.WidthSelecting)
                {
                    _selectedWidth += 2;
                }
            };
            InputManager.Instance.AddKeyHandler(Keys.Up, UpKeyAction);

            Action DownKeyAction = () =>
            {
                if (GameState == GameState.MainMenu)
                {
                    if (_loadState != LoadState.HuntAndKill) { _loadState++; }
                    logger.Info("Menu Selection Down");
                }
                else if (GameState == GameState.Playing)
                {
                    player.player.MoveBackward();
                    player.hasDrawnPlayer = true;
                    logger.Info("Player Moved Backwards to block X: " + map.Player.Position.X + " Y: " + map.Player.Position.Y);
                }
                else if (GameState == GameState.HeightSelecting)
                {
                    if (_selectedHeight > 5)
                    {
                        _selectedHeight -= 2;
                    }
                }
                else if (GameState == GameState.WidthSelecting)
                {
                    if (_selectedWidth > 5)
                    {
                        _selectedWidth -= 2;
                    }
                }
            };
            InputManager.Instance.AddKeyHandler(Keys.Down, DownKeyAction);

            Action LeftKeyAction = () =>
            {
                if (GameState == GameState.Playing)
                {
                    player.player.TurnLeft();
                    player.hasDrawnPlayer = true;
                    logger.Info("Player Turned Left");
                }
            };
            InputManager.Instance.AddKeyHandler(Keys.Left, LeftKeyAction);

            Action RightKeyAction = () =>
            {
                if (GameState == GameState.Playing)
                {
                    player.player.TurnRight();
                    player.hasDrawnPlayer = true;
                    logger.Info("Player Turned Right");
                }
            };
            InputManager.Instance.AddKeyHandler(Keys.Right, RightKeyAction);

            Action EnterKeyAction = () =>
            {
                if (GameState == GameState.MainMenu)
                {
                    if (_loadState == LoadState.File)
                    {
                        logger.Info("Menu selection File");
                        using (OpenFileDialog openFileDialog = new OpenFileDialog())
                        {
                            if (openFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                mapFilePath = openFileDialog.FileName;
                                map = new Map(new MazeFromFile.MazeFromFile(mapFilePath));
                                map.CreateMap();
                                player = new PlayerSprite(this, map);
                                Components.Add(player);
                            }
                        }
                        GameState = GameState.Playing;
                        logger.Info("File Loaded");

                        _graphics.PreferredBackBufferWidth = map.Height * 32;
                        _graphics.PreferredBackBufferHeight = map.Width * 32;
                        _graphics.ApplyChanges();
                    }
                    else if (_loadState == LoadState.Recursive)
                    {
                        GameState = GameState.HeightSelecting;
                        logger.Info("Menu selection Recursive");
                        logger.Info("Changed to Height Selection");
                    }
                    else if (_loadState == LoadState.HuntAndKill)
                    {
                        GameState = GameState.HeightSelecting;
                        logger.Info("Menu selection Hunt");
                        logger.Info("Changed to Height Selection");
                    }

                }
                else if (GameState == GameState.HeightSelecting)
                {
                    GameState = GameState.WidthSelecting;
                    logger.Info("Changed to Width Selection");
                }
                else if (GameState == GameState.WidthSelecting)
                {
                    if (_loadState == LoadState.Recursive)
                    {
                        IMapProvider maze = MazeCreationRecursive.CreateMazeRecursion(2);
                        map = new Map(maze);
                        map.CreateMap(_selectedWidth, _selectedHeight);
                        player = new PlayerSprite(this, map);
                        Components.Add(player);

                        GameState = GameState.Playing;
                        logger.Info("Recursive Generated");

                        _graphics.PreferredBackBufferWidth = map.Height * 32;
                        _graphics.PreferredBackBufferHeight = map.Width * 32;
                        _graphics.ApplyChanges();
                    }
                    else if (_loadState == LoadState.HuntAndKill)
                    {
                        IMapProvider maze = MazeCreationHuntKill.CreateMazeHuntKill();
                        map = new Map(maze);
                        map.CreateMap(_selectedWidth, _selectedHeight);
                        player = new PlayerSprite(this, map);
                        Components.Add(player);

                        GameState = GameState.Playing;
                        logger.Info("Hunt Kill Generated");

                        _graphics.PreferredBackBufferWidth = map.Height * 32;
                        _graphics.PreferredBackBufferHeight = map.Width * 32;
                        _graphics.ApplyChanges();
                    }
                }
            };
            InputManager.Instance.AddKeyHandler(Keys.Enter, EnterKeyAction);
        }
    }
}