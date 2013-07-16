using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GJExperience.PingPongGame.V1.Core
{
    public class Score
    {
        public int ScorePlayerOne { get; set; }

        public int ScorePlayerTwo { get; set; }

        public SpriteFont ScoreFont { get; set; }

        public Ball CurrentBall { get; set; }

        public Score(Game game)
        {
            ScoreFont = game.Content.Load<SpriteFont>(@"Fonts\score");            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 playerOneTextSize = ScoreFont.MeasureString(ScorePlayerOne.ToString("000"));

            spriteBatch.DrawString(ScoreFont, ScorePlayerOne.ToString("000"), new Vector2(300, 35) - playerOneTextSize / 2, Color.White);

            Vector2 playerTwoTextSize = ScoreFont.MeasureString(ScorePlayerTwo.ToString("000"));

            spriteBatch.DrawString(ScoreFont, ScorePlayerTwo.ToString("000"), new Vector2(500, 35) - playerTwoTextSize / 2, Color.White);
        }
    }
}
