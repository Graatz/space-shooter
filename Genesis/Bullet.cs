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
        public float Angle { get; set; }
        public Player Player { get; set; }
        public Vector2 Position { get; set; }
        public float Velocity { get; set; }
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public Space Space { get; set; }
        public float Scale { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        private int bulletPath;

        public Bullet(Player player, Space space, Vector2 position, float velocity, Texture2D texture, Color color)
        {
            Player = player;
            Space = space;
            Position = position;
            Velocity = velocity;
            Texture = texture;
            Color = color;
            Angle = Player.Angle;

            bulletPath = 0;
            Scale = 0.07f;
            Width = texture.Width * Scale;
            Height = texture.Height * Scale;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 direction = new Vector2((float)Math.Cos(Angle),
                                    (float)Math.Sin(Angle));
            direction.Normalize();

            Vector2 newPosition = new Vector2(Position.X + direction.X * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds), Position.Y + direction.Y * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds));

            Position = newPosition;
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
                    Vector2 direction = new Vector2((float)Math.Cos(Angle),
                                    (float)Math.Sin(Angle));
                    direction.Normalize();
                    fadingColor = new Color(Color.R, Color.G, Color.B, fadingColor.A - 20);
                    Vector2 newLocation = Position - direction * (Velocity * i/200);

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
