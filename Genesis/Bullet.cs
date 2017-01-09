using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Genesis
{
    class Bullet : GameObject
    {
        public ISpaceShip SpaceShip { get; set; }
        public Space Space { get; set; }
        private int bulletPath;

        public Bullet(ISpaceShip spaceShip, Space space, Texture2D texture, Vector2 position, float scale, float rotation, Vector2 direction, float velocity, Color color)
            : base (texture, position, scale, rotation, direction, velocity, color )
        {
            SpaceShip = spaceShip;
            Space = space;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 direction = new Vector2((float)Math.Cos(Rotation),
                                    (float)Math.Sin(Rotation));
            direction.Normalize();

            Vector2 newPosition = new Vector2(Position.X + direction.X * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds), Position.Y + direction.Y * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds));

            Position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            if (Position.X >= 0 && Position.Y >= 0 && Position.X < Space.Width && Position.Y < Space.Height)
            {
                Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
                Color fadingColor = Color;

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, Space.Camera.getTransformation(graphics));
                spriteBatch.Draw(Texture, Position, null, Color, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), 0.3f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Texture, Position, null, Color, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
                spriteBatch.End();

                for (int i = 0; i < 10; i++)
                {
                    Vector2 direction = new Vector2((float)Math.Cos(Rotation),
                                    (float)Math.Sin(Rotation));
                    direction.Normalize();
                    fadingColor = new Color(Color.R, Color.G, Color.B, fadingColor.A - 20);
                    Vector2 newLocation = Position - direction * (Velocity * i/200);

                    if (bulletPath >= i)
                    {
                        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, Space.Camera.getTransformation(graphics));
                        spriteBatch.Draw(Texture, newLocation, null, fadingColor, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
                        spriteBatch.End();
                    }
                }
                bulletPath+=2;
            }
        }
    }
}
