using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NoIdea.GameObjects;
using NoIdea.GameObjects.Blocs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoIdea
{
	class World
	{
		private Vector2 position;
		private Texture2D texture;

		public int scale;

		public int height;
		public int width;

		public float g = 9.81f;
		
		private Bloc[,] myMap;
		public Bloc[,] MyMap {
			get { return myMap; }
		}


		private Player player;

		#region CONSTRUCTORS

		/// <summary>
		/// Set the position at (0, 0)
		/// </summary>
		public World(int scale, int height, int width, ContentManager Content) : this(scale, height, width, Content, new Vector2(0, 0)){

		}

		public World(int scale, int height, int width, ContentManager Content, Vector2 position)
		{
			this.position = position;

			this.scale = scale;
			this.height = height;
			this.width = width;

			this.texture = Content.Load<Texture2D>("font");

			myMap = new Bloc[width, height];

			World_Generator noise = new World_Generator(new Random().Next(0, 1000000000), height);

			for (int x = 0; x < width; x++) {
				int columnHeight = noise.getNoise(x, height - 2);
				if (columnHeight <= 0)
					columnHeight = 1;

				for (int y = 0; y < columnHeight; y++) {
					myMap[x, y] = new Bloc(Content, x, y, this);
				}
			}
			
			player = new Player(5, Color.Red, this, width * scale, height * scale, Content);
		}
		#endregion

		public void Update(GameTime gametime)
		{
			player.Update(gametime);
		}

		public void readFormKeyboard(KeyboardState state)
		{
			if (state.IsKeyDown(Keys.Space)) {
				player.Jump();
			}
			if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q)) {
				player.direction = enumDirection.LEFT;
			} else if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D)) {
				player.direction = enumDirection.RIGHT;
			} else {
				player.direction = enumDirection.NONE;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, position, Color.White);

			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					if (myMap[x, y] != null)
						myMap[x, y].Draw(spriteBatch);
				}
			}

			this.player.Draw(spriteBatch);
		}
	}
}
