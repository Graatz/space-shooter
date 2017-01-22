using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Genesis
{
    class SmokeEffect
    {
        private Random random;
        private Vector2 emitterLocation;
        private List<Particle> particles;

        public SmokeEffect()
        {
            particles = new List<Particle>();
            random = new Random();
        }

        private Particle GenerateParticle(Texture2D texture)
        {
            float scale = 0.1f;
            Vector2 position = emitterLocation;
            Vector2 direction = new Vector2((float)random.NextDouble() * 300 - (float)random.NextDouble() * 300, (float)random.NextDouble() * 300 - (float)random.NextDouble() * 300);
            Color color = Color.White;
            float velocity = random.Next(1, 1);

            return new Particle(texture, emitterLocation, scale, 0f, direction, velocity, color);
        }

        public void GenerateParticles(int number, Vector2 location, Texture2D texture)
        {
            emitterLocation = location;
            for (int i = 0; i < number; ++i)
            {
                particles.Add(GenerateParticle(texture));
            }
        }

        public void Update(GameTime gameTime, Camera camera)
        {
            for (int i = 0; i < particles.Count; ++i)
            {
                particles[i].Transparency -= 15;
                particles[i].Color = new Color(255, 255, 255, particles[i].Transparency);
                particles[i].Position += particles[i].Direction * (particles[i].Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                particles[i].Scale -= 0.1f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                particles[i].Velocity /= 1.1f;

                if (particles[i].Color.A <= 0 || particles[i].Scale <= 0)
                    particles.RemoveAt(i);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Camera camera)
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
