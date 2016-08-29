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
        public Vector2 Location { get; set; }
        public float Velocity { get; set; }
        public Vector2 Dir { get; set; }
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public Space Space { get; set; }
        public float Scale { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        private int bulletPath;

        public Bullet(Space space, Vector2 location, float velocity, Vector2 dir, Texture2D texture, Color color)
        {
            Space = space;
            Location = location;
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

            Location -= Dir * Velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Location.X >= 0 && Location.Y >= 0 && Location.X < Space.Width && Location.Y < Space.Height)
            {
                Rectangle destinationRectangle = new Rectangle((int)Location.X, (int)Location.Y, Texture.Width, Texture.Height);
                Color fadingColor = Color;

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
                spriteBatch.Draw(Texture, Location, null, Color, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
                spriteBatch.End();

                for (int i = 0; i < 10; i++)
                {
                    fadingColor = new Color(Color.R, Color.G, Color.B, fadingColor.A - 20);
                    Vector2 newLocation = Location + Dir * (Velocity * i/2);

                    if (bulletPath >= i)
                    {
                        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
                        spriteBatch.Draw(Texture, newLocation, null, fadingColor, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
                        spriteBatch.End();
                    }
                }
                bulletPath+=2;
            }
        }
    }
}
