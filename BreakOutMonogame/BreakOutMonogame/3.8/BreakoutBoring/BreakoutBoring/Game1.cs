using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;

namespace BreakoutBoring
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        //Services
        InputHandler input;
        GameConsole console;

        //Components
        BlockManager bm;
        Paddle paddle;
        Ball ball;

        ScoreManager score;
        bool gameOver = false;
        bool onTextScreen = false;

        SpriteFont endScreenFont; // TODO: Add a new font for this screen
        Texture2D overlayTexture;
        string endMessage = "";

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Services
            input = new InputHandler(this);
            console = new GameConsole(this);
            this.Components.Add(console);
#if RELEASE
            console.ToggleConsole(); //close the console
#endif
            this.Components.Add(input);

            score = new ScoreManager(this, this);
            this.Components.Add(score);

            //GameComponents
            ball = new Ball(this, score); //Ball first paddle and block manager depend on ball
            this.Components.Add(ball);
            paddle = new Paddle(this, ball);
            this.Components.Add(paddle);

            bm = new BlockManager(this, ball, score);
            this.Components.Add(bm);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            endScreenFont = Content.Load<SpriteFont>("Arial");

            overlayTexture = new Texture2D(GraphicsDevice, 1, 1); // Create a 1x1 texture for the overlay for end text
            overlayTexture.SetData(new[] { Color.White });

            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void EndGame()
        {
            if (!gameOver)
            {
                gameOver = true;
                console.GameConsoleWrite("Lost Game");
                ShowTextScreen();
            }
        }

        public void ResetGame()
        {
            gameOver = false;
            onTextScreen = false;

            ball.SetInitialLocation();
            paddle.SetInitialLocation();

            bm.ResetBlocks();

            ScoreManager.Lives = 3;
            ScoreManager.Level = 1;
            ScoreManager.Score = 0;

            foreach (GameComponent component in Components)
            {
                component.Enabled = true;

                if (component is DrawableGameComponent drawable)
                    drawable.Visible = true;
            }
        }

        public void NextLevel()
        {
            onTextScreen = false;

            ball.SetInitialLocation();
            paddle.SetInitialLocation();

            foreach (GameComponent component in Components)
            {
                component.Enabled = true;

                if (component is DrawableGameComponent drawable)
                    drawable.Visible = true;
            }
        }

        public void ShowTextScreen()
        {
            foreach (GameComponent component in Components)
            {
                component.Enabled = false;

                if (component is DrawableGameComponent drawable)
                    drawable.Visible = false;
            }
            onTextScreen = true;
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (onTextScreen)
            {
                if (keyboard.IsKeyDown(Keys.Enter))
                {
                    if (!gameOver) { NextLevel(); }
                    else { ResetGame(); }
                } // test if this stille scapes on escape

                return;
            }

            if (score.CheckForEnd())
            {
                EndGame();
                return;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                keyboard.IsKeyDown(Keys.Escape))
            {
                Exit();
                return;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (onTextScreen)
            {
                spriteBatch.Begin();

                // Darken the whole screen
                spriteBatch.Draw(
                    overlayTexture,
                    GraphicsDevice.Viewport.Bounds,
                    Color.Black * 0.7f
                );

                string instructions = "";
                if (!gameOver) { instructions = endMessage + "Press ENTER to continue\nPress ESC to exit\nScore: " + ScoreManager.Score; }
                else instructions = endMessage + "Game Over!\nPress ENTER to continue\nPress ESC to exit";

                Vector2 textSize = endScreenFont.MeasureString(instructions);

                Vector2 textPosition = new Vector2(
                    (GraphicsDevice.Viewport.Width - textSize.X) / 2,
                    (GraphicsDevice.Viewport.Height - textSize.Y) / 2
                );

                spriteBatch.DrawString(
                    endScreenFont,
                    instructions,
                    textPosition,
                    Color.White
                );

                spriteBatch.End();
                return;
            }

            base.Draw(gameTime);
        }
    }
}
