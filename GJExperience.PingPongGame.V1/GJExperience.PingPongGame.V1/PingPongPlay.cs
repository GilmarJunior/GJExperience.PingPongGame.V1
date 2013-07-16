using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GJExperience.PingPongGame.V1.Core;

namespace GJExperience.PingPongGame.V1
{
    public class PingPongPlay : Microsoft.Xna.Framework.Game
    {
        Ball Ball;
        Bat BatOne, BatTwo;

        Score RecentPlayScore;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 defaultPosition;

        private static List<string> CurrentCombo;

        public PingPongPlay()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ResetGame();
        }

        private void ResetGame()
        {
            CurrentCombo = new List<string>();
            defaultPosition = new Vector2(393, 313);
            Ball = new Ball(this, defaultPosition);
            BatOne = new Bat(this, new Vector2(10.0f, 200.0f), "Itens Fisicos\\bat", 3.0f);
            BatTwo = new Bat(this, new Vector2(765.0f, 200.0f), "Itens Fisicos\\bats", 3.0f);
            RecentPlayScore = new Score(this);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                KeyboardState keyState = Keyboard.GetState();

                //Pressionar ESC para sair
                if (keyState.IsKeyDown(Keys.Escape))
                    this.Exit();

                if (keyState.IsKeyDown(Keys.R))
                    ResetGame();


                //Movimentar Bastões
                if (keyState.IsKeyDown(Keys.W))
                    BatOne.UpdatePosition(-BatOne.BatVelocity);
                else if (keyState.IsKeyDown(Keys.S))
                    BatOne.UpdatePosition(BatOne.BatVelocity);

                if (keyState.IsKeyDown(Keys.Up))
                    BatTwo.UpdatePosition(-BatTwo.BatVelocity);
                else if (keyState.IsKeyDown(Keys.Down))
                    BatTwo.UpdatePosition(BatTwo.BatVelocity);


                //Alterar Placar
                if (Ball.Position.X < 0.0f)
                {
                    RecentPlayScore.ScorePlayerOne++;
                    Ball.Position = defaultPosition;
                }
                else if (Ball.Position.X > 800.0f)
                {
                    RecentPlayScore.ScorePlayerTwo++;
                    Ball.Position = defaultPosition;
                }

                //Colisão entre bola e bastão
                if (Ball.GetBounding().Intersects(BatOne.GetBounding()))
                {
                    Vector2 cBall = new Vector2(Ball.GetBounding().Center.X, Ball.GetBounding().Center.Y);
                    cBall.Normalize();

                    Vector2 cBat = new Vector2(BatOne.GetBounding().Center.X, BatTwo.GetBounding().Center.Y);
                    cBat.Normalize();

                    double angDir = Math.Atan2(cBall.Y - cBat.Y, cBall.X - cBat.X);

                    Ball.Direction = new Vector2((float)Math.Cos(angDir), (float)Math.Sin(angDir));
                }
                else if (Ball.GetBounding().Intersects(BatTwo.GetBounding()))
                {
                    Vector2 cBall = new Vector2(Ball.GetBounding().Center.X, Ball.GetBounding().Center.Y);
                    cBall.Normalize();

                    Vector2 cBat = new Vector2(BatTwo.GetBounding().Center.X, BatTwo.GetBounding().Center.Y);
                    cBat.Normalize();

                    double angDir = Math.Atan2(cBall.Y - cBat.Y, cBall.X - cBat.X);
                    Ball.Direction = new Vector2(-Ball.Direction.X, (float)Math.Sin(angDir));
                }

                VerificarCombo(keyState);

                Ball.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public void VerificarCombo(KeyboardState currentKeyState)
        {
            var keys = currentKeyState.GetPressedKeys().ToList();
            if (keys.Count > 0)
            {
                if (keys[0].ToString() == "Left" || CurrentCombo.Contains("Left"))
                {
                    if (!CurrentCombo.Contains(keys[0].ToString()))
                        CurrentCombo.Add(keys[0].ToString());

                    List<string> UltraButtons = new List<string>(){
                        "Left","Right","Up", "B", "A", "C"
                    };

                    bool isCombo = true;
                    if (CurrentCombo.Count == 6)
                    {
                        foreach (string item in UltraButtons.ToList())
                        {
                            if (!CurrentCombo.Contains(item))
                                isCombo = false;
                        }
                        if (isCombo)
                            UltraButtons.Add("ULTRA!!");
                    }
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            Ball.Draw(spriteBatch);
            BatOne.Draw(spriteBatch);
            BatTwo.Draw(spriteBatch);
            RecentPlayScore.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

