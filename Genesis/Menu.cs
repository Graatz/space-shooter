using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Genesis
{
    class Menu
    {
        private SpriteFont font;
        public List<String> Options = new List<String>()
        {
            "Start game",
            "Exit"
        };
        public String ActiveOption { get; set; } 
        public int ActiveNumber { get; set; }
        KeyboardState oldState;

        public Menu()
        {
            ActiveOption = Options[0];
        }

        public void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("Fonts/Menu");
        }

        public void Update(GameTime gameTime, Game game)
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S))
            {
                GoDown();
            }
            else if (newState.IsKeyDown(Keys.W) && oldState.IsKeyUp(Keys.W))
            {
                GoUp();
            }
            else if (newState.IsKeyDown(Keys.Space) && ActiveOption.Equals("Start game"))
            {
                Genesis.Paused = false;
            }
            else if (newState.IsKeyDown(Keys.Space) && ActiveOption.Equals("Exit"))
            {
                game.Exit();
            }

            oldState = newState;
        }

        public void GoDown()
        {
            if (ActiveNumber < Options.Count - 1)
            {
                ActiveNumber++;
                ActiveOption = Options[ActiveNumber];
            }
        }

        public void GoUp()
        {
            if (ActiveNumber > 0)
            {
                ActiveNumber--;
                ActiveOption = Options[ActiveNumber];
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(new Color(10, 10, 10, 255));
            spriteBatch.Begin();
            
            for (int i = 0; i < Options.Count; i++)
            {
                if (ActiveOption.Equals(Options[i]))
                    spriteBatch.DrawString(font, "> " + Options[i], new Vector2(30, 30 + i * 50), Color.BlueViolet, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                else
                    spriteBatch.DrawString(font, Options[i], new Vector2(30, 30 + i * 50), Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
        }
    }
}
