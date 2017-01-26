using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Genesis
{
    class Spawner
    {
        private Random random;
        public Camera Camera { get; set; }
        public ParticleHandler ParticleHandler { get; set; }
        public Player Player { get; set; }
        public Space Space { get; set; }

        public List<ISpaceShip> Enemies { get; set; }
        private List<Texture2D> enemyTextures;

        public List<Asteroid> Asteroids { get; set; }
        public List<Texture2D> asteroidTextures;

        public double Counter { get; set; }

        public Spawner(ParticleHandler particleHandler, Space space, Player player, Camera camera)
        {
            random = new Random();
            ParticleHandler = particleHandler;
            Space = space;
            Player = player;
            Camera = camera;
            Space.Spawner = this;
        }

        public void LoadContent(ContentManager Content)
        {
            Enemies = new List<ISpaceShip>();
            Player.Enemies = Enemies;

            Asteroids = new List<Asteroid>();

            asteroidTextures = new List<Texture2D>();
            asteroidTextures.Add(Content.Load<Texture2D>("Textures/asteroid1"));
            asteroidTextures.Add(Content.Load<Texture2D>("Textures/asteroid2"));
            asteroidTextures.Add(Content.Load<Texture2D>("Textures/asteroid3"));

            enemyTextures = new List<Texture2D>();
            enemyTextures.Add(Content.Load<Texture2D>("Textures/enemy"));

            SpawnEnemies(50);
        }

        public void SpawnAsteroid()
        {
            Texture2D texture = asteroidTextures[random.Next(asteroidTextures.Count)];

            float scale = 0.5f / (float)(random.NextDouble() * (1.0 - 4.0) + 4.0);
            float rotation = (float)random.NextDouble();
            Vector2 position = new Vector2(random.Next(-900, Space.Width - 900), random.Next(-900, -300));
            float velocity = random.Next(200, 400);
            Vector2 direction = new Vector2(random.Next(1, 10), random.Next(1, 10));
            direction.Normalize();

            int R = (int)(scale * 300);
            int G = (int)(scale * 300);
            int B = (int)(scale * 300);
            Color color = new Color(R, G, B);

            Asteroid asteroid = new Asteroid(texture, position, rotation, scale, velocity, direction, color);
            Asteroids.Add(asteroid);
        }

        public void SpawnAsteroids(int number)
        {
            for (int i = 0; i < number; ++i)
            {
                SpawnAsteroid();
            }
        }

        public void SpawnEnemy()
        {
            Texture2D texture = enemyTextures[random.Next(enemyTextures.Count)];
            float scale = 0.8f / (float)(random.NextDouble() * (3.0 - 4.0) + 4.0);
            Vector2 position = new Vector2(random.Next(-900, Space.Width + 900), random.Next(-900, Space.Height + 900));
            float velocity = random.Next(200, 400);
            Vector2 target = new Vector2(random.Next(0, Space.Width), random.Next(0, Space.Height));
            float rotation = (float)Math.Round(Math.Atan2(target.Y - position.Y, target.X - position.X), 1);
            Vector2 direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            direction.Normalize();

            Enemy enemy = new Enemy(this, Player, ParticleHandler, Space, texture, position, Camera, rotation, scale, velocity, direction, target);
            Enemies.Add(enemy);
        }

        public void SpawnEnemies(int number)
        {
            for (int i = 0; i < number; ++i)
            {
                SpawnEnemy();
            }
        }

        public void Update(GameTime gameTime)
        {
            Counter -= gameTime.ElapsedGameTime.TotalSeconds;

            if (Counter <= 0)
            {
                SpawnAsteroids(100);
                Counter = 1;
            }

            for (int i = 0; i < Enemies.Count; ++i)
            {
                Enemies[i].Update(gameTime, Camera);
            }

            for (int i = 0; i < Asteroids.Count; ++i)
            {
                Asteroids[i].Update(gameTime);

                if (Asteroids[i].Position.X > Space.Width || Asteroids[i].Position.Y > Space.Height || Asteroids[i].Position.X < 0)
                    Asteroids.RemoveAt(i);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Camera camera)
        {
            for (int i = 0; i < Asteroids.Count; ++i)
            {
                if (camera.InView(new Rectangle((int)Asteroids[i].Position.X, (int)Asteroids[i].Position.Y, Asteroids[i].Width, Asteroids[i].Height)))
                    Asteroids[i].Draw(spriteBatch, graphics, camera);
            }

            for (int i = 0; i < Enemies.Count; ++i)
            {
                Enemies[i].Weapon.Draw(spriteBatch, graphics, camera);
                if (camera.InView(new Rectangle((int)Enemies[i].Position.X, (int)Enemies[i].Position.Y, Enemies[i].Width, Enemies[i].Height)))
                    Enemies[i].Draw(spriteBatch, graphics, Camera);
            }
        }

    }
}
