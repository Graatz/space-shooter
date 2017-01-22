using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Genesis
{
    class Menu : GameState
    {
        private CurrentOptions CurrentOptions;
        public void Initialize()
        {
            List<String> availableOptions = new List<String>()
            {
                "NEW GAME",
                "OPTIONS",
                "EXIT"
            };

            CurrentOptions = new CurrentOptions(availableOptions);
        }

        KeyboardState oldState { get; set; }
        public Menu(Stack<GameState> gameStates, GraphicsDeviceManager graphics, ContentManager content) : base(gameStates, graphics, content)
        {
            Initialize();
        }

        public override void Update(GameTime gameTime, Game game, Genesis genesis)
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
            else if (newState.IsKeyDown(Keys.Space) && Genesis.oldState.IsKeyUp(Keys.Space) && CurrentOptions.ActiveOption.Equals("NEW GAME"))
            {
                genesis.StartNewGame();
                Genesis.Paused = false;
            }
            else if (newState.IsKeyDown(Keys.Space) && Genesis.oldState.IsKeyUp(Keys.Space) && CurrentOptions.ActiveOption.Equals("OPTIONS"))
            {
                Options options = new Options(GameState.GameStates, Graphics, Content);
                CurrentOptions.ActiveOptionNumber = 0;
            }
            else if (newState.IsKeyDown(Keys.Space) && Genesis.oldState.IsKeyUp(Keys.Space) && CurrentOptions.ActiveOption.Equals("EXIT"))
            {
                game.Exit();
            }
            else if (newState.IsKeyDown(Keys.Escape) && Genesis.oldState.IsKeyUp(Keys.Escape) && Genesis.Paused == true)
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

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(new Color(10, 10, 10, 255));
            spriteBatch.Begin();
            
            for (int i = 0; i < CurrentOptions.AvailableOptions.Count; ++i)
            {
                if (CurrentOptions.ActiveOption.Equals(CurrentOptions.AvailableOptions[i]))
                    spriteBatch.DrawString(Font, CurrentOptions.AvailableOptions[i], new Vector2(Genesis.Width / 2 - 50, Genesis.Height / 2 + i * 50 - CurrentOptions.AvailableOptions.Count * 30), HighlightColor, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                else
                    spriteBatch.DrawString(Font, CurrentOptions.AvailableOptions[i], new Vector2(Genesis.Width / 2 - 50, Genesis.Height / 2 + i * 50 - CurrentOptions.AvailableOptions.Count * 30), Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
        }
    }
}
