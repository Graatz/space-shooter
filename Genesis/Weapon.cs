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
        public ISpaceShip SpaceShip { get; set; }
        public Space Space { get; set; }

        public ParticleHandler ParticleHandler { get; set; }

        public List<Bullet> Bullets { get; set; }
        public Texture2D BulletTexture { get; set; }
        public float BulletVelocity { get; set; }
        public float BulletScale { get; set; }

        public int Power { get; set; }
        public double Delay { get; set; }

        public Weapon(ISpaceShip spaceShip, ParticleHandler particleHandler, Space space, Texture2D bulletTexture, float bulletVelocity, float bulletScale, int power)
        {
            SpaceShip = spaceShip;
            ParticleHandler = particleHandler;
            Space = space;
            BulletTexture = bulletTexture;
            BulletVelocity = bulletVelocity;
            BulletScale = bulletScale;
            Power = power;

            Bullets = new List<Bullet>();
        }

        public void Update(GameTime gameTime, Camera camera)
        {
            UpdateBullets(gameTime, camera);
        }

        public void UpdateBullets(GameTime gameTime, Camera camera)
        {
            for (int i = 0; i < Bullets.Count; ++i)
            {
                Bullets[i].Update(gameTime);
                if (Bullets[i].Position.X < 0 || Bullets[i].Position.Y < 0 && Bullets[i].Position.X > Space.Width && Bullets[i].Position.Y > Space.Height)
                {
                    Bullets.RemoveAt(i);
                }
                else
                {
                    for (int j = 0; j < SpaceShip.Enemies.Count; ++j)
                    {
                       if (camera.InView(new Rectangle((int)SpaceShip.Enemies.ElementAt(j).Position.X, (int)SpaceShip.Enemies.ElementAt(j).Position.Y,
                            SpaceShip.Enemies.ElementAt(j).Width, SpaceShip.Enemies.ElementAt(j).Height)))
                        {
                            Rectangle bulletRectangle = new Rectangle((int)Bullets[i].Position.X, (int)Bullets[i].Position.Y, 
                                                                      (int)(Bullets[i].Width), (int)(Bullets[i].Height));
                            Rectangle enemyRectangle = new Rectangle((int)SpaceShip.Enemies[j].Position.X, (int)SpaceShip.Enemies[j].Position.Y, 
                                                                     (int)SpaceShip.Enemies[j].Width, (int)SpaceShip.Enemies[j].Height);
                            if (bulletRectangle.Intersects(enemyRectangle))
                            {
                                if (SpaceShip.Enemies.ElementAt(j).Statistics.Health > 10)
                                    SpaceShip.Enemies.ElementAt(j).Statistics.Health -= Power;

                                if (SpaceShip.Enemies.ElementAt(j).Statistics.Health <= 0)
                                {
                                    SpaceShip.Enemies.RemoveAt(j);
                                    ParticleHandler.Destruction.GenerateParticles(150, Bullets[i].Position, BulletTexture);
                                }
                                else
                                {
                                    ParticleHandler.Destruction.GenerateParticles(20, Bullets[i].Position, BulletTexture);
                                }

                                Bullets.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void Shoot(GameTime gameTime)
        {
            Delay -= SpaceShip.Statistics.AttackSpeed * gameTime.ElapsedGameTime.TotalSeconds;

            if (Delay <= 0)
            {
                Vector2 bulletPosition = new Vector2(SpaceShip.Position.X, SpaceShip.Position.Y);
                Bullets.Add(new Bullet(SpaceShip, Space, BulletTexture, bulletPosition, BulletScale, SpaceShip.Rotation, new Vector2((float)Math.Cos(SpaceShip.Rotation), (float)Math.Sin(SpaceShip.Rotation)), BulletVelocity + SpaceShip.Velocity, Color.White));
                Delay = 1;
            }

        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            for (int i = 0; i < Bullets.Count; ++i)
            {
                if (Space.Camera.InView(new Rectangle((int)Bullets[i].Position.X, (int)Bullets[i].Position.Y, Bullets[i].Width, Bullets[i].Height)))
                    Bullets[i].Draw(spriteBatch, graphics);
            }
        }
    }
}
