using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Genesis
{
    interface ISpaceShip
    {
        Weapon Weapon { get; set; }
        List<ISpaceShip> Enemies { get; set; }
        Vector2 Position { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        Statistics Statistics { get; set; }
        float Rotation { get; set; }
        float Velocity { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics);
    }
}
