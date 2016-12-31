using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Genesis
{
    class Options : GameState
    {
        private CurrentOptions CurrentOptions;
        public void Initialize()
        {
            List<String> availableOptions = new List<String>()
            {
                "Resolution",
                "Fullscreen",
                "Apply",
                "Back"
            };

            CurrentOptions = new CurrentOptions(availableOptions);

            Fullscreen = Graphics.IsFullScreen;
            List<Resolution> availableResolutions = new List<Resolution>()
            {
                new Resolution(1366, 768),
                new Resolution(1440, 900),
                new Resolution(1600, 900),
                new Resolution(1920, 1080),
                new Resolution(2256, 1080)
            };

            Resolutions = new List<Resolution>();
            for (int i = 0; i < availableResolutions.Count; i++)
            {
                if (availableResolutions[i].Width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width && 
                    availableResolutions[i].Height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
                    Resolutions.Add(availableResolutions[i]);
            }

            DetectResolution();
            GameState.GameStates.Push(this);
        }

        struct Resolution
        {
            public int Width;
            public int Height;

            public Resolution(int width, int height)
            {
                Width = width;
                Height = height;
            }
        }
        private List<Resolution> Resolutions { get; set; }
        private String ActiveResolution { get; set; }
        private int activeResolutionNumber;
        public int ActiveResolutionNumber
        {
            get
            {
                return activeResolutionNumber;
            }
            set
            {
                activeResolutionNumber = value;
                ActiveResolution = Resolutions[activeResolutionNumber].Width + "x" + Resolutions[activeResolutionNumber].Height;
            }
        }

        private bool Fullscreen { get; set; }
        private KeyboardState oldState { get; set; }

        public Options(Stack<GameState> gameStates, GraphicsDeviceManager graphics, ContentManager content) : base (gameStates, graphics, content)
        {
            Initialize();
        }

        public void DetectResolution()
        {
            for (int i = 0; i < Resolutions.Count; i++)
            {
                if (Resolutions[i].Width == Graphics.PreferredBackBufferWidth &&
                    Resolutions[i].Height == Graphics.PreferredBackBufferHeight)
                {
                    ActiveResolutionNumber = i;
                }
            }
        }

        public override void Update(GameTime gameTime, Game game)
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
            else if (newState.IsKeyDown(Keys.D) && oldState.IsKeyUp(Keys.D) && CurrentOptions.ActiveOptionNumber ==  0)
            {
                if (ActiveResolutionNumber < Resolutions.Count - 1)
                {
                    ActiveResolutionNumber++;
                }
            }
            else if (newState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A) && CurrentOptions.ActiveOptionNumber == 0)
            {
                if (ActiveResolutionNumber > 0)
                {
                    ActiveResolutionNumber--;
                }
            }
            else if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space) && CurrentOptions.ActiveOptionNumber == 1)
            {
                Fullscreen = !Fullscreen;
            }
            else if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space) && CurrentOptions.ActiveOptionNumber == 2)
            {
                if (Fullscreen && !Graphics.IsFullScreen)
                    Graphics.IsFullScreen = true;
                else if (!Fullscreen && Graphics.IsFullScreen)
                    Graphics.IsFullScreen = false;

                if (Graphics.PreferredBackBufferWidth != Resolutions[ActiveResolutionNumber].Width || 
                    Graphics.PreferredBackBufferHeight != Resolutions[ActiveResolutionNumber].Height)
                {
                    Graphics.PreferredBackBufferWidth = Resolutions[ActiveResolutionNumber].Width;
                    Graphics.PreferredBackBufferHeight = Resolutions[ActiveResolutionNumber].Height;
                    Genesis.Width = Resolutions[ActiveResolutionNumber].Width;
                    Genesis.Height = Resolutions[ActiveResolutionNumber].Height;
                }
                
                Graphics.ApplyChanges();
            }
            else if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space) && CurrentOptions.ActiveOptionNumber == 3)
            {
                GameState.GameStates.Pop();
            }

            oldState = newState;
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

            for (int i = 0; i < CurrentOptions.AvailableOptions.Count; i++)
            {
                String optionString = CurrentOptions.AvailableOptions[i];
                if (i == 0)
                {
                    optionString += " : " + ActiveResolution;
                }
                else if (i == 1)
                {
                    if (Fullscreen)
                        optionString += " : ON";
                    else
                        optionString += " : OFF";
                }

                if (CurrentOptions.ActiveOption.Equals(CurrentOptions.AvailableOptions[i]))
                    spriteBatch.DrawString(Font, "> " + optionString, new Vector2(30, 30 + i * 50), HighlightColor, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                else 
                    spriteBatch.DrawString(Font, optionString, new Vector2(30, 30 + i * 50), Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
        }
    }
}
