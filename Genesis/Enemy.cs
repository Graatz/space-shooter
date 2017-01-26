using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace Genesis
{
    class Enemy : GameObject, ISpaceShip
    {
        private Random random;
        public Spawner Spawner { get; set; }
        public Weapon Weapon { get; set; }
        public List<ISpaceShip> Enemies { get; set; }
        public Space Space { get; set; }
        public Vector2 Target { get; set; }
        public Vector2 InitialPosition { get; set; }
        public Statistics Statistics { get; set; }
        public float TargetRotation { get; set; }
        public Player Player { get; set; }
        public bool aggro { get; set; }

        public Enemy(Spawner spawner, Player player, ParticleHandler ParticleHandler, Space space, Texture2D texture, Vector2 position, Camera camera, float rotation, float scale, float velocity, Vector2 direction, Vector2 target)
             : base (texture, position, scale, rotation, direction, velocity, Color.White)
        {
            random = new Random();
            Spawner = spawner;
            Player = player;
            Space = space;
            InitialPosition = Position;
            Direction = direction;
            Target = target;

            Statistics = new Statistics(1, 0.9f / Scale, 800, 30);
            Enemies = new List<ISpaceShip>();
            Enemies.Add(Player);

            Weapon = new Weapon(this, ParticleHandler, Space, ParticleHandler.ColorTextures["red"], 1500f, 0.04f, 0);

            aggro = false;
            TargetRotation = (float)Math.Round(Math.Atan2(Target.Y - Position.Y, Target.X - Position.X), 1);
        }

        public void Move(GameTime gameTime, Camera camera)
        {
            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            Direction.Normalize();
            Position = new Vector2(Position.X + Direction.X * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds), Position.Y + Direction.Y * (Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds));

            if (camera.InView(new Rectangle((int)Position.X, (int)Position.Y, Width, Height)))
            {
                Weapon.Shoot(gameTime);
                aggro = true;
            }
            else
                aggro = false;

            if (aggro)
            {
                TargetRotation = (float)Math.Round(Math.Atan2(Player.Position.Y - Position.Y, Player.Position.X - Position.X), 1);
            }
            else
            {
                if (InitialPosition.X > Target.X)
                {
                    if (Position.X <= Target.X)
                    {
                        InitialPosition = Position;
                        Target = new Vector2(random.Next(0, Space.Width), random.Next(0, Space.Height));
                        TargetRotation = (float)Math.Round(Math.Atan2(Target.Y - Position.Y, Target.X - Position.X), 1);
                    }
                }
                else if (InitialPosition.X < Target.X)
                {
                    if (Position.X >= Target.X)
                    {
                        InitialPosition = Position;
                        Target = new Vector2(random.Next(0, Space.Width), random.Next(0, Space.Height));
                        TargetRotation = (float)Math.Round(Math.Atan2(Target.Y - Position.Y, Target.X - Position.X), 1);
                    }
                }
            }

            if (TargetRotation > Math.Round(Rotation, 1))
            {
                Rotation += 0.02f;
            }
            else if (TargetRotation < Math.Round(Rotation, 1))
            {
                Rotation -= 0.02f;
            }
        }
        
        public bool Destroyed()
        {
            if (Statistics.Health <= 0)
                return true;
            else
                return false;
        }

        public void Update(GameTime gameTime, Camera camera)
        {
            Move(gameTime, camera);
            Weapon.Update(gameTime, camera);
            if (Destroyed())
            {
                for (int i = 0; i < Enemies.Count; i++)
                {
                    Spawner.ParticleHandler.Destruction.GenerateParticles(150, Position, Texture);
                    Enemies[i].Enemies.Remove(this);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics, Camera camera)
        {
            if (camera.InView(new Rectangle((int)Position.X, (int)Position.Y, Width, Height)))
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, camera.getTransformation(graphics));
                spriteBatch.Draw(Texture, Position, null, Color, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), Scale, SpriteEffects.None, 0f);
                spriteBatch.End();
            }
        }
    }
}
