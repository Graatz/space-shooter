using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Genesis
{
    class FadeEffect
    {
        private byte fadeOpacity;
        private Texture2D fadeTexture;

        public FadeEffect(Texture2D texture)
        {
            fadeTexture = texture;
        }

        public void Activate()
        {
            fadeOpacity = 255;
        }

        public void Update()
        {
            if (fadeOpacity > 0)
                fadeOpacity -= 15;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Camera camera)
        {
            if (fadeOpacity > 0)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(fadeTexture, new Rectangle(0, 0, Genesis.Width, Genesis.Height), new Color(255, 255, 255, fadeOpacity));
                spriteBatch.End();
            }
        }
    }
}
