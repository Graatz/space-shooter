using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis
{
    class Bullet
    {
        public Vector2 Position { get; set; }
        public float Velocity { get; set; }
        public Vector2 Dir { get; set; }
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public Space Space { get; set; }
        public float Scale { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        private int bulletPath;

        public Bullet(Space space, Vector2 position, float velocity, Vector2 dir, Texture2D texture, Color color)
        {
            Space = space;
            Position = position;
            Velocity = velocity;
            Texture = texture;
            Color = color;
            Dir = dir;

            bulletPath = 0;
            Scale = 0.07f;
            Width = texture.Width * Scale;
            Height = texture.Height * Scale;
        }

        public void Update()
        {
            Position -= Dir * Velocity;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            if (Position.X >= 0 && Position.Y >= 0 && Position.X < Space.Width && Position.Y < Space.Height)
            {
                Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
                Color fadingColor = Color;

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, Space.Camera.getTransformation(graphics));
                spriteBatch.Draw(Texture, Position, null, Color, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
                spriteBatch.End();

                for (int i = 0; i < 10; i++)
                {
                    fadingColor = new Color(Color.R, Color.G, Color.B, fadingColor.A - 20);
                    Vector2 newLocation = Position + Dir * (Velocity * i/2);

                    if (bulletPath >= i)
                    {
                        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, Space.Camera.getTransformation(graphics));
                        spriteBatch.Draw(Texture, newLocation, null, fadingColor, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
                        spriteBatch.End();
                    }
                }
                bulletPath+=2;
            }
        }
    }
}
