using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis
{
    class Weapon
    {
        public Player Player { get; set; }
        public List<Bullet> Bullets { get; set; }
        public float Velocity { get; set; }
        public double Counter { get; set; }

        public Weapon(Player player)
        {
            Velocity = 1300f;
            Player = player;
            Bullets = new List<Bullet>();
        }

        public void Update(GameTime gameTime)
        {
            Shoot(gameTime);
            UpdateBullets(gameTime);
        }

        public void UpdateBullets(GameTime gameTime)
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                Bullets[i].Update(gameTime);
                if (Bullets[i].Position.X < 0 || Bullets[i].Position.Y < 0 && Bullets[i].Position.X > Player.Space.Width && Bullets[i].Position.Y > Player.Space.Height)
                {
                    Bullets.RemoveAt(i);
                }
                else
                {
                    for (int j = 0; j < Player.EnemySpawner.Enemies.Count; j++)
                    {
                        Rectangle bulletRectangle = new Rectangle((int)Bullets[i].Position.X, (int)Bullets[i].Position.Y, (int)(Bullets[i].Width), (int)(Bullets[i].Height));
                        Rectangle enemyRectangle = new Rectangle((int)Player.EnemySpawner.Enemies[j].Position.X, (int)Player.EnemySpawner.Enemies[j].Position.Y, (int)Player.EnemySpawner.Enemies[j].Width, (int)Player.EnemySpawner.Enemies[j].Height);
                        if (bulletRectangle.Intersects(enemyRectangle))
                        {
                            Player.ParticleEngine.GenerateParticles(70, Bullets[i].Position);
                            Bullets.RemoveAt(i);
                            Player.EnemySpawner.Enemies.RemoveAt(j);
                            Player.EnemySpawner.SpawnEnemies(1);
                            break;
                        }
                    }
                }
            }
        }

        public void Shoot(GameTime gameTime)
        {
            Counter -= Player.AttackSpeed * gameTime.ElapsedGameTime.TotalSeconds;

            if (Counter <= 0)
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    Vector2 bulletPosition = new Vector2(Player.Position.X, Player.Position.Y);
                    Bullets.Add(new Bullet(Player, Player.Space, bulletPosition, Velocity + Player.Velocity, Player.ParticleEngine.textures[1], Color.Yellow));
                    Counter = 1;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                Bullets[i].Draw(spriteBatch, graphics);
            }
        }
    }
}
