using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Genesis
{
    class ParticleEngine
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
            textures.Add(Content.Load<Texture2D>("Textures/purple"));
            textures.Add(Content.Load<Texture2D>("Textures/green"));
            textures.Add(Content.Load<Texture2D>("Textures/blue"));
            textures.Add(Content.Load<Texture2D>("Textures/red"));
        }

        public Particle GenerateParticle(Texture2D texture)
        {
            float scale = 0.3f;
            Vector2 position = emitterLocation;
            Vector2 direction = new Vector2((float)random.NextDouble() * 300 - (float)random.NextDouble() * 300, (float)random.NextDouble() * 300 - (float)random.NextDouble() * 300);
            Color color = Color.White;
            float velocity = random.Next(2, 5);

            return new Particle(texture, emitterLocation, scale, 0f, direction, velocity, color);
        }

        public void GenerateParticles(int number, Vector2 location, Texture2D texture)
        {
            emitterLocation = location;
            for (int i = 0; i < number; i++)
            {
                particles.Add(GenerateParticle(texture));
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update(gameTime);
                if (particles[i].Color.A <= 0 || particles[i].Scale <= 0)
                    particles.RemoveAt(i);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Camera camera)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(spriteBatch, graphics, camera);
            }
        }
    }
}
