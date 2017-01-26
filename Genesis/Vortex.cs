using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis
{
    class Vortex
    {
        public List<Planet> Moons { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public float Scale { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public float ShiningScale { get; set; }
        public bool ShiningStatus { get; set; }
        public float RotationVelocity { get; set; }

        public Vortex(Texture2D texture, Vector2 position, float scale, Color color, Vector2 origin, float rotationVelocity)
        {
            RotationVelocity = rotationVelocity; 
            Origin = origin;
            Texture = texture;
            Position = position;
            Scale = scale;
            Color = color;
            Width = (int)(Texture.Width * Scale);
            Height = (int)(Texture.Height * Scale);
        }

        public void Update(GameTime gameTime)
        {
            Rotation += RotationVelocity * gameTime.ElapsedGameTime.Milliseconds;
            if (ShiningStatus)
            {
                ShiningScale += (float)gameTime.ElapsedGameTime.TotalSeconds * 0.3f;
                if (ShiningScale >= 1.1)
                    ShiningStatus = false;
            }
            else
            {
                ShiningScale -= (float)gameTime.ElapsedGameTime.TotalSeconds * 0.3f;
                if (ShiningScale <= 0.7f)
                    ShiningStatus = true;
            }

        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if (camera.InView(new Rectangle((int)Position.X - Texture.Width / 2, (int)Position.Y - Texture.Height / 2, Width, Height)))
            {
                spriteBatch.Draw(Texture, Position, null, new Color(255, 255, 255, 50), Rotation, Origin, Scale * 1.3f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Texture, Position, null, new Color(255, 255, 255, 50), Rotation, Origin, Scale, SpriteEffects.None, 0f);
            }
        }
    }
}
