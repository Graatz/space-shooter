using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Genesis
{
    class PlanetModel
    {
        public Texture2D Texture { get; set; }

        public PlanetModel()
        {
            Texture = null;
        }

        public void setTexture(Texture2D texture)
        {
            Texture = texture;
        }
    }
}
