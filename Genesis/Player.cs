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

        public double AttackSpeed { get; set; }
        public float AngularVelocity { get; set; }
        public float Acceleration { get; set; }

        public Player(Camera camera, Space space, ParticleEngine particleEngine, Vector2 position, float scale, float rotation, float velocity, Color color)
            : base(position, scale, rotation, velocity, Color.White)
        {
            Camera = camera;
            ParticleEngine = particleEngine;
            Space = space;

            Acceleration = 800;
            AttackSpeed = 7;
            AngularVelocity = 0.9f / Scale;
        }

        public void LoadContent(ContentManager Content)
        {
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Rotation += AngularVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Rotation -= AngularVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Velocity > 0)
            {
                Vector2 direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
                direction.Normalize();

                Vector2 newPosition = new Vector2(Position.X+direction.X*(Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds), Position.Y+direction.Y*(Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds));
                Position = newPosition;

                Camera.MoveCamera(direction * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds));
            }

            if (Keyboard.GetState().IsKeyUp(Keys.W))
            {
                if (Velocity > 0)
                    Velocity -= Acceleration / 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                if (Velocity < Acceleration)
                    Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
