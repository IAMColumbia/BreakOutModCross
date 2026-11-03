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

            score = new ScoreManager(this);
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
                ShowTextScreen("Game Over! Press ESC to exit or ENTER to try again.");
            }
        }

        public void ShowTextScreen(string message)
        {
            foreach (GameComponent component in Components)
            {
                component.Enabled = false;

                if (component is DrawableGameComponent drawable)
                    drawable.Visible = false;
            }
            endMessage = message;
            console.GameConsoleWrite(message);
            onTextScreen = true;
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (score.CheckForEnd())
            {
                EndGame();
            }
            if (onTextScreen)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    if (gameOver)
                    {
                        gameOver = false;
                        score = new ScoreManager(this);
                        bm = new BlockManager(this, ball, score);
                    }
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
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            else
            {
                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
