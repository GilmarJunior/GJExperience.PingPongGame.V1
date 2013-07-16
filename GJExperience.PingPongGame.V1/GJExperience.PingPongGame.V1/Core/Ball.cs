using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GJExperience.PingPongGame.V1.Core
{
    public class Ball
    {
        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 Direction { get; set; }

        public float Velocity { get; set; }

        public Ball(Game game, Vector2 position)
        {
            Texture = game.Content.Load<Texture2D>(@"Itens Fisicos\ball");
            Position = position;
            Direction = new Vector2(-3.0f, 0.0f);
            Velocity = 150f;
        }

        public Rectangle GetBounding()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)Texture.Width, (int)Texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            if (Position.Y > 460.0f || Position.Y < 0.0f)
                Direction = new Vector2(Direction.X, -Direction.Y);

            Position += Direction * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
