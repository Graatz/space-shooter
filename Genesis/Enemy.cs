using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis
{
    class Enemy
    {
        public Texture2D Texture { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2 Position { get; set; }

        private Player player;
        public Player Player
        {
            get
            {
                return player;
            }
            set
            {
                player = value;
            }
        }

        private Space space;
        public Space Space
        {
            get
            {
                return space;
            }
            set
            {
                space = value;
            }
        }

        private float velocity;
        public float Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }

        public Enemy(Space space, Player player, GraphicsDevice device, Texture2D texture, Vector2 position)
        {
            Space = space;
            Player = player;
            Position = position;
            Texture = texture;
            Scale = 0.3f;
            Velocity = 3;
            Width = (int)(Texture.Width * Scale);
            Height = (int)(Texture.Height * Scale);
        }

        public void Update()
        {
            Vector2 pos = Position - Player.Position;
            pos.Normalize();
            Position -= pos * Velocity;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Space.Camera.getTransformation(graphics));
            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, new Vector2(Width / 2, Height / 2), Scale, SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
