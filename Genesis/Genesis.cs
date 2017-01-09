using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Genesis
{
    public class Genesis : Game
    {
        public static KeyboardState oldState;
        public static int Width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int Height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        public static bool Paused = true;
        public static Random random;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Spawner spawner;
        ParticleEngine particleEngine;
        Space space;
        Texture2D cursor;
        Camera camera;
        GameState gameState;

        public Genesis()
        {
            random = new Random();
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
            gameState = new GameState(new Stack<GameState>(), graphics, Content);
            GameState.GameStates.Push(new Menu(GameState.GameStates, graphics, Content));

            cursor = Content.Load<Texture2D>("Textures/cursor");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            particleEngine = new ParticleEngine();
            particleEngine.LoadContent(Content);

            camera = new Camera();

            space = new Space(camera, 20000, 15000);
            space.LoadContent(Content, GraphicsDevice);
            camera.SetCameraPosition(new Vector2(space.Width/2 - Width/2, space.Height / 2 - Height / 2));

            player = new Player(camera, space, particleEngine, new Vector2(space.Width/2, space.Height/2), 0.3f, 0f, 0f, Color.White);
            player.LoadContent(Content);

            spawner = new Spawner(particleEngine, space, player, camera);
            spawner.LoadContent(Content);
            spawner.SpawnEnemies(30);
        }

        public static void StartNewGame()
        {

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();
            if (Paused)
            {
                gameState.Update(gameTime, this);
            }
            else
            {
                if (newState.IsKeyDown(Keys.Escape) && Genesis.oldState.IsKeyUp(Keys.Escape) && Genesis.Paused == false)
                    Paused = true;

                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                    particleEngine.GenerateParticles(50, new Vector2(Mouse.GetState().Position.X + camera.Position.X, Mouse.GetState().Position.Y + camera.Position.Y), particleEngine.textures[0]);

                player.Update(gameTime);
                spawner.Update(gameTime);
                particleEngine.Update(gameTime);
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
                particleEngine.Draw(spriteBatch, GraphicsDevice, camera);
                player.UI.Draw(spriteBatch, GraphicsDevice);

                /*spriteBatch.Begin();
                spriteBatch.Draw(cursor, new Rectangle(Mouse.GetState().Position - new Point(5, 5), new Point(cursor.Width, cursor.Height)), Color.White);
                spriteBatch.End();*/
            }
            base.Draw(gameTime);
        }
    }
}
