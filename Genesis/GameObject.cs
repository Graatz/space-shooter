using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis
{
    class GameObject
    {
        private Texture2D texture;
        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
            }
        }

        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        private float scale;
        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
            }
        }

        private float rotation;
        public float Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;
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

        private Vector2 origin;
        public Vector2 Origin
        {
            get
            {
                return origin;
            }
            set
            {
                origin = value;
            }
        }

        private Color color;
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        private Vector2 direction;
        public Vector2 Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        private int width;
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        private int height;
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        public GameObject(Texture2D texture, Vector2 position, float scale, float rotation, Vector2 direction, float velocity, Color color)
        {
            this.position = position;
            this.scale = scale;
            this.rotation = rotation;
            this.direction = direction;
            this.velocity = velocity;
            this.color = color;

            LoadTexture(texture, scale);
        }

        public GameObject(Vector2 position, float scale, float rotation, float velocity, Color color)
        {
            this.position = position;
            this.scale = scale;
            this.rotation = rotation;
            this.velocity = velocity;
            this.color = color;
        }

        public void LoadTexture(Texture2D texture, float scale)
        {
            this.texture = texture;
            this.width = (int)(texture.Width * scale);
            this.height = (int)(texture.Height * scale);
            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }
    }
}
