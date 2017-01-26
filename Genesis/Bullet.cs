using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Genesis
{
    class Bullet : GameObject
    {
        public ISpaceShip SpaceShip { get; set; }
        private int bulletPath;

        public Bullet(ISpaceShip spaceShip, Space space, Texture2D texture, Vector2 position, float scale, float rotation, Vector2 direction, float velocity, Color color)
            : base (texture, position, scale, rotation, direction, velocity, color )
        {
            SpaceShip = spaceShip;
        }

        public void Update(GameTime gameTime)
        {
            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));

            Direction.Normalize();

            Position = new Vector2(Position.X + Direction.X * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds), 
                                   Position.Y + Direction.Y * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds));

        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Space space, Camera camera)
        {
            if (Position.X >= 0 && Position.Y >= 0 && Position.X < space.Width && Position.Y < space.Height)
            {
                Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
                Color fadingColor = Color;

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, camera.getTransformation(graphics));
                spriteBatch.Draw(Texture, Position, null, Color, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), 0.3f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Texture, Position, null, Color, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
                spriteBatch.End();

                for (int i = 0; i < 10; ++i)
                {
                    Vector2 direction = new Vector2((float)Math.Cos(Rotation),
                                    (float)Math.Sin(Rotation));
                    direction.Normalize();
                    fadingColor = new Color(Color.R, Color.G, Color.B, fadingColor.A - 20);
                    Vector2 newLocation = Position - direction * (Velocity * i/200);

                    if (bulletPath >= i)
                    {
                        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, camera.getTransformation(graphics));
                        spriteBatch.Draw(Texture, newLocation, null, fadingColor, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
                        spriteBatch.End();
                    }
                }
                bulletPath+=2;
            }
        }
    }
}
