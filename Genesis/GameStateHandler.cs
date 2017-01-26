using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Genesis
{
    class GameStateHandler
    {
        private static bool paused = true;
        public static bool Paused
        {
            get
            {
                return paused;
            }
            set
            {
                paused = value;
                FadeEffect.Activate();
            }
        }
        public static Color HighlightColor { get; set; }
        public static SpriteFont MenuFont { get; set; }
        public static Stack<IGameState> GameStates { get; set; }
        public static FadeEffect FadeEffect;

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
        public static GraphicsDeviceManager Graphics { get; set; }

        public GameStateHandler(GraphicsDeviceManager graphics, ContentManager content, FadeEffect fadeEffect)
        {
            FadeEffect = fadeEffect;
            GameStates = new Stack<IGameState>();
            GameStates.Push(new Menu());

            Graphics = graphics;
            Content = content;
            HighlightColor = Color.Cyan;

            LoadContent(Content);
        }

        private void LoadContent(ContentManager Content)
        {
            MenuFont = Content.Load<SpriteFont>("Fonts/Menu");
        }

        public void Update(GameTime gameTime, Game game, Genesis genesis)
        {
            GameStates.Peek().Update(gameTime, game, genesis);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(new Color(10, 10, 10, 255));
            GameStates.Peek().Draw(spriteBatch, graphicsDevice);
        }
    }
}
