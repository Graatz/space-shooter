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
    class Asteroid : GameObject
    {

        public Asteroid(Texture2D texture, Vector2 position, float rotation, float scale, float velocity, Vector2 direction, Color color)
            : base(texture, position, scale, rotation, direction, velocity, color)
        {

        }

        public void Update(GameTime gameTime)
        {
            Position += Direction * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Rotation += 0.01f * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, camera.getTransformation(graphics));
            spriteBatch.Draw(Texture, Position, null, Color, Rotation, Origin, Scale, SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
