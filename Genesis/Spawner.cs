using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis
{
    class Spawner
    {
        public Player Player { get; set; }
        public Space Space { get; set; }
        public List<Enemy> Enemies { get; set; }
        public List<Asteroid> Asteroids { get; set; }
        public double Counter { get; set; }
        public Camera Camera { get; set; }

        private List<Texture2D> enemyTextures;
        private List<Texture2D> asteroidTextures;

        public Spawner(Space space, Player player, Camera camera)
        {
            Space = space;
            Player = player;
            Camera = camera;
            Player.Spawner = this; 
        }

        public void LoadContent(ContentManager Content)
        {
            Enemies = new List<Enemy>();
            Asteroids = new List<Asteroid>();

            asteroidTextures = new List<Texture2D>();
            asteroidTextures.Add(Content.Load<Texture2D>("Textures/asteroid1"));
            asteroidTextures.Add(Content.Load<Texture2D>("Textures/asteroid2"));

            enemyTextures = new List<Texture2D>();
            enemyTextures.Add(Content.Load<Texture2D>("Textures/enemy"));
        }

        public void SpawnAsteroid()
        {
            Texture2D texture = asteroidTextures[Space.random.Next(asteroidTextures.Count)];

            float scale = 0.7f / (float)(Space.random.NextDouble() * (1.0 - 4.0) + 4.0);
            float rotation = (float)Space.random.NextDouble();
            Vector2 position = new Vector2(Space.random.Next(-900, Space.Width - 900), Space.random.Next(-900, -300));
            float velocity = Space.random.Next(200, 400);
            Vector2 direction = new Vector2(Space.random.Next(1, 10), Space.random.Next(1, 10));
            direction.Normalize();

            int R = (int)(scale * 300);
            int G = (int)(scale * 300);
            int B = (int)(scale * 300);
            Color color = new Color(R, G, B);

            Asteroid asteroid = new Asteroid(Space, texture, position, rotation, scale, velocity, direction, color);
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
            Texture2D texture = enemyTextures[Space.random.Next(enemyTextures.Count)];
            float scale = 0.8f / (float)(Space.random.NextDouble() * (3.0 - 4.0) + 4.0);
            Vector2 position = new Vector2(Space.random.Next(Space.Width + 300, Space.Width + 900), Space.random.Next(Space.Height + 300, Space.Height + 900));
            float velocity = Space.random.Next(200, 400);
            Vector2 target = new Vector2(Space.random.Next(300, Space.Width - 300), Space.random.Next(300, 700));
            float rotation = (float)Math.Atan2(target.Y - position.Y, target.X - position.X);
            Vector2 direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            direction.Normalize();

            Enemy enemy = new Enemy(Space, texture, position, Camera, rotation, scale, velocity, direction, target);
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
                Counter = 1;
            }

            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Update(gameTime);
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
