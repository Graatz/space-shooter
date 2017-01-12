using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Genesis
{
    class PlayerUI
    {
        public Statistics Statistics { get; set; }
        public Texture2D HealthTexture { get; set; }
        public Texture2D EnergyTexture { get; set; }

        public PlayerUI(Statistics statistics)
        {
            Statistics = statistics;
        }

        public void LoadContent()
        {

        }

        private void DrawHealth(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            HealthTexture = new Texture2D(graphics, (Statistics.Health * 3), 10);

            Color[] data = new Color[(Statistics.Health * 3) * 10];

            for (int i = 0; i < data.Length; ++i)
                data[i] = Color.Red;

            HealthTexture.SetData(data);

            spriteBatch.Begin();
            spriteBatch.Draw(HealthTexture, new Vector2(0, 0), Color.White);
            spriteBatch.End();
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            DrawHealth(spriteBatch, graphics);
        }
    }
}
