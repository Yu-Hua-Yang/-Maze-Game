using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Input;
using NLog;

namespace MazeGame
{
    public class PlayerSprite : DrawableGameComponent
    {
        Game game;
        Map map;
        private SpriteBatch _spriteBatch;
        Texture2D playerTexture;
        Texture2D pathTexture;
        public bool hasDrawnPlayer;
        public Player player;
        private Vector2 _previousPosition;
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public PlayerSprite(Game game, Map map) : base(game)
        {
            this.game = game;
            this.map = map;
            player = (Player)map.Player;
            hasDrawnPlayer = true;
            setPreviousPosition();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = game.Content.Load<Texture2D>("player");
            pathTexture = game.Content.Load<Texture2D>("path");
        }

        public override void Update(GameTime gameTime)
        {
            // Add player-specific update logic here
            InputManager.Instance.Update();
            base.Update(gameTime);
        }

        public void setPreviousPosition()
        {
            _previousPosition = new Vector2(map.Player.Position.X * 32, map.Player.Position.Y * 32);
        }

        public override void Draw(GameTime gameTime)
        {
            if (hasDrawnPlayer)
            {
                _spriteBatch.Begin();
                Vector2 playerPosition = new Vector2(map.Player.Position.X * playerTexture.Width + 16, map.Player.Position.Y * playerTexture.Height + 16);
                _spriteBatch.Draw(pathTexture, _previousPosition, new Rectangle(0, 0, 32, 32), Color.White);

                _spriteBatch.Draw(playerTexture, playerPosition, new Rectangle(0, 0, 32, 32), Color.White, player.GetRotation(), new Vector2(16, 16), 1f, SpriteEffects.None, DrawOrder);

                _spriteBatch.End();
                setPreviousPosition();

                logger.Debug("Sprite Has been drawn at position X: " + map.Player.Position.X + " Y: " + map.Player.Position.Y);
                hasDrawnPlayer = false;
            }
            
            base.Draw(gameTime);
        }
    }
}
