using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis
{
    class GameObject
    {
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

        public GameObject(Vector2 position, int width, int height)
        {
            Position = position;
            Width = width;
            Height = height;
        }
    }
}
