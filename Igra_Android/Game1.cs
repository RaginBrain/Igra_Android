using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;

namespace Igra_Android
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

		enum GameState { Start, InGame, GameOver };
		GameState currentGameState = GameState.InGame;



        public void LelevUp()
        {
            scrolling1.brzina_kretanja += 1;
            scrolling2.brzina_kretanja += 1;
            barijera1.brzina_kretanja += 1;
            barijera2.brzina_kretanja += 1;
            barijera3.brzina_kretanja += 1;
            barijera.brzina_kretanja += 1;
            player1.speed += 1;
            sljedeciLevel += 1;
            maca.brzina_kretanja += 1;
            
        }
		Sprite start_button;

		Sprite game_over;
		Stopwatch sat;

        Barijera maca;
        Stit stit;
        LevelUp lvlUp;
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Barijera barijera,barijera2;
        PokretnaBarijera barijera1, barijera3;
        Random r = new Random();
        int mjera;

        int sljedeciLevel;
        Scrolling scrolling1;
        Scrolling scrolling2;
        Player player1;
        KeyboardState keyState;


		TouchCollection touchCollection;
        SpriteFont score;
		int sirina;
		int visina;

        public Game1()
        {
            
            graphics = new GraphicsDeviceManager(this);
            
            graphics.ApplyChanges();
			graphics.IsFullScreen = true;
			sirina = graphics.PreferredBackBufferWidth;
			visina = graphics.PreferredBackBufferHeight;
            Content.RootDirectory = "Content";
        }

      


        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
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
            player1.Initialize();
            player1.playerAnimation.Initialize();
            sljedeciLevel = lvlUp.level + 1;
			sat = new Stopwatch ();
			currentGameState = GameState.InGame;

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);

			//TODO: use this.Content to load your game content here 
			scrolling1 = new Scrolling(Content.Load<Texture2D>("bg1"), new Rectangle(0, 0, sirina, visina), 3);
			scrolling2 = new Scrolling(Content.Load<Texture2D>("bg2"), new Rectangle(sirina, 0,sirina, visina), 3);

			player1 = new Player(Content.Load<Texture2D>("tica_gotova"), new Rectangle(0, 0, 50, 57));
			player1.playerAnimation.Image = player1.texture;
			player1.texture_stit = Content.Load<Texture2D>("tica_stit");
			//			score = Content.Load<SpriteFont> ("SpriteFont1");

			//jst = new Jostick (Content.Load<Texture2D> ("prazan_krug"), new Rectangle (200, visina - 200, 24, 24), 150);

			game_over = new Sprite ();
			game_over.rectangle = new Rectangle (-visina, 0, 300, 409);
			game_over.texture=Content.Load<Texture2D>("game_over");

			maca = new Barijera(Content.Load<Texture2D>("maca"), new Rectangle(2200, 0,  80,  80));
			stit = new Stit(Content.Load<Texture2D>("stit"), new Rectangle(2000, 50, 64, 64));
            lvlUp = new LevelUp(Content.Load<Texture2D>("be_ready"), new Rectangle(1000, 250,150, 50));
			barijera = new Barijera(Content.Load<Texture2D>("barijera"), new Rectangle(1000, 0, 35, 100));
			barijera1 = new PokretnaBarijera(Content.Load<Texture2D>("barijera"), new Rectangle(1250, 100, 35, 100), false, visina);
			barijera2 = new Barijera(Content.Load<Texture2D>("barijera"), new Rectangle(1500, 400, 35, 100));
			barijera3 = new PokretnaBarijera(Content.Load<Texture2D>("barijera"), new Rectangle(1750, 500, 35, 100), true, sirina);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

			switch (currentGameState)
			{
			case GameState.Start:
				break;
			case GameState.InGame:

					// Pozadina*********************************************************************
				if (scrolling1.rectangle.X + graphics.PreferredBackBufferWidth <= 0)
					scrolling1.rectangle.X = scrolling2.rectangle.X + graphics.PreferredBackBufferWidth;
				if (scrolling2.rectangle.X + graphics.PreferredBackBufferWidth <= 0)
					scrolling2.rectangle.X = scrolling1.rectangle.X + graphics.PreferredBackBufferWidth;
				scrolling1.Update ();
				scrolling2.Update ();
					//******************************************************************************

				stit.Update (player1, 350, lvlUp);
				player1.Update (gameTime, graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);

				int t = r.Next (5, 30);
				maca.Update (player1, t, graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth * 3);

					//triba uštimat s velicinom sprite-a
					///////////////////////////////////////
				int interval = (int)(graphics.PreferredBackBufferWidth - graphics.PreferredBackBufferWidth / 2);
				barijera.Update (player1, r.Next (0, interval), graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
				barijera1.Update (player1, r.Next (0, interval), graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
				barijera2.Update (player1, r.Next (0, interval), graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
				barijera3.Update (player1, r.Next (0, interval), graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
				lvlUp.Update (player1, 300);

				if (sljedeciLevel == lvlUp.level)
					LelevUp ();

				touchCollection = TouchPanel.GetState ();

					//jst.Update (touchCollection);


				barijera1.Kretanje ();
				barijera3.Kretanje ();
				if (player1.stit)
					player1.playerAnimation.Image = player1.texture_stit;
				else
					player1.playerAnimation.Image = player1.texture;
				if (player1.alive == false) {
					currentGameState = GameState.GameOver;
				}
				break;

			case GameState.GameOver:
				//cekaj
				if (sat.ElapsedMilliseconds == 0 || sat.ElapsedMilliseconds > 2400)
					sat.Restart ();


				game_over.rectangle.X = player1.rectangle.X - game_over.rectangle.Width / 2;
				if (game_over.rectangle.X < 0)
					game_over.rectangle.X = 0;

				if (game_over.rectangle.X > sirina - game_over.rectangle.Width)
					game_over.rectangle.X = sirina - game_over.rectangle.Width;

				if (game_over.rectangle.Y > visina / 2 - game_over.rectangle.Height / 2)
					game_over.rectangle.Y -= 1;
				if(sat.ElapsedMilliseconds>2300)
					Initialize ();
				break;
			}
           
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
			switch (currentGameState) 
			{
			case GameState.Start:
				break;
			case GameState.InGame:
				{

					GraphicsDevice.Clear (Color.CornflowerBlue);

					// TODO: Add your drawing code here
					spriteBatch.Begin (SpriteSortMode.Deferred, BlendState.NonPremultiplied);
					scrolling1.Draw (spriteBatch);
					scrolling2.Draw (spriteBatch);
					lvlUp.Draw (spriteBatch);

					//spriteBatch.Draw(jst.texture,jst.rectangle,Color.White);
					//maca.Draw(spriteBatch);
					stit.Draw (spriteBatch);
					barijera.Draw (spriteBatch);
					barijera1.Draw (spriteBatch);
					barijera2.Draw (spriteBatch);
					barijera3.Draw (spriteBatch);
					spriteBatch.Draw (maca.texture, new Rectangle (maca.rectangle.X, maca.rectangle.Y + 20, 80, 80), Color.White);
					// stit.Draw(spriteBatch);

					player1.playerAnimation.Draw (spriteBatch);
					spriteBatch.End ();
					break;
				}
			case GameState.GameOver:
				GraphicsDevice.Clear (Color.CornflowerBlue);

				// TODO: Add your drawing code here
				spriteBatch.Begin (SpriteSortMode.Deferred, BlendState.NonPremultiplied);

				scrolling1.Draw (spriteBatch);
				scrolling2.Draw (spriteBatch);
				spriteBatch.Draw (game_over.texture, game_over.rectangle, Color.White);
				spriteBatch.End ();
				break;
			}
				
			

            


            base.Draw(gameTime);
        }
    }
}
