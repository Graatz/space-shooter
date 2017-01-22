using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Genesis
{
    class ParticleHandler
    {
        public Dictionary<String, Texture2D> ColorTextures;
        public List<Texture2D> AsteroidTextures;
        public Destruction Destruction;
        public SmokeEffect SmokeEffect;


        public void LoadContent(ContentManager Content)
        {
            Destruction = new Destruction();
            SmokeEffect = new SmokeEffect();

            ColorTextures = new Dictionary<String, Texture2D>();
            AsteroidTextures = new List<Texture2D>();

            ColorTextures.Add("gray", Content.Load<Texture2D>("Textures/gray"));
            ColorTextures.Add("orange", Content.Load<Texture2D>("Textures/orange"));
            ColorTextures.Add("purple", Content.Load<Texture2D>("Textures/purple"));
            ColorTextures.Add("green", Content.Load<Texture2D>("Textures/green"));
            ColorTextures.Add("red", Content.Load<Texture2D>("Textures/red"));
            ColorTextures.Add("blue", Content.Load<Texture2D>("Textures/blue"));

            AsteroidTextures.Add(Content.Load<Texture2D>("Textures/asteroid1"));
            AsteroidTextures.Add(Content.Load<Texture2D>("Textures/asteroid2"));
        }


        public void Update(GameTime gameTime, Camera camera)
        {
            Destruction.Update(gameTime, camera);
            SmokeEffect.Update(gameTime, camera);
        }
    }
}