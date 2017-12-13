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
	public class World
	{
		private Vector2 position;
		private Texture2D texture;

		private Texture2D textureArrow;
		private Texture2D textureCursor;

		public Texture2D textureDirt { get; private set; }
		public Texture2D textureGrass { get; private set; }
		public Texture2D textureHover { get; private set; }
		public Texture2D textureHoverRed { get; private set; }
		private Vector2 posHover;
		private bool blocReachable;
		public Texture2D texturePlayer { get; private set; }

		public int scale { get; private set; }

		/// <summary>
		/// World's size in units (number of blocs)
		/// </summary>
		public Point size { get ; private set; }
		/// <summary>
		/// World's size in pixels
		/// </summary>
		public Point sizePx {
			get { return new Point(size.X * scale, size.Y * scale); }
		}
		
		public float g = 9.81f;
		
		private Bloc[,] myMap;
		public Bloc[,] MyMap {
			get { return myMap; }
		}
		
		public Player player;
		private Vector2 mousePos;
		private float circleRadius;
		//private bool leftClickPressed = false;
		//private bool rightClickPressed = false;

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

			size = new Point(width, height);

			circleRadius = scale * 2.8f;
		}
		#endregion

		public void Load(ContentManager Content, string blocsFolder = "Blocs")
		{
			this.texture = Content.Load<Texture2D>("font");
			this.textureArrow = Content.Load<Texture2D>("arrow");
			this.textureHover = Content.Load<Texture2D>("hover");
			this.texturePlayer = Content.Load<Texture2D>("hero");
			this.textureCursor = Content.Load<Texture2D>("cursor");

			this.textureDirt = Content.Load<Texture2D>(blocsFolder + @"\dirt");
			this.textureGrass = Content.Load<Texture2D>(blocsFolder + @"\grass");

			this.textureHoverRed = Content.Load<Texture2D>("hover_red");
		}

		public void Init()
		{
			myMap = new Bloc[size.X, size.Y];

			World_Generator noise = new World_Generator(new Random().Next(0, 1000000000), size.Y);

			for (int x = 0; x < size.X; x++) {
				int columnHeight = noise.getNoise(x, size.Y - 2);
				if (columnHeight <= 0)
					columnHeight = 1;

				for (int y = 0; y < size.Y; y++) {
					if (y >= columnHeight) {
						myMap[x, y] = new Bloc(x, y, IDBlock.NONE, this);
					} else if (y == columnHeight - 1) {
						myMap[x, y] = new Bloc(x, y, IDBlock.GRASS, this);
					} else {
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

		public void ReadFromKeyboard(KeyboardState state)
		{
			if (state.IsKeyDown(Keys.Space) || state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.Z)) {
				player.Jump(new Vector2(0, 5));
			}
			if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Q)) {
				player.Direction = EDirection.LEFT;
			} else if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D)) {
				player.Direction = EDirection.RIGHT;
			} else {
				player.Direction = EDirection.NONE;
			}
		}

		public void ReadFromMouse(MouseState state, Camera cam)
		{
			
			if (state.X > 0 && state.X < sizePx.X && state.Y > 0 && state.Y < sizePx.Y) {

				mousePos = new Vector2(state.Position.X, state.Position.Y) + cam.center;
				posHover = new Vector2((int)Math.Floor(mousePos.X/scale), (int)Math.Floor(mousePos.Y/scale));

				bool collided = false;
				Vector2[] hoverCorners = new Vector2[4] {   new Vector2(posHover.X, posHover.Y),
															new Vector2(posHover.X + 1, posHover.Y),
															new Vector2(posHover.X, posHover.Y + 1),
															new Vector2(posHover.X + 1, posHover.Y + 1)};
				for (int i = 0; i < 4; i++) {
					if (hoverCorners[i].X > player.RealPosition.X &&			hoverCorners[i].X < player.RealPosition.X + player.Texture.Width/(float)scale &&
						size.Y - hoverCorners[i].Y > player.RealPosition.Y &&	size.Y - hoverCorners[i].Y < player.RealPosition.Y + player.Texture.Height/(float)scale) {
						collided = true;
						break;
					}
				}

				if (posHover.X > Vector2.Zero.X && posHover.Y > Vector2.Zero.Y) {
					Bloc targetedBloc = myMap[(int)posHover.X, (int)posHover.Y];
					if (Math.Abs((targetedBloc.PositionCenter - player.PositionCenter).Length()) < circleRadius) {
						blocReachable = true;
					} else {
						blocReachable = false;
					}

					if (collided)
						blocReachable = false;
					else
						blocReachable = true;

					if (state.LeftButton == ButtonState.Pressed) {
						RemoveBlock();
					} else if (state.RightButton == ButtonState.Pressed) {
						PlaceBlock();
					}
				}

			} else {
				mousePos = new Vector2(0, 0);
			}

		}

		public void PlaceBlock()
		{
			Bloc targetedBloc = myMap[(int)Math.Floor(posHover.X / scale), size.Y - 1 - (int)Math.Floor(posHover.Y / scale)];

			if (blocReachable) {
				if (!targetedBloc.blocking) {
					targetedBloc.ID = IDBlock.DIRT;
				}
			}			
		}

		public void RemoveBlock()
		{
			Bloc targetedBloc = myMap[(int)Math.Floor(posHover.X / scale), size.Y - 1 - (int)Math.Floor(posHover.Y / scale)];

			if (blocReachable) {
				if (targetedBloc.blocking) {
					targetedBloc.ID = IDBlock.NONE;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			for (int x = 0; x < size.X; x++) {
				for (int y = 0; y < size.Y; y++) {
					if (myMap[x, y] != null)
						myMap[x, y].Draw(spriteBatch);
				}
			}

			this.player.Draw(spriteBatch);

			if (mousePos.X > 0 && mousePos.X < sizePx.X && mousePos.Y > 0 && mousePos.Y < sizePx.Y) {
				
				if (blocReachable)
					spriteBatch.Draw(textureHover, posHover * scale, null, Color.White);
				else
					spriteBatch.Draw(textureHoverRed, posHover * scale, null, Color.White);
								
				spriteBatch.Draw(textureCursor, mousePos - new Vector2(textureCursor.Width, textureCursor.Height) / 2, Color.White);
			}

		}

		public void DrawFont(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, position, Color.White);
		}
	}
}
