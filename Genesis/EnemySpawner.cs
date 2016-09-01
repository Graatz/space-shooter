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
    class EnemySpawner
    {
        public List<Enemy> Enemies { get; set; }
        public Player Player { get; set; }
        public GraphicsDevice GraphicsDevice { get; set; }
        public Space Space { get; set; }

        private List<Texture2D> enemyTextures;
        private Random random;

        public EnemySpawner(Space space, Player player, GraphicsDevice graphicsDevice)
        {
            Space = space;
            Player = player;
            GraphicsDevice = graphicsDevice;
            Player.EnemySpawner = this;     
        }

        public void LoadContent(ContentManager Content)
        {
            Enemies = new List<Enemy>();
            random = new Random();

            enemyTextures = new List<Texture2D>();
            enemyTextures.Add(Content.Load<Texture2D>("Textures/enemy"));
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

        public void Update()
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Draw(spriteBatch, graphics);
            }
        }

    }
}
