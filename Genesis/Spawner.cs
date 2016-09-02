using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis
{
    class Spawner
    {
        public List<Enemy> Enemies { get; set; }
        public List<Asteroid> Asteroids { get; set; }
        public Player Player { get; set; }
        public GraphicsDevice GraphicsDevice { get; set; }
        public Space Space { get; set; }
        public double Counter { get; set; }

        private List<Texture2D> enemyTextures;
        private Random random;

        public Spawner(Space space, Player player, GraphicsDevice graphicsDevice)
        {
            Space = space;
            Player = player;
            GraphicsDevice = graphicsDevice;
            Player.EnemySpawner = this;     
        }

        public void LoadContent(ContentManager Content)
        {
            Enemies = new List<Enemy>();
            Asteroids = new List<Asteroid>();
            random = new Random();

            enemyTextures = new List<Texture2D>();
            enemyTextures.Add(Content.Load<Texture2D>("Textures/asteroid1"));
            enemyTextures.Add(Content.Load<Texture2D>("Textures/asteroid2"));
        }

        public void SpawnAsteroid()
        {
            Texture2D texture = enemyTextures[random.Next(enemyTextures.Count)];

            float scale = 0.7f / (float)(random.NextDouble() * (1.0 - 4.0) + 4.0);
            float rotation = (float)random.NextDouble();
            Vector2 position = new Vector2(random.Next(-900, Space.Width - 900), random.Next(-900, -300));
            float velocity = random.Next(200, 400);
            Vector2 direction = new Vector2(random.Next(1, 10), random.Next(1, 10));
            direction.Normalize();

            Asteroid asteroid = new Asteroid(Space, texture, position, rotation, scale, velocity, direction);
            Asteroids.Add(asteroid);
        }

        public void SpawnAsteroids(int number)
        {
            for (int i = 0; i < number; i++)
            {
                SpawnAsteroid();
            }
        }

        public void SpawnEnemy()
        {
            Enemy enemy = new Enemy(Space, Player, GraphicsDevice, enemyTextures[0], new Vector2(random.Next(Space.Width), random.Next(Space.Height)));
            Enemies.Add(enemy);
        }

        public void SpawnEnemies(int number)
        {
            for (int i = 0; i < number; i++)
            {
                SpawnEnemy();
            }
        }

        public void Update(GameTime gameTime)
        {
            Counter -= gameTime.ElapsedGameTime.TotalSeconds;

            if (Counter <= 0)
            {
                SpawnAsteroids(10);
                Counter = 10;
            }

            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Update();
            }

            for (int i = 0; i < Asteroids.Count; i++)
            {
                Asteroids[i].Update(gameTime);

                if (Asteroids[i].Position.X > Space.Width || Asteroids[i].Position.Y > Space.Height)
                {
                    Asteroids.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            for (int i = 0; i < Asteroids.Count; i++)
            {
                if (Space.Camera.InView(new Rectangle((int)Asteroids[i].Position.X, (int)Asteroids[i].Position.Y, Asteroids[i].Width, Asteroids[i].Height)))
                    Asteroids[i].Draw(spriteBatch, graphics);
            }

            for (int i = 0; i < Enemies.Count; i++)
            {
                if (Space.Camera.InView(new Rectangle((int)Enemies[i].Position.X, (int)Enemies[i].Position.Y, Enemies[i].Width, Enemies[i].Height)))
                    Enemies[i].Draw(spriteBatch, graphics);
            }
        }

    }
}
