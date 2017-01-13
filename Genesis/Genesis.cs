using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Genesis
{
    public class Genesis : Game
    {
        public static KeyboardState oldState;
        public static int Width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int Height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        public static bool Paused = true;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Spawner spawner;
        ParticleEffect ParticleEffect;
        Space space;
        Camera camera;
        GameState gameState;

        public Genesis()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Width;
            graphics.PreferredBackBufferHeight = Height;
            graphics.IsFullScreen = false;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            GameState.GameStates = new Stack<GameState>();
            gameState = new GameState(GameState.GameStates, graphics, Content);
            GameState.GameStates.Push(new Menu(GameState.GameStates, graphics, Content));

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public void StartNewGame()
        {
            ParticleEffect = new ParticleEffect();
            ParticleEffect.LoadContent(Content);

            camera = new Camera();

            space = new Space(camera, 20000, 15000);
            space.LoadContent(Content, GraphicsDevice);
            camera.SetCameraPosition(new Vector2(space.Width / 2 - Width / 2, space.Height / 2 - Height / 2));

            player = new Player(camera, space, ParticleEffect, new Vector2(space.Width / 2, space.Height / 2), 0.3f, 0f, 0f, Color.White);
            player.LoadContent(Content);

            spawner = new Spawner(ParticleEffect, space, player, camera);
            spawner.LoadContent(Content);
            spawner.SpawnEnemies(50);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();
            if (Paused)
            {
                gameState.Update(gameTime, this, this);
            }
            else
            {
                if (newState.IsKeyDown(Keys.Escape) && Genesis.oldState.IsKeyUp(Keys.Escape) && Genesis.Paused == false)
                    Paused = true;

                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                    ParticleEffect.GenerateParticles(50, new Vector2(Mouse.GetState().Position.X + camera.Position.X, Mouse.GetState().Position.Y + camera.Position.Y), ParticleEffect.textures[0]);

                player.Update(gameTime);
                spawner.Update(gameTime);
                ParticleEffect.Update(gameTime);
                space.Update(gameTime);
            }
            Genesis.oldState = newState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (Paused)
                gameState.Draw(spriteBatch, GraphicsDevice);
            else
            {
                GraphicsDevice.Clear(Color.Black);
                space.Draw(spriteBatch, GraphicsDevice);
                player.Draw(spriteBatch, GraphicsDevice);
                spawner.Draw(spriteBatch, GraphicsDevice);
                ParticleEffect.Draw(spriteBatch, GraphicsDevice, camera);
            }
            base.Draw(gameTime);
        }
    }
}
