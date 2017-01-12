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

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Camera camera)
        {          
            spriteBatch.Draw(Texture, Position, null, Color, Rotation, Origin, Scale, SpriteEffects.None, 0f);
        }
    }
}
