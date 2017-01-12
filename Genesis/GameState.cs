using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Genesis
{
    class GameState
    {
        public static Stack<GameState> GameStates { get; set; }

        public struct CurrentOptions
        {
            public List<String> AvailableOptions { get; set; }
            public String ActiveOption { get; set; }

            private int activeOptionNumber;
            public int ActiveOptionNumber
            {
                get
                {
                    return activeOptionNumber;
                }
                set
                {
                    activeOptionNumber = value;
                    ActiveOption = AvailableOptions[activeOptionNumber];
                }
            }

            public CurrentOptions(List<String> availableOptions)
            {
                AvailableOptions = availableOptions;
                ActiveOption = AvailableOptions[0];
                activeOptionNumber = 0;
            }
        }

        protected ContentManager Content { get; set; }
        protected GraphicsDeviceManager Graphics { get; set; }
        protected SpriteFont Font { get; set; }
        protected Color HighlightColor { get; set; }

        public GameState(Stack<GameState> gameStates, GraphicsDeviceManager graphics, ContentManager content)
        {
            GameStates = gameStates;
            Graphics = graphics;
            Content = content;
            HighlightColor = Color.Cyan;

            LoadContent(Content);
        }

        private void LoadContent(ContentManager Content)
        {
            Font = Content.Load<SpriteFont>("Fonts/Menu");
        }

        public virtual void Update(GameTime gameTime, Game game, Genesis genesis)
        {
            GameStates.Peek().Update(gameTime, game, genesis);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            GameStates.Peek().Draw(spriteBatch, graphicsDevice);
        }
    }
}
