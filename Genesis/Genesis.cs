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
        public static bool Paused = true;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Player player;
        private Spawner spawner;
        private ParticleHandler particleHandler;
        private Space space;
        private Camera camera;
        private GameState gameState;

        public Genesis()
        {
            IsFixedTimeStep = true;
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
            GameState.GameStates = new Stack<GameState>();
            gameState = new GameState(GameState.GameStates, graphics, Content);
            GameState.GameStates.Push(new Menu(GameState.GameStates, graphics, Content));

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public void StartNewGame()
        {
            particleHandler = new ParticleHandler();
            particleHandler.LoadContent(Content);

            camera = new Camera();

            space = new Space(camera, 20000, 15000);
            space.LoadContent(Content, GraphicsDevice);
            camera.SetCameraPosition(new Vector2(space.Width / 2 - Width / 2, space.Height / 2 - Height / 2));

            player = new Player(space, particleHandler, new Vector2(space.Width / 2, space.Height / 2), 0.3f, 0f, 0f, Color.White);
            player.LoadContent(Content);

            spawner = new Spawner(particleHandler, space, player, camera);
            spawner.LoadContent(Content);
            spawner.SpawnEnemies(50);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            newState = Keyboard.GetState();
            if (Paused)
            {
                gameState.Update(gameTime, this, this);
            }
            else
            {
                if (newState.IsKeyDown(Keys.Escape) && Genesis.oldState.IsKeyUp(Keys.Escape) && Genesis.Paused == false)
                    Paused = true;

                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                    particleHandler.Destruction.GenerateParticles(10, new Vector2(Mouse.GetState().Position.X + camera.Position.X, Mouse.GetState().Position.Y + camera.Position.Y), particleHandler.ColorTextures["red"]);

                player.Update(gameTime);
                spawner.Update(gameTime);
                particleHandler.Update(gameTime, camera);
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
                particleHandler.SmokeEffect.Draw(spriteBatch, GraphicsDevice, camera);
                space.Draw(spriteBatch, GraphicsDevice);
                player.Draw(spriteBatch, GraphicsDevice);
                spawner.Draw(spriteBatch, GraphicsDevice);
                particleHandler.Destruction.Draw(spriteBatch, GraphicsDevice, camera);
            }
            base.Draw(gameTime);
        }
    }
}
