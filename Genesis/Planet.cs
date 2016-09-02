using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis
{
    class Planet
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

        public Planet(Camera camera, Texture2D texture, Vector2 location, float scale, Color color, Vector2 origin)
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

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Camera.InView(new Rectangle((int)Location.X, (int)Location.Y, Width, Height)))
            {
                spriteBatch.Draw(Texture, Location, null, Color, Rotation, Origin, Scale, SpriteEffects.None, 0f);
            }
        }
    }
}
