using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Genesis
{
    class Planet
    {
        public Vector2 Location { get; set; }
        public Color Color { get; set; }
        public float Scale { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public PlanetModel PlanetModel { get; set; }

        public Planet(PlanetModel planetModel, Vector2 location, float scale, Color color, Vector2 origin)
        {
            PlanetModel = planetModel;
            Origin = origin;
            Location = location;
            Scale = scale;
            Color = color;
            Width = (int)(PlanetModel.Texture.Width * Scale);
            Height = (int)(PlanetModel.Texture.Height * Scale);
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if (camera.InView(new Rectangle((int)Location.X, (int)Location.Y, Width, Height)))
            {
                spriteBatch.Draw(PlanetModel.Texture, Location, null, Color, Rotation, Origin, Scale, SpriteEffects.None, 0f);
            }
        }
    }
}
