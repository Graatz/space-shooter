using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Genesis
{
    public class Genesis : Game
    {
        public static int Width = 2560;
        public static int Height = 1080;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Spawner enemySpawner;
        ParticleEngine particleEngine;
        Space space;
        Texture2D cursor;
        Camera camera;

        public Genesis()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Width;
            graphics.PreferredBackBufferHeight = Height;
            graphics.IsFullScreen = true ;
            graphics.SynchronizeWithVerticalRetrace = true;
            //graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            IsFixedTimeStep = false;
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            cursor = Content.Load<Texture2D>("Textures/cursor");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            particleEngine = new ParticleEngine();
            particleEngine.LoadContent(Content);

            camera = new Camera();
            camera.SetCameraPosition(new Vector2(0, 0));

            space = new Space(camera, 10000, 8000);
            space.LoadContent(Content, GraphicsDevice);

            player = new Player(camera, space, particleEngine, GraphicsDevice, new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight /2));
            player.LoadContent(Content);

            enemySpawner = new Spawner(space, player, GraphicsDevice);
            enemySpawner.LoadContent(Content);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);
            enemySpawner.Update(gameTime);
            particleEngine.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            space.Draw(spriteBatch, GraphicsDevice);
            player.Draw(spriteBatch, GraphicsDevice);
            enemySpawner.Draw(spriteBatch, GraphicsDevice);
            particleEngine.Draw(spriteBatch, GraphicsDevice, camera);

            spriteBatch.Begin();
            spriteBatch.Draw(cursor, new Rectangle(Mouse.GetState().Position - new Point(5, 5), new Point(cursor.Width, cursor.Height)), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
