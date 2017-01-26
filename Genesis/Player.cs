using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Genesis
{
    class Player : GameObject, ISpaceShip
    {
        public List<ISpaceShip> Enemies { get; set; }
        public Space Space { get; set; }
        public Weapon Weapon { get; set; }
        public ParticleHandler ParticleHandler { get; set; }
        public Statistics Statistics { get; set; }

        public Player(Space space, ParticleHandler particleHandler, Vector2 position, float scale, float rotation, float velocity, Color color)
            : base(position, scale, rotation, velocity, Color.White)
        {
            ParticleHandler = particleHandler;
            Space = space;
        }

        public void LoadContent(ContentManager Content)
        {
            ParticleHandler.Destruction = new Destruction();
            Statistics = new Statistics(7, 0.9f / Scale, 1000, 100);
            Weapon = new Weapon(this, ParticleHandler, Space, ParticleHandler.ColorTextures["orange"], 1300f, 0.07f, 100);
            LoadTexture(Content.Load<Texture2D>("Textures/player"), Scale);
        }

        public bool Intersects(Rectangle rectangle)
        {
            Rectangle playerRectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            if (playerRectangle.Intersects(rectangle))
                return true;
            else
                return false;

        }

        public void Move(GameTime gameTime, Camera camera)
        {
            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Weapon.Shoot(gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Rotation += Statistics.AngularVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Rotation -= Statistics.AngularVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Velocity > 0)
            {
                Vector2 direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
                direction.Normalize();

                Vector2 newPosition = new Vector2(Position.X+direction.X*(Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds), Position.Y+direction.Y*(Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds));
                Position = newPosition;

                camera.MoveCamera(direction * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds));
            }

            if (Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.Up))
            {
                if (Velocity > 0) 
                    Velocity -= Statistics.Acceleration / 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            else
            {
                if (Velocity < Statistics.Acceleration)
                    Velocity += Statistics.Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Velocity > 0)
            {
                ParticleHandler.SmokeEffect.GenerateParticles((int)Velocity / 200, Position - Direction * 6, ParticleHandler.ColorTextures["orange"]);
                ParticleHandler.SmokeEffect.GenerateParticles((int)Velocity / 300, Position - Direction * 6, ParticleHandler.ColorTextures["gray"]);
            }
        }

        public void Update(GameTime gameTime, Camera camera)
        {
            Move(gameTime, camera);
            Weapon.Update(gameTime, camera);

            foreach(var asteroid in Space.Spawner.Asteroids)
            {
                if (Intersects(new Rectangle((int)asteroid.Position.X, (int)asteroid.Position.Y, asteroid.Width, asteroid.Height)))
                {
                    ParticleHandler.Destruction.GenerateParticles((int)(asteroid.Scale * 30), asteroid.Position, asteroid.Texture);
                    Space.Spawner.Asteroids.Remove(asteroid);
                    break;
                }
            }

            float TargetRotation;
            foreach (var vortex in Space.Vortexes)
            {
                TargetRotation = (float)Math.Round(Math.Atan2(vortex.Position.Y + vortex.Origin.Y - Position.Y, vortex.Position.X + vortex.Origin.X - Position.X), 1);
                if (Intersects(new Rectangle((int)vortex.Position.X - vortex.Width / 2, (int)vortex.Position.Y - vortex.Height / 2, vortex.Width, vortex.Height)))
                {
                    if (Velocity <= 100)
                        Velocity = 100;
                    if (Rotation >= TargetRotation)
                        Rotation -= Statistics.AngularVelocity / 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    else
                        Rotation += Statistics.AngularVelocity / 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Camera camera)
        {
            Weapon.Draw(spriteBatch, graphics, camera);
            Rectangle sourceRectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, camera.getTransformation(graphics));
            spriteBatch.Draw(Texture, Position, null, new Color(150, 150, 150, 255), Rotation, new Vector2(Texture.Width/2, Texture.Height/2), Scale, SpriteEffects.None, 0f);
            spriteBatch.End();
        }


    }
}
