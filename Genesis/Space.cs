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
        public List<Star> Nebulas { get; set; }
        public List<Star> Planets { get; set; }
        public List<Texture2D> Textures { get; set; }
        public List<Texture2D> Objects { get; set; }
        public Camera Camera { get; set; }
        private Random random;

        public struct Star
        {
            public Texture2D Texture { get; set; }
            public Vector2 Location { get; set; }
            public Color Color { get; set; }
            public float Scale { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }

            public Star(Texture2D texture, Vector2 location, float scale, Color color)
            {
                Texture = texture;
                Location = location;
                Scale = scale;
                Color = color;
                Width = (int)(Texture.Width * Scale);
                Height = (int)(Texture.Height * Scale);
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
            Objects = new List<Texture2D>();

            Stars = new List<Star>();
            Nebulas = new List<Star>();
            Planets = new List<Star>();

            Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
            Color[] colors = new Color[1];
            colors[0] = Color.White;
            pixel.SetData(colors);

            Textures.Add(pixel);
            Textures.Add(Content.Load<Texture2D>("Textures/blue"));

            Objects.Add(Content.Load<Texture2D>("Textures/moon"));
            Objects.Add(Content.Load<Texture2D>("Textures/mars"));
            Objects.Add(Content.Load<Texture2D>("Textures/pluton"));
            Objects.Add(Content.Load<Texture2D>("Textures/sun"));
        }

        public void GenerateStars()
        {
            // NEBULAS
            for (int i = 0; i < 500; i++)
            {
                Texture2D nebulaTexture = Textures[1];
                Vector2 nebulaLocation = new Vector2(random.Next(Width), random.Next(Height));
                float scale = 1.8f;
                Star nebula = new Star(nebulaTexture, nebulaLocation, scale, new Color(255, 255, 255, random.Next(30, 55)));
                Nebulas.Add(nebula);
            }

            // PLANETS
            for (int i = 0; i < 5; i++)
            {
                bool repeat;
                do
                {
                    repeat = false;
                    int numberOfPlanets = Planets.Count;

                    float scale = 1.0f / random.Next(4);
                    Texture2D planetTexture = Objects[random.Next(Objects.Count)];
                    Vector2 planetLocation = new Vector2(random.Next((int)(planetTexture.Width * scale), Width - (int)(planetTexture.Width * scale)), random.Next((int)(planetTexture.Height * scale), Height - (int)(planetTexture.Height * scale)));
                    Star planet = new Star(planetTexture, planetLocation, scale, Color.White);

                    if (numberOfPlanets >= 1)
                    {
                        for (int j = 0; j < numberOfPlanets; j++)
                        {
                            Rectangle object1 = new Rectangle((int)planet.Location.X, (int)planet.Location.Y, (int)planet.Width, (int)planet.Height);
                            Rectangle object2 = new Rectangle((int)Planets[j].Location.X, (int)Planets[j].Location.Y, (int)Planets[j].Width, (int)Planets[j].Height);
                            if (object1.Intersects(object2))
                            {
                                repeat = true;
                                break;
                            }
                            else if (j == numberOfPlanets - 1)
                            {
                                Planets.Add(planet);
                                repeat = false;
                            }
                        }
                    }
                    else
                        Planets.Add(planet);                  
                } while (repeat == true);
            }
            Debug.WriteLine(Planets.Count);

            // STARS
            for (int i = 0; i < 2000; i++)
            {
                Texture2D starTexture = Textures[0];
                Vector2 starLocation = new Vector2(random.Next(Width), random.Next(Height));
                float scale = 1.0f;
                Star star = new Star(starTexture, starLocation, scale, new Color(255, 255, 255, random.Next(10, 255)));
                Stars.Add(star);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, Camera.getTransformation(graphics));

            for (int i = 0; i < Stars.Count; i++)
            {
                if (Camera.InView(new Rectangle((int)Stars[i].Location.X, (int)Stars[i].Location.Y, Stars[i].Width, Stars[i].Height)))
                {
                    Rectangle destinationRectangle = new Rectangle((int)Stars[i].Location.X, (int)Stars[i].Location.Y, Stars[i].Texture.Width, Stars[i].Texture.Height);
                    spriteBatch.Draw(Stars[i].Texture, Stars[i].Location, null, Stars[i].Color, 0f, Vector2.Zero, Stars[i].Scale, SpriteEffects.None, 0f);
                }
            }

            for (int i = 0; i < Nebulas.Count; i++)
            {
                if (Camera.InView(new Rectangle((int)Nebulas[i].Location.X, (int)Nebulas[i].Location.Y, Nebulas[i].Width, Nebulas[i].Height)))
                {
                    Rectangle destinationRectangle = new Rectangle((int)Nebulas[i].Location.X, (int)Nebulas[i].Location.Y, Nebulas[i].Texture.Width, Nebulas[i].Texture.Height);
                    spriteBatch.Draw(Nebulas[i].Texture, Nebulas[i].Location, null, Nebulas[i].Color, 0f, new Vector2(Nebulas[i].Width / 2, Nebulas[i].Height / 2), Nebulas[i].Scale, SpriteEffects.None, 0f);
                }
            }

            for (int i = 0; i < Planets.Count; i++)
            {
                if (Camera.InView(new Rectangle((int)Planets[i].Location.X, (int)Planets[i].Location.Y, Planets[i].Width, Planets[i].Height)))
                {
                    Rectangle destinationRectangle = new Rectangle((int)Planets[i].Location.X, (int)Planets[i].Location.Y, Planets[i].Width, Planets[i].Texture.Height);
                    spriteBatch.Draw(Planets[i].Texture, Planets[i].Location, null, Color.White, 0f, Vector2.Zero, Planets[i].Scale, SpriteEffects.None, 0f);
                }
            }

            spriteBatch.End();
        }
    }
}
