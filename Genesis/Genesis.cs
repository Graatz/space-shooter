using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Genesis
{
    public class Genesis : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        EnemySpawner enemySpawner;
        ParticleEngine particleEngine;
        Space space;
        Texture2D cursor;
        MouseState mouseState;

        public Genesis()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content"; 
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            cursor = Content.Load<Texture2D>("Textures/cursor");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            particleEngine = new ParticleEngine();
            particleEngine.LoadContent(Content);

            space = new Space(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            space.LoadContent(Content, GraphicsDevice);
            space.GenerateStars();

            player = new Player(space, particleEngine, GraphicsDevice, new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight /2));
            player.LoadContent(Content);

            enemySpawner = new EnemySpawner(space, player, GraphicsDevice);
            enemySpawner.LoadContent(Content);
            enemySpawner.SpawnEnemies(10);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime, mouseState);
            enemySpawner.Update();
            particleEngine.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            space.Draw(spriteBatch);
            player.Draw(spriteBatch);
            enemySpawner.Draw(spriteBatch);
            particleEngine.Draw(spriteBatch);

            spriteBatch.Begin();
            spriteBatch.Draw(cursor, new Rectangle(mouseState.Position - new Point(5, 5), new Point(cursor.Width, cursor.Height)), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
