using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GJExperience.PingPongGame.V1.Core
{
    public class Bat
    {
        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 Direction { get; set; }

        public float BatVelocity { get; set; }

        public float Velocity { get; set; }

        public Bat(Game game, Vector2 position, string ImagePath, float velocity = 1.0f)
        {
            Texture = game.Content.Load<Texture2D>(ImagePath);
            Position = position;
            BatVelocity = velocity;
            Direction = new Vector2(0.0f, 0.0f);
            Velocity = 150f;
        }

        public void UpdatePosition(float directionY)
        {
            if (this.Position.Y == 380)
                directionY = -1.0f;
            else if (this.Position.Y == 0)
                directionY = 1.0f;

            this.Position = new Vector2(this.Position.X, this.Position.Y + directionY);            
        }

        public Rectangle GetBounding()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)Texture.Width, (int)Texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            Position += Direction * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
