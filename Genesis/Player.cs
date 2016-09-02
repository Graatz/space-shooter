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
    class Player
    {
        public Texture2D Texture { get; set; }
        public ParticleEngine ParticleEngine { get; set; }
        public Spawner EnemySpawner { get; set; }
        public double AttackSpeed { get; set; }
        public Weapon Weapon { get; set; }
        public float Scale { get; set; }
        public Vector2 Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Camera Camera { get; set; }
        public float Angle { get; set; }
        public float AngularVelocity { get; set; }
        public float Acceleration { get; set; }

        private Space space;
        public Space Space
        {
            get
            {
                return space;
            }
            set
            {
                space = value;
            }
        }

        private float velocity;
        public float Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }

        public Player(Camera camera, Space space, ParticleEngine particleEngine, GraphicsDevice device, Vector2 position)
        {
            Camera = camera;
            ParticleEngine = particleEngine;
            Space = space;
            Position = position;

            Acceleration = 800;
            Scale = 0.4f;
            Velocity = 700f;
            AttackSpeed = 7;
            AngularVelocity = 0.9f / Scale;
        }

        public void LoadContent(ContentManager Content)
        {
            Weapon = new Weapon(this);
            Texture = Content.Load<Texture2D>("Textures/player");
            Width = (int)(Texture.Width * Scale);
            Height = (int)(Texture.Height * Scale);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Angle += AngularVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Angle -= AngularVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Velocity > 0)
            {
                Debug.WriteLine((float)gameTime.ElapsedGameTime.TotalSeconds);
                Vector2 direction = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));
                direction.Normalize();
                Vector2 newPosition = new Vector2(Position.X+direction.X*(Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds), Position.Y+direction.Y*(Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds));               
                Vector2 dir = direction * velocity;

                Position = newPosition;
                Camera.MoveCamera(direction * (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds));
            }

            if (Keyboard.GetState().IsKeyUp(Keys.W))
            {
                if (Velocity > 0)
                    Velocity -= Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
            Weapon.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            Weapon.Draw(spriteBatch, graphics);

            if (Position.X >= 0 && Position.Y >= 0 && Position.X < Space.Width && Position.Y < Space.Height)
            {
                Rectangle sourceRectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
                spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Space.Camera.getTransformation(graphics));
                spriteBatch.Draw(Texture, Position, null, new Color(150, 150, 150, 255), Angle, new Vector2(Texture.Width/2, Texture.Height/2), Scale, SpriteEffects.None, 0f);
                //spriteBatch.Draw(Texture, sourceRectangle, Color.White);
                spriteBatch.End();
            }
        }


    }
}
