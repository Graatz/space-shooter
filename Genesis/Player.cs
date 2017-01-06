using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
    class Player : GameObject
    {
        public Camera Camera { get; set; }
        public Space Space { get; set; }
        public Spawner Spawner { get; set; }
        public Weapon Weapon { get; set; }
        public ParticleEngine ParticleEngine { get; set; }
        public Statistics Statistics { get; set; }
        public PlayerUI UI { get; set; }

        public Player(Camera camera, Space space, ParticleEngine particleEngine, Vector2 position, float scale, float rotation, float velocity, Color color)
            : base(position, scale, rotation, velocity, Color.White)
        {
            Camera = camera;
            ParticleEngine = particleEngine;
            Space = space;
        }

        public void LoadContent(ContentManager Content)
        {
            Statistics = new Statistics(7, 0.9f / Scale, 800, 100, 100);
            UI = new PlayerUI(Statistics);
            Weapon = new Weapon(this, ParticleEngine.textures[1], 1300f, 0.07f);
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

        public void Move(GameTime gameTime)
        {
            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
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

                Camera.MoveCamera(direction * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds));
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
        }

        public void Update(GameTime gameTime)
        {
            Move(gameTime);
            Weapon.Update(gameTime, Camera);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            Weapon.Draw(spriteBatch, graphics);

            Rectangle sourceRectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Space.Camera.getTransformation(graphics));
            spriteBatch.Draw(Texture, Position, null, new Color(150, 150, 150, 255), Rotation, new Vector2(Texture.Width/2, Texture.Height/2), Scale, SpriteEffects.None, 0f);
            spriteBatch.End();
        }


    }
}
