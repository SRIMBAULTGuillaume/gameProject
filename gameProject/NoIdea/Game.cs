using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NoIdea.GameObjects;
using System;

namespace NoIdea
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		public int worldScale = 32;

		public const int WINDOW_HEIGHT = 20;
		public const int WINDOW_WIDTH = 32;

		private World world;

		public Game()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			this.graphics.PreferredBackBufferWidth = WINDOW_WIDTH * worldScale;
			this.graphics.PreferredBackBufferHeight = WINDOW_HEIGHT * worldScale;
			this.IsMouseVisible = false;
			this.Window.Title = "Game";

			Console.WriteLine("Constructor");
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
			world = new World(worldScale, WINDOW_HEIGHT, WINDOW_WIDTH);

			Console.WriteLine("Init - 1");

			base.Initialize();

			world.Init();

			Console.WriteLine("Init - 2");
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			world.Load(this.Content);

			Console.WriteLine("Load");
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
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
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
			else if (Keyboard.GetState().IsKeyDown(Keys.R)) {
				world = new World(worldScale, WINDOW_HEIGHT, WINDOW_WIDTH);
				world.Load(this.Content);
				world.Init();
			}

			// TODO: Add your update logic here
			world.ReadFromKeyboard(Keyboard.GetState());
			world.ReadFromMouse(Mouse.GetState());

			world.Update(gameTime);

			base.Update(gameTime);			
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			spriteBatch.Begin();

			this.world.Draw(spriteBatch);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
