using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Genesis
{
    class Space
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Planet> Nebulas { get; set; }
        public List<Planet> Planets { get; set; }
        public List<Vortex> Vortexes { get; set; }
        public List<Texture2D> Textures { get; set; }
        public List<Texture2D> Objects { get; set; }
        public PlanetModel PlanetModel { get; set; }
        public Spawner Spawner { get; set; }
        private Random random;
        public float vortexRotation = 0;

        public Space(Camera camera, int width, int height)
        {
            Width = width;
            Height = height;

            camera.SetCameraPosition(new Vector2(Width / 2 - Genesis.Width / 2, Height / 2 - Genesis.Height / 2));
        }

        public void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            random = new Random();

            Textures = new List<Texture2D>();
            Objects = new List<Texture2D>();

            Nebulas = new List<Planet>();
            Planets = new List<Planet>();
            Vortexes = new List<Vortex>();

            Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
            Color[] colors = new Color[1];
            colors[0] = Color.White;
            pixel.SetData(colors);

            Textures.Add(pixel);

            Textures.Add(Content.Load<Texture2D>("Textures/blue"));
            Textures.Add(Content.Load<Texture2D>("Textures/purple"));
            Textures.Add(Content.Load<Texture2D>("Textures/green"));
            Textures.Add(Content.Load<Texture2D>("Textures/gray"));
            Textures.Add(Content.Load<Texture2D>("Textures/orange"));

            Objects.Add(Content.Load<Texture2D>("Textures/moon"));
            Objects.Add(Content.Load<Texture2D>("Textures/mars"));
            Objects.Add(Content.Load<Texture2D>("Textures/pluton"));
            Objects.Add(Content.Load<Texture2D>("Textures/redGiant"));
            Objects.Add(Content.Load<Texture2D>("Textures/wir"));

            Generate();
        }

        public void Generate()
        {
            int numberOfVortexes = random.Next(1, 10);
            for (int i = 0; i < numberOfVortexes; i++)
                GenerateVortex();

            GeneratePlanets();
            GenerateNebulas();
        }

        public void GeneratePlanets()
        {         
            for (int i = 0; i < (Width + Height) / 1000; ++i)
            {
                bool repeat;
                do
                {
                    repeat = false;
                    int numberOfPlanets = Planets.Count;
                    float scale = (float)random.NextDouble() * (1.2f - 0.7f) + 0.7f;
                    int randomTexture = random.Next(Objects.Count - 1);
                    Texture2D planetTexture = Objects[randomTexture];
                    Vector2 planetLocation = new Vector2(random.Next((int)(planetTexture.Width * scale), 
                        Width - (int)(planetTexture.Width * scale)), 
                        random.Next((int)(planetTexture.Height * scale), 
                        Height - (int)(planetTexture.Height * scale)));
                    PlanetModel = new PlanetModel();
                    PlanetModel.setTexture(planetTexture);
                    Planet planet = new Planet(PlanetModel, planetLocation, scale, Color.White, Vector2.Zero);

                    if (numberOfPlanets >= 1)
                    {
                        for (int j = 0; j < numberOfPlanets; ++j)
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
            PlanetModel = new PlanetModel();
            Texture2D nebulaTexture = Textures[random.Next(1, 6)];
            PlanetModel.setTexture(nebulaTexture);
            for (int i = 0; i < (Width + Height) / (250000 / (Width + Height)); ++i)
            {
                Vector2 nebulaLocation = new Vector2(random.Next(Width), random.Next(Height));
                float scale = 1.8f;
                Planet nebula = new Planet(PlanetModel, nebulaLocation, scale, new Color(255, 255, 255, random.Next(30, 55)), new Vector2(nebulaTexture.Width/2, nebulaTexture.Height/2));
                Nebulas.Add(nebula);
            }
        }

        public void GenerateVortex()
        {
            float rotationVelocity = (float)random.NextDouble() * (0.01f - 0.003f) + 0.003f;
            float scale = (float)random.Next(1, 2);
            Texture2D planetTexture = Objects[4];
            Vector2 planetLocation = new Vector2(random.Next((int)(planetTexture.Width * scale), Width - (int)(planetTexture.Width * scale)), random.Next((int)(planetTexture.Height * scale), Height - (int)(planetTexture.Height * scale)));
            Vortex Vortex = new Vortex(planetTexture, planetLocation, scale, Color.White, new Vector2(planetTexture.Width / 2, planetTexture.Height / 2), rotationVelocity);

            Vortexes.Add(Vortex);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < Vortexes.Count; ++i)
            {
                Vortexes[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, camera.getTransformation(graphics));

            for (int i = 0; i < Nebulas.Count; ++i)
            {
                Nebulas[i].Draw(spriteBatch, camera);
            }

            for (int i = 0; i < Planets.Count; ++i)
            {
                Planets[i].Draw(spriteBatch, camera);
            }

            for (int i = 0; i < Vortexes.Count; ++i)
            {
                Vortexes[i].Draw(spriteBatch, camera);
            }

            spriteBatch.End();
        }
    }
}
