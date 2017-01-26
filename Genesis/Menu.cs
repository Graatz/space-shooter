using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Genesis
{
    class Menu : IGameState
    {
        private GameStateHandler.CurrentOptions CurrentOptions;
        public void Initialize()
        {
            List<String> availableOptions = new List<String>()
            {
                "START GAME",
                "GENERATE NEW SPACE",
                "OPTIONS",
                "EXIT"
            };

            CurrentOptions = new GameStateHandler.CurrentOptions(availableOptions);
        }

        KeyboardState oldState { get; set; }
        public Menu()
        {
            GameStateHandler.FadeEffect.Activate();
            Initialize();
        }

        public void Update(GameTime gameTime, Game game, Genesis genesis)
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.S) && Genesis.oldState.IsKeyUp(Keys.S))
            {
                GoDown();
            }
            else if (newState.IsKeyDown(Keys.W) && Genesis.oldState.IsKeyUp(Keys.W))
            {
                GoUp();
            }
            else if (newState.IsKeyDown(Keys.Space) && Genesis.oldState.IsKeyUp(Keys.Space) && CurrentOptions.ActiveOption.Equals("START GAME"))
            {
                genesis.StartGame();
            }
            else if (newState.IsKeyDown(Keys.Space) && Genesis.oldState.IsKeyUp(Keys.Space) && CurrentOptions.ActiveOption.Equals("GENERATE NEW SPACE"))
            {
                genesis.GenerateSpace();
                genesis.StartGame();
            }
            else if (newState.IsKeyDown(Keys.Space) && Genesis.oldState.IsKeyUp(Keys.Space) && CurrentOptions.ActiveOption.Equals("OPTIONS"))
            {
                Options options = new Options();
                CurrentOptions.ActiveOptionNumber = 0;
            }
            else if (newState.IsKeyDown(Keys.Space) && Genesis.oldState.IsKeyUp(Keys.Space) && CurrentOptions.ActiveOption.Equals("EXIT"))
            {
                game.Exit();
            }
            else if (newState.IsKeyDown(Keys.Escape) && Genesis.oldState.IsKeyUp(Keys.Escape) && GameStateHandler.Paused == true)
            {
                game.Exit();
            }

            Genesis.oldState = newState;
        }

        public void GoDown()
        {
            if (CurrentOptions.ActiveOptionNumber < CurrentOptions.AvailableOptions.Count - 1)
            {
                CurrentOptions.ActiveOptionNumber++;
            }
        }

        public void GoUp()
        {
            if (CurrentOptions.ActiveOptionNumber > 0)
            {
                CurrentOptions.ActiveOptionNumber--;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin();
            
            for (int i = 0; i < CurrentOptions.AvailableOptions.Count; ++i)
            {
                if (CurrentOptions.ActiveOption.Equals(CurrentOptions.AvailableOptions[i]))
                    spriteBatch.DrawString(GameStateHandler.MenuFont, CurrentOptions.AvailableOptions[i], new Vector2(Genesis.Width / 2 - 50, Genesis.Height / 2 + i * 50 - CurrentOptions.AvailableOptions.Count * 30), GameStateHandler.HighlightColor, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                else
                    spriteBatch.DrawString(GameStateHandler.MenuFont, CurrentOptions.AvailableOptions[i], new Vector2(Genesis.Width / 2 - 50, Genesis.Height / 2 + i * 50 - CurrentOptions.AvailableOptions.Count * 30), Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
        }
    }
}
