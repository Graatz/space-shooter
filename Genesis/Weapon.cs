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
        public Texture2D BulletTexture { get; set; }
        public float BulletVelocity { get; set; }
        public float BulletScale { get; set; }
        public double Counter { get; set; }

        public Weapon(Player player, Texture2D bulletTexture, float bulletVelocity, float bulletScale)
        {
            BulletTexture = bulletTexture;
            BulletVelocity = bulletVelocity;
            BulletScale = bulletScale;

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
                    for (int j = 0; j < Player.Spawner.Enemies.Count; j++)
                    {
                        Rectangle bulletRectangle = new Rectangle((int)Bullets[i].Position.X, (int)Bullets[i].Position.Y, (int)(Bullets[i].Width), (int)(Bullets[i].Height));
                        Rectangle enemyRectangle = new Rectangle((int)Player.Spawner.Enemies[j].Position.X, (int)Player.Spawner.Enemies[j].Position.Y, (int)Player.Spawner.Enemies[j].Width, (int)Player.Spawner.Enemies[j].Height);
                        if (bulletRectangle.Intersects(enemyRectangle))
                        {
                            Player.ParticleEngine.GenerateParticles(70, Bullets[i].Position);
                            Bullets.RemoveAt(i);
                            Player.Spawner.Enemies.RemoveAt(j);
                            Player.Spawner.SpawnEnemies(1);
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
                    Bullets.Add(new Bullet(Player, Player.Space, BulletTexture, bulletPosition, BulletScale, Player.Rotation, new Vector2((float)Math.Cos(Player.Rotation), (float)Math.Sin(Player.Rotation)), BulletVelocity + Player.Velocity, Color.White));
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
