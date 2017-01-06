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
    class Enemy : GameObject
    {
        public Space Space { get; set; }
        public Vector2 Target { get; set; }
        public Vector2 InitialPosition { get; set; }
        public Camera Camera { get; set; }
        public float TargetRotation { get; set; }

        public Enemy(Space space, Texture2D texture, Vector2 position, Camera camera, float rotation, float scale, float velocity, Vector2 direction, Vector2 target)
             : base (texture, position, scale, rotation, direction, velocity, Color.White)
        {
            Space = space;
            InitialPosition = Position;
            Camera = camera;
            Direction = direction;
            Target = target;

            TargetRotation = (float)Math.Round(Math.Atan2(Target.Y - Position.Y, Target.X - Position.X), 1);
        }

        public void Update(GameTime gameTime)
        {
            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            Direction.Normalize();
            Position = new Vector2(Position.X + Direction.X * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds), Position.Y + Direction.Y * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds));

            if (InitialPosition.X > Target.X)
            {
                if (Position.X <= Target.X)
                {
                    InitialPosition = Position;
                    Target = new Vector2(Space.random.Next(0, Space.Width), Space.random.Next(0, Space.Height));
                    TargetRotation = (float)Math.Round(Math.Atan2(Target.Y - Position.Y, Target.X - Position.X), 1);
                    //Rotation = (float)Math.Round(Math.Atan2(Target.Y - Position.Y, Target.X - Position.X), 1);
                }
            } else if (InitialPosition.X < Target.X)
            {
                if (Position.X >= Target.X)
                {
                    InitialPosition = Position;
                    Target = new Vector2(Space.random.Next(0, Space.Width), Space.random.Next(0, Space.Height));
                    TargetRotation = (float)Math.Round(Math.Atan2(Target.Y - Position.Y, Target.X - Position.X), 1);
                    //Rotation = (float)Math.Round(Math.Atan2(Target.Y - Position.Y, Target.X - Position.X), 1);
                    //Debug.WriteLine(Rotation);
                }
            }

            if (TargetRotation > Math.Round(Rotation, 1))
            {
                Rotation += 0.05f;
            }
            else if (TargetRotation < Math.Round(Rotation, 1))
            {
                Rotation -= 0.05f;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            if (Camera.InView(new Rectangle((int)Position.X, (int)Position.Y, Width, Height)))
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Space.Camera.getTransformation(graphics));
                spriteBatch.Draw(Texture, Position, null, Color, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
                spriteBatch.End();
            }
        }
    }
}
