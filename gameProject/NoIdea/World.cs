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

		private Texture2D textureArrow;
		private Texture2D textureArrowGreen;

		public Texture2D textureDirt { get; private set; }
		public Texture2D textureGrass { get; private set; }
		public Texture2D textureHover { get; private set; }
		public Texture2D texturePlayer { get; private set; }

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
		public World(int scale, int height, int width) : this(scale, height, width, new Vector2(0, 0)){

		}

		public World(int scale, int height, int width, Vector2 position)
		{
			this.position = position;

			this.scale = scale;
			this.height = height;
			this.width = width;
		}
		#endregion

		public void Load(ContentManager Content, string blocsFolder = "Blocs")
		{
			this.texture = Content.Load<Texture2D>("font");
			this.textureArrow = Content.Load<Texture2D>("arrow");
			this.textureHover = Content.Load<Texture2D>("hover");
			this.texturePlayer = Content.Load<Texture2D>("hero");

			this.textureDirt = Content.Load<Texture2D>(blocsFolder + @"\dirt");
			this.textureGrass = Content.Load<Texture2D>(blocsFolder + @"\grass");

			textureArrowGreen = new Texture2D(textureArrow.GraphicsDevice, textureArrow.Width, textureArrow.Height);

			Color[] data = new Color[textureArrowGreen.Width * textureArrowGreen.Height];
			textureArrow.GetData(data);

			for (int i = 0; i < data.Length; i++)
				if (data[i].R != 0) {
					data[i].R = 80;
					data[i].G = 200;
					data[i].B = 32;
				}
			textureArrowGreen.SetData(data);
		}

		public void Init()
		{
			myMap = new Bloc[width, height];

			World_Generator noise = new World_Generator(new Random().Next(0, 1000000000), height);

			//BlocFactory blockFactory = BlocFactory.GetInstance();
			//blockFactory.setContent(Content);

			for (int x = 0; x < width; x++) {
				int columnHeight = noise.getNoise(x, height - 2);
				if (columnHeight <= 0)
					columnHeight = 1;

				for (int y = 0; y < height; y++) {
					if (y >= columnHeight) {
						//myMap[x, y] = blockFactory.CreateBlock(x, y, IDBlock.NONE, this);
						myMap[x, y] = new Bloc(x, y, IDBlock.NONE, this);
					} else if (y == columnHeight - 1) {
						//myMap[x, y] = blockFactory.CreateBlock(x, y, IDBlock.GRASS, this);
						myMap[x, y] = new Bloc(x, y, IDBlock.GRASS, this);
					} else {
						//myMap[x, y] = blockFactory.CreateBlock(x, y, IDBlock.DIRT, this);
						myMap[x, y] = new Bloc(x, y, IDBlock.DIRT, this);
					}
				}
			}
			
			player = new Player(texturePlayer, this);
		}

		public void Update(GameTime gametime)
		{
			player.Update(gametime);
		}

		public void ReadFormKeyboard(KeyboardState state)
		{
			if (state.IsKeyDown(Keys.Space) || state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.Z)) {
				player.Jump(new Vector2(0, scale*7));
			}
			if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q)) {
				player.Direction = EDirection.LEFT;
			} else if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D)) {
				player.Direction = EDirection.RIGHT;
			} else {
				player.Direction = EDirection.NONE;
			}
		}

		public void Draw(SpriteBatch spriteBatch, MouseState state)
		{
			spriteBatch.Draw(texture, position, Color.White);

			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					if (myMap[x, y] != null)
						myMap[x, y].Draw(spriteBatch);
				}
			}

			this.player.Draw(spriteBatch);

			if (state.X > 0 && state.X < width*scale && state.Y > 0 && state.Y < height * scale) {

				Vector2 posPlayerCenter = new Vector2((player.Position.X + player.Texture.Width/2), (height * scale - player.Position.Y - player.Texture.Height / 2));
				Vector2 posArrowCenter = new Vector2(textureArrow.Width / 2, textureArrow.Height / 2);
				
				Vector2 direction = new Vector2(state.X, state.Y) - posPlayerCenter;
				Vector2 posHover = new Vector2(state.X - (state.X%scale), state.Y - (state.Y % scale));

				int circleRadius = (int)(scale * 1.2f);

				posArrowCenter = posPlayerCenter + Vector2.Normalize(direction) * circleRadius;

				float angle = (float)Math.Atan2(direction.Y, direction.X) + (float)(Math.PI * 0.5f);

				if (state.LeftButton == ButtonState.Pressed) {

					spriteBatch.Draw(	textureArrowGreen, posArrowCenter, null, Color.White,
										angle,
										new Vector2(textureArrow.Width / 2, textureArrow.Height / 2), 1, SpriteEffects.None, 0);

					myMap[(int)Math.Floor(posHover.X / scale), height - 1 - (int)Math.Floor(posHover.Y / scale)].ID = IDBlock.NONE;

				} else {
					spriteBatch.Draw(	textureArrow, posArrowCenter, null, Color.White,
										angle,
										new Vector2(textureArrow.Width/2, textureArrow.Height/2), 1, SpriteEffects.None, 0);
				}
				
				spriteBatch.Draw(	textureHover, posHover, null, Color.White, 0,
									new Vector2((textureHover.Width - textureArrow.Width)/2, (textureHover.Width - textureArrow.Width)/2), 1, SpriteEffects.None, 0);

				Console.WriteLine(myMap[(int)Math.Floor(posHover.X/scale), height-1 - (int)Math.Floor(posHover.Y/scale)]);

			}

		}
	}
}
