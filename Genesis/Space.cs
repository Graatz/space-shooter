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
        public List<Planet> Stars { get; set; }
        public List<Planet> Nebulas { get; set; }
        public List<Planet> Planets { get; set; }
        public List<Texture2D> Textures { get; set; }
        public List<Texture2D> Objects { get; set; }
        public Camera Camera { get; set; }
        private Random random;
        public float vortexRotation = 0;

        public Space(Camera camera, int width, int height)
        {
            Camera = camera;
            Width = width;
            Height = height;
        }

        public void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            random = new Random();

            Textures = new List<Texture2D>();
            Objects = new List<Texture2D>();

            Stars = new List<Planet>();
            Nebulas = new List<Planet>();
            Planets = new List<Planet>();

            Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
            Color[] colors = new Color[1];
            colors[0] = Color.White;
            pixel.SetData(colors);

            Textures.Add(pixel);
            Textures.Add(Content.Load<Texture2D>("Textures/blue"));
            Textures.Add(Content.Load<Texture2D>("Textures/purple"));
            Textures.Add(Content.Load<Texture2D>("Textures/red"));
            Textures.Add(Content.Load<Texture2D>("Textures/green"));
            Textures.Add(Content.Load<Texture2D>("Textures/gray"));

            Objects.Add(Content.Load<Texture2D>("Textures/moon"));
            Objects.Add(Content.Load<Texture2D>("Textures/mars"));
            Objects.Add(Content.Load<Texture2D>("Textures/pluton"));
            Objects.Add(Content.Load<Texture2D>("Textures/wir"));
            Objects.Add(Content.Load<Texture2D>("Textures/sun"));

            Generate();
        }

        public void Generate()
        {
            GenerateSun();
            GeneratePlanets();
            GenerateNebulas();
            GenerateStars();
        }

        public void GeneratePlanets()
        {
            for (int i = 0; i < 5; i++)
            {
                bool repeat;
                do
                {
                    repeat = false;
                    int numberOfPlanets = Planets.Count;

                    float scale = 1.0f / (float)(random.NextDouble() * (1.0 - 4.0) + 4.0);
                    int randomTexture = random.Next(Objects.Count - 2);
                    Texture2D planetTexture = Objects[randomTexture];
                    Vector2 planetLocation = new Vector2(random.Next((int)(planetTexture.Width * scale), Width - (int)(planetTexture.Width * scale)), random.Next((int)(planetTexture.Height * scale), Height - (int)(planetTexture.Height * scale)));
                    Planet planet = new Planet(Camera, planetTexture, planetLocation, scale, Color.White, Vector2.Zero);

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
        }

        public void GenerateNebulas()
        {
            for (int i = 0; i < 2000; i++)
            {
                Texture2D nebulaTexture = Textures[1];
                Vector2 nebulaLocation = new Vector2(random.Next(Width), random.Next(Height));
                float scale = 1.8f;
                Planet nebula = new Planet(Camera, nebulaTexture, nebulaLocation, scale, new Color(255, 255, 255, random.Next(30, 55)), new Vector2(nebulaTexture.Width/2, nebulaTexture.Height/2));
                Nebulas.Add(nebula);
            }
        }

        public void GenerateSun()
        {
            float scale = 2.0f;
            Texture2D planetTexture = Objects[4];
            Vector2 planetLocation = new Vector2(random.Next((int)(planetTexture.Width * scale), Width - (int)(planetTexture.Width * scale)), random.Next((int)(planetTexture.Height * scale), Height - (int)(planetTexture.Height * scale)));
            Planet planet = new Planet(Camera, planetTexture, planetLocation, scale, Color.White, Vector2.Zero);

            Planets.Add(planet);
        }

        public void GenerateStars()
        {
            for (int i = 0; i < 6000; i++)
            {
                bool repeat;
                do
                {
                    repeat = false;
                    int numberOfPlanets = Planets.Count;

                    float scale = 1.0f;
                    Texture2D starTexture = Textures[0];
                    Vector2 starLocation = new Vector2(random.Next(Width), random.Next(Height));
                    Planet star = new Planet(Camera, starTexture, starLocation, scale, new Color(255, 255, 255, random.Next(10, 255)), Vector2.Zero);

                    if (numberOfPlanets >= 1)
                    {
                        for (int j = 0; j < numberOfPlanets; j++)
                        {
                            Rectangle object1 = new Rectangle((int)star.Location.X, (int)star.Location.Y, (int)star.Width, (int)star.Height);
                            Rectangle object2 = new Rectangle((int)Planets[j].Location.X, (int)Planets[j].Location.Y, (int)Planets[j].Width, (int)Planets[j].Height);
                            if (object1.Intersects(object2))
                            {
                                repeat = true;
                                break;
                            }
                            else if (j == numberOfPlanets - 1)
                            {
                                Stars.Add(star);
                                repeat = false;
                            }
                        }
                    }
                    else
                        Stars.Add(star);
                } while (repeat == true);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, Camera.getTransformation(graphics));

            for (int i = 0; i < Stars.Count; i++)
            {
                Stars[i].Draw(spriteBatch);
            }

            for (int i = 0; i < Nebulas.Count; i++)
            {
                Nebulas[i].Draw(spriteBatch);
            }

            for (int i = 0; i < Planets.Count; i++)
            {
                Planets[i].Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
