using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;

namespace BreakoutBoring
{
    class PaddleController
    {
        InputHandler input;
        Ball ball; //may should delgate to parent
        public bool wantsautopaddle; //cheat
        public Vector2 Direction { get; private set; }

        public PaddleController(Game game, Ball ball)
        {
            input = (InputHandler)game.Services.GetService(typeof(IInputHandler));
            this.Direction = Vector2.Zero;
            this.ball = ball;   //need refernce to ball to be able to lanch ball could possibly use delegate here
        }

        public void HandleInput(GameTime gametime)
        {
            this.Direction = Vector2.Zero;  //Start with no direction on each new upafet

            //No need to sum input only uses left and right
            if (input.KeyboardState.IsKeyDown(Keys.Left) || input.KeyboardState.IsKeyDown(Keys.A))
            {
                this.Direction = new Vector2(-1, 0);
            }
            if (input.KeyboardState.IsKeyDown(Keys.Right) || input.KeyboardState.IsKeyDown(Keys.D))
            {
                this.Direction = new Vector2(1, 0);
            }
            //TODO add mouse controll?

            //Up launches ball
            if (input.KeyboardState.WasKeyPressed(Keys.Up) || input.KeyboardState.IsKeyDown(Keys.W))
            {
                if (ball.State == BallState.OnPaddleStart) //Only Launch Ball is it's on paddle
                    this.ball.LaunchBall(gametime);
            }
#if DEBUG
            if (input.KeyboardState.WasKeyPressed(Keys.S) || input.KeyboardState.IsKeyDown(Keys.Down))
            {
                wantsautopaddle = !wantsautopaddle; //toggle autopaddle cheat
            }
#endif
        }
    }
}
