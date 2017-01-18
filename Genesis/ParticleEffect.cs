using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Genesis
{
    class ParticleEffect
    {
        private Random random;
        private Vector2 emitterLocation;
        private List<Particle> particles;
        public List<Texture2D> textures;
        public Destruction Destruction;


        public void LoadContent(ContentManager Content)
        {
            particles = new List<Particle>();
            random = new Random();
            textures = new List<Texture2D>();
            Destruction = new Destruction();
            textures.Add(Content.Load<Texture2D>("Textures/gray"));
            textures.Add(Content.Load<Texture2D>("Textures/orange"));
            textures.Add(Content.Load<Texture2D>("Textures/purple"));
            textures.Add(Content.Load<Texture2D>("Textures/green"));
            textures.Add(Content.Load<Texture2D>("Textures/red"));
            textures.Add(Content.Load<Texture2D>("Textures/blue"));
            textures.Add(Content.Load<Texture2D>("Textures/asteroid1"));
            textures.Add(Content.Load<Texture2D>("Textures/asteroid2"));
        }

        public virtual Particle GenerateParticle(Texture2D texture)
        {
            float scale = 0.3f;
            Vector2 position = emitterLocation;
            Vector2 direction = new Vector2((float)random.NextDouble() * 300 - (float)random.NextDouble() * 300, (float)random.NextDouble() * 300 - (float)random.NextDouble() * 300);
            Color color = Color.White;
            float velocity = random.Next(2, 5);

            return new Particle(texture, emitterLocation, scale, 0f, direction, velocity, color);
        }

        public virtual void GenerateParticles(int number, Vector2 location, Texture2D texture)
        {
            emitterLocation = location;
            for (int i = 0; i < number; ++i)
            {
                particles.Add(GenerateParticle(texture));
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            for (int i = 0; i < particles.Count; ++i)
            {
                particles[i].Transparency -= 5;
                particles[i].Color = new Color(255, 255, 255, particles[i].Transparency);
                particles[i].Position += particles[i].Direction * (particles[i].Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                particles[i].Scale -= 0.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                particles[i].Velocity /= 1.1f;

                if (particles[i].Color.A <= 0 || particles[i].Scale <= 0)
                    particles.RemoveAt(i);
            }
        }

        public virtual void Update(GameTime gameTime, Camera camera)
        {
            Destruction.Update(gameTime, camera);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Camera camera)
        {
            for (int i = 0; i < particles.Count; ++i)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, camera.getTransformation(graphics));
                particles[i].Draw(spriteBatch, graphics, camera);
                spriteBatch.End();
            }
        }
    }
}
