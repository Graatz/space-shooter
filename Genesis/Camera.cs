using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis
{
    class Camera
    {
        private Matrix transform;
        public Matrix Transform
        {
            get
            {
                return transform;
            }
        }

        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        protected float rotation;
        protected float zoom;

        public Camera()
        {
            zoom = 1f;
            rotation = 0.0f;
            position = Vector2.Zero;
        }

        public Matrix getTransformation(GraphicsDevice graphicsDevice)
        {
            transform = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 1)) *
                        Matrix.CreateRotationZ(rotation) *
                        Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                        Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.X * 0.5f, graphicsDevice.Viewport.Y * 0.5f, 0));

            return transform;
        }

        public Vector2 transformPosition(Vector2 position)
        {
            Vector2 transformedPosition = new Vector2(position.X, position.Y);
            Matrix transform = Matrix.Invert(Transform);
            Vector2.Transform(ref transformedPosition, ref transform, out transformedPosition);

            return position;
        }

        public bool InView(Rectangle object1)
        {
            if (object1.Location.X + object1.Width >= Position.X && object1.Location.X - object1.Width <= Position.X + Genesis.Width && object1.Location.Y + object1.Height >= Position.Y && object1.Location.Y - object1.Height <= Position.Y + Genesis.Height)
                return true;
            else
                return false;
        }

        public void SetCameraPosition(Vector2 position)
        {
            this.position = position;
        }

        public void MoveCamera(Vector2 velocity)
        {
            //if (Position.X + velocity.X > 0 && Position.X + velocity.X + Genesis.Width < 5000 && Position.Y + velocity.Y > 0 && Position.Y + velocity.Y + Genesis.Height < 4000)
                position += velocity;
        }
    }
}
