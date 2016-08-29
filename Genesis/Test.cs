using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Genesis
{
    class Test : Game
    {
        private Random random;
        private Vector2 emitterLocation;
        private List<Particle> particles;
        public List<Texture2D> textures;

        public void LoadContent(ContentManager Content)
        {
            particles = new List<Particle>();
            random = new Random();
            textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>("Textures/orange"));
        }

        public Particle GenerateParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = emitterLocation;
            Vector2 velocity = new Vector2((float)random.NextDouble() * 3 - (float)random.NextDouble() * 3, (float)random.NextDouble() * 3 - (float)random.NextDouble() * 3);
            float angle = 0;
            float angularVelocity = (float)random.NextDouble();
            Color color = Color.White;
            int ttl = 20 + random.Next(40);

            return new Particle(texture, emitterLocation, velocity, angle, angularVelocity, ttl, color);
        }

        public void GenerateParticles(int number, Vector2 location)
        {
            emitterLocation = location;
            for (int i = 0; i < number; i++)
            {
                particles.Add(GenerateParticle());
            }
        }

        public void Update()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update();
                if (particles[i].Color.A <= 0 || particles[i].Scale <= 0)
                    particles.RemoveAt(i);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(spriteBatch);
            }
        }
    }
}
