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
    class Particle : GameObject
    {
        public byte Transparency;

        public Particle(Texture2D texture, Vector2 position, float scale, float rotation, Vector2 direction, float velocity, Color color)
            : base (texture, position, scale, rotation, direction, velocity, color)
        {
            Transparency = 255;
        }

        public void Update(GameTime gameTime)
        {
            Transparency -= 5;
            Color = new Color(255, 255, 255, Transparency);
            Position += Direction * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Scale -= 0.6f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, camera.getTransformation(graphics));
            spriteBatch.Draw(Texture, Position, null, Color, Rotation, Origin, Scale, SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
