using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis
{
    class Sun
    {
        public Camera Camera { get; set; }
        public List<Planet> Moons { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Location { get; set; }
        public Color Color { get; set; }
        public float Scale { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public float ShiningScale { get; set; }
        public bool ShiningStatus { get; set; }

        public Sun(Camera camera, Texture2D texture, Vector2 location, float scale, Color color, Vector2 origin)
        {
            Origin = origin;
            Camera = camera;
            Texture = texture;
            Location = location;
            Scale = scale;
            Color = color;
            Width = (int)(Texture.Width * Scale);
            Height = (int)(Texture.Height * Scale);
        }

        public void Update(GameTime gameTime)
        {
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

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Camera.InView(new Rectangle((int)Location.X - Texture.Width / 2, (int)Location.Y - Texture.Height / 2, Width, Height)))
            {
                spriteBatch.Draw(Texture, Location, null, Color, Rotation, Origin, ShiningScale * 1.3f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Texture, Location, null, Color, Rotation, Origin, Scale, SpriteEffects.None, 0f);
            }
        }
    }
}
