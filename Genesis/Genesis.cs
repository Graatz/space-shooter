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
        public static KeyboardState newState;
        public static int Width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int Height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Player player;
        private Spawner spawner;
        private ParticleHandler particleHandler;
        private Space space;
        private Camera camera;
        private GameStateHandler gameState;

        public Genesis()
        {
            IsFixedTimeStep = false;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Width;
            graphics.PreferredBackBufferHeight = Height;
            graphics.IsFullScreen = true;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            particleHandler = new ParticleHandler();
            particleHandler.LoadContent(Content);

            gameState = new GameStateHandler(graphics, Content, particleHandler.FadeEffect);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GenerateSpace();
        }

        public void GenerateSpace()
        {
            camera = new Camera();

            space = new Space(camera, 20000, 20000);
            space.LoadContent(Content, GraphicsDevice);

            player = new Player(space, particleHandler, new Vector2(space.Width / 2, space.Height / 2), 0.3f, 0f, 0f, Color.White);
            player.LoadContent(Content);

            spawner = new Spawner(particleHandler, space, player, camera);
            spawner.LoadContent(Content);
        }

        public void StartGame()
        {
            GameStateHandler.Paused = false;
            particleHandler.FadeEffect.Activate();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            newState = Keyboard.GetState();
            if (GameStateHandler.Paused)
            {
                gameState.Update(gameTime, this, this);
            } else if (!GameStateHandler.Paused)
            {
                if (newState.IsKeyDown(Keys.Escape) && Genesis.oldState.IsKeyUp(Keys.Escape) && GameStateHandler.Paused == false)
                {
                    GameStateHandler.Paused = true;
                }

                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                    particleHandler.Destruction.GenerateParticles(10, new Vector2(Mouse.GetState().Position.X + camera.Position.X, Mouse.GetState().Position.Y + camera.Position.Y), particleHandler.ColorTextures["red"]);

                player.Update(gameTime, camera);
                spawner.Update(gameTime);
                space.Update(gameTime);
            }
            particleHandler.Update(gameTime, camera);
            Genesis.oldState = newState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if(!GameStateHandler.Paused) { 
                GraphicsDevice.Clear(Color.Black);
                particleHandler.SmokeEffect.Draw(spriteBatch, GraphicsDevice, camera);
                space.Draw(spriteBatch, GraphicsDevice, camera);
                player.Draw(spriteBatch, GraphicsDevice, camera);
                spawner.Draw(spriteBatch, GraphicsDevice, camera);
                particleHandler.Destruction.Draw(spriteBatch, GraphicsDevice, camera);
            }

            if (GameStateHandler.Paused)
                gameState.Draw(spriteBatch, GraphicsDevice);

            particleHandler.FadeEffect.Draw(spriteBatch, GraphicsDevice, camera);
            base.Draw(gameTime);
        }
    }
}
