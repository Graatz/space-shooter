using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis
{
    class Asteroid
    {
        public Texture2D Texture { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2 Position { get; set; }
        public float Velocity { get; set; }
        public Vector2 Direction { get; set; }
        private Random random { get; set; }
        public Space Space { get; set; }
        public Color Color { get; set; }

        public Asteroid(Space space, Texture2D texture, Vector2 position, float rotation, float scale, float velocity, Vector2 direction)
        {
            random = new Random();
            Space = space;
            Texture = texture;
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Velocity = velocity;
            Direction = direction;
            int R = (int)(Scale * 300);
            int G = (int)(Scale * 300);
            int B = (int)(Scale * 300);
            Color = new Color(R, G, B);

            Width = (int)(Texture.Width * Scale);
            Height = (int)(Texture.Height * Scale);
        }

        public void Update(GameTime gameTime)
        {
            Position += Direction * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Rotation += 0.01f * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Space.Camera.getTransformation(graphics));
            spriteBatch.Draw(Texture, Position, null, Color, Rotation, new Vector2(Texture.Width/2, Texture.Height/2), Scale, SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
