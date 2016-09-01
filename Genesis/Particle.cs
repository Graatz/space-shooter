using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis
{
    class Particle
    {
        private Texture2D texture;
        public Vector2 position;
        private Vector2 velocity;
        private float angle;
        private float angularVelocity;
        public Color Color;
        public int ttl;
        public float Scale;
        public byte Transparency;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, float angle, float angularVelocity, int ttl, Color color)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            this.angle = angle;
            this.angularVelocity = angularVelocity;
            this.Color = color;
            this.ttl = ttl;

            Scale = 0.3f;
            Transparency = 255;
        }

        public void Update()
        {
            Transparency -= 5;
            Color.A = Transparency;
            position += velocity * 2;
            angle += angularVelocity;
            Scale -= 0.01f;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Camera camera)
        {
            //Rectangle sourceRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, camera.getTransformation(graphics));
            spriteBatch.Draw(texture, position, null, Color, 0f, new Vector2(50, 50), Scale, SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
