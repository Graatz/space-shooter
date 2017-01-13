using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Genesis
{
    class Planet
    {
        public Camera Camera { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Location { get; set; }
        public Color Color { get; set; }
        public float Scale { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public PlanetModel PlanetModel { get; set; }

        public Planet(Camera camera, PlanetModel planetModel, Vector2 location, float scale, Color color, Vector2 origin)
        {
            PlanetModel = planetModel;
            Origin = origin;
            Camera = camera;
            //Texture = texture;
            Location = location;
            Scale = scale;
            Color = color;
            Width = (int)(PlanetModel.Texture.Width * Scale);
            Height = (int)(PlanetModel.Texture.Height * Scale);
        }

        public Planet(Camera camera, Texture2D texture, Vector2 location, float scale, Color color, Vector2 origin)
        {
            PlanetModel = new PlanetModel();
            PlanetModel.setTexture(texture);
            Origin = origin;
            Camera = camera;
            Location = location;
            Scale = scale;
            Color = color;
            Width = (int)(PlanetModel.Texture.Width * Scale);
            Height = (int)(PlanetModel.Texture.Height * Scale);
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Camera.InView(new Rectangle((int)Location.X, (int)Location.Y, Width, Height)))
            {
                spriteBatch.Draw(PlanetModel.Texture, Location, null, Color, Rotation, Origin, Scale, SpriteEffects.None, 0f);
            }
        }
    }
}
