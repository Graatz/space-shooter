using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Genesis
{
    interface IGameState
    {
         void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice);
         void Update(GameTime gameTime, Game game, Genesis genesis);
    }
}
