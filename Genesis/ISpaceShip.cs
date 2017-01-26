using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Genesis
{
    interface ISpaceShip
    {
        Vector2 Position { get; set; }
        Statistics Statistics { get; set; }
        Weapon Weapon { get; set; }
        List<ISpaceShip> Enemies { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        float Rotation { get; set; }
        float Velocity { get; set; }

        void Update(GameTime gameTime, Camera camera);

        void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Camera camera);
    }
}
