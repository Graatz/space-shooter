using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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

        public Weapon(Player player)
        {
            Velocity = 20.5f;
            Player = player;
            Bullets = new List<Bullet>();
        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            Shoot(gameTime, mouseState);
            UpdateBullets();
        }

        public void UpdateBullets()
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                Bullets[i].Update();
                if (Bullets[i].Location.X < 0 || Bullets[i].Location.Y < 0 && Bullets[i].Location.X > Player.Space.Width && Bullets[i].Location.Y > Player.Space.Height)
                {
                    Bullets.RemoveAt(i);
                }
                else
                {
                    for (int j = 0; j < Player.EnemySpawner.Enemies.Count; j++)
                    {
                        Rectangle bulletRectangle = new Rectangle((int)Bullets[i].Location.X, (int)Bullets[i].Location.Y, (int)(Bullets[i].Width), (int)(Bullets[i].Height));
                        Rectangle enemyRectangle = new Rectangle((int)Player.EnemySpawner.Enemies[j].Position.X, (int)Player.EnemySpawner.Enemies[j].Position.Y, (int)Player.EnemySpawner.Enemies[j].Width, (int)Player.EnemySpawner.Enemies[j].Height);
                        if (bulletRectangle.Intersects(enemyRectangle))
                        {
                            Player.ParticleEngine.GenerateParticles(70, Bullets[i].Location);
                            Bullets.RemoveAt(i);
                            Player.EnemySpawner.Enemies.RemoveAt(j);
                            Player.EnemySpawner.SpawnEnemies(1);
                            break;
                        }
                    }
                }
            }
        }

        public void Shoot(GameTime gameTime, MouseState mouseState)
        {
            Player.Counter -= Player.AttackSpeed * gameTime.ElapsedGameTime.TotalSeconds;

            if (Player.Counter <= 0)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Vector2 mousePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y);
                    Vector2 bulletPosition = new Vector2(Player.Position.X + Player.Width / 2, Player.Position.Y + Player.Height / 2);
                    Vector2 direction = bulletPosition - mousePosition;
                    direction.Normalize();
                    Bullets.Add(new Bullet(Player.Space, bulletPosition, Velocity, direction, Player.ParticleEngine.textures[0], Color.Yellow));
                    Player.Counter = 1;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                Bullets[i].Draw(spriteBatch);
            }
        }
    }
}
