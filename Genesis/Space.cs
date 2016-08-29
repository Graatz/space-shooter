using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis
{
    class Space
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Star> Stars { get; set; }
        public List<Star> Stars2 { get; set; }
        public List<Star> Nebulas { get; set; }
        public List<Star> Nebulas2 { get; set; }
        public List<Texture2D> Textures { get; set; }
        private Random random;

        public struct Star
        {
            public Texture2D Texture { get; set; }
            public Vector2 Location { get; set; }
            public float Scale { get; set; }

            public Star(Texture2D texture, Vector2 location, float scale)
            {
                Texture = texture;
                Location = location;
                Scale = scale;
            }
        }

        public Space(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            random = new Random();
            Textures = new List<Texture2D>();
            Stars = new List<Star>();
            Stars2 = new List<Star>();
            Nebulas = new List<Star>();
            Nebulas2 = new List<Star>();

            Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
            Color[] colors = new Color[1];
            colors[0] = Color.White;
            pixel.SetData(colors);

            Textures.Add(pixel);
            Textures.Add(Content.Load<Texture2D>("Textures/blue"));
            Textures.Add(Content.Load<Texture2D>("Textures/sun"));
        }

        public void GenerateStars()
        {
            for (int i = 0; i < 300; i++)
            {
                Texture2D starTexture = Textures[0];
                Vector2 starLocation = new Vector2(random.Next(Width), random.Next(Height));
                float scale = 1.0f;
                Star star = new Star(starTexture, starLocation, scale);
                Stars.Add(star);
            }

            for (int i = 0; i < 300; i++)
            {
                Texture2D starTexture = Textures[0];
                Vector2 starLocation = new Vector2(random.Next(Width), random.Next(Height));
                float scale = 1f;
                Star star = new Star(starTexture, starLocation, scale);
                Stars2.Add(star);
            }

            for (int i = 0; i < 100; i++)
            {
                Texture2D starTexture = Textures[1];
                Vector2 starLocation = new Vector2(random.Next(Width), random.Next(Height));
                float scale = 1.8f;
                Star star = new Star(starTexture, starLocation, scale);
                Nebulas.Add(star);
            }

            for (int i = 0; i < 1; i++)
            {
                Texture2D starTexture = Textures[2];
                Vector2 starLocation = new Vector2(random.Next(Width), random.Next(Height));
                float scale = 2f;
                Star star = new Star(starTexture, starLocation, scale);
                Nebulas2.Add(star);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Stars.Count; i++)
            {
                Rectangle destinationRectangle = new Rectangle((int)Stars[i].Location.X, (int)Stars[i].Location.Y, Stars[i].Texture.Width, Stars[i].Texture.Height);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
                spriteBatch.Draw(Stars[i].Texture, Stars[i].Location, null, Color.White, 0f, new Vector2(50, 50), Stars[i].Scale, SpriteEffects.None, 0f);
                spriteBatch.End();
            }

            for (int i = 0; i < Stars2.Count; i++)
            {
                Rectangle destinationRectangle = new Rectangle((int)Stars[i].Location.X, (int)Stars[i].Location.Y, Stars[i].Texture.Width, Stars[i].Texture.Height);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
                spriteBatch.Draw(Stars2[i].Texture, Stars2[i].Location, null, new Color(255, 255, 255, 100), 0f, new Vector2(50, 50), Stars2[i].Scale, SpriteEffects.None, 0f);
                spriteBatch.End();
            }

            for (int i = 0; i < Nebulas.Count; i++)
            {
                Rectangle destinationRectangle = new Rectangle((int)Stars[i].Location.X, (int)Stars[i].Location.Y, Stars[i].Texture.Width, Stars[i].Texture.Height);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
                spriteBatch.Draw(Nebulas[i].Texture, Nebulas[i].Location, null, new Color(255, 255, 255, 50), 0f, new Vector2(Nebulas[i].Texture.Width / 2, Nebulas[i].Texture.Height / 2), Nebulas[i].Scale, SpriteEffects.None, 0f);
                spriteBatch.End();
            }

            for (int i = 0; i < Nebulas2.Count; i++)
            {
                Rectangle destinationRectangle = new Rectangle((int)Stars[i].Location.X, (int)Stars[i].Location.Y, Stars[i].Texture.Width, Stars[i].Texture.Height);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
                spriteBatch.Draw(Nebulas2[i].Texture, Nebulas2[i].Location, null, new Color(255, 255, 255, 255), 0f, new Vector2(Nebulas2[i].Texture.Width / 2, Nebulas2[i].Texture.Height / 2), Nebulas2[i].Scale, SpriteEffects.None, 0f);
                spriteBatch.End();
            }


        }
    }
}
