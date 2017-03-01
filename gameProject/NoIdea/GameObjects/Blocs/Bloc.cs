using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoIdea.GameObjects.Blocs
{
	class Bloc
	{
		private Vector2 _position;
		public Vector2 Position
		{
			get { return _position; }
			private set { _position = value; }
		}

		private Texture2D texture;

		private World myWorld;

		public IDBlock ID { get; private set; }
		public Boolean blocking;
		
		public Bloc(Texture2D texture, int posX, int posY, IDBlock ID, bool blocking, World myWorld)
		{
			this.texture = texture;

			Position = new Vector2(posX * myWorld.scale, posY * myWorld.scale);

			this.ID = ID;
			this.blocking = blocking;
			this.myWorld = myWorld;
		}

		public Bloc(int posX, int posY, IDBlock ID, bool blocking, World myWorld) : this(null, posX, posY, ID, blocking, myWorld)
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (texture != null) {
				Vector2 reversedPos = new Vector2(Position.X, (myWorld.height* myWorld.scale) - Position.Y - texture.Height);
				
				spriteBatch.Draw(texture, reversedPos, Color.White);
			}
		}

		public override string ToString()
		{
			return "{" + ID.ToString() + "}" + "X = " + Position.X + "; Y = " + Position.Y + "; BLOCKING : " + blocking;
		}
	}

	class BlocFactory
	{
		private static BlocFactory instance = null;
		private static readonly object padlock = new object();

		private ContentManager Content;

		private BlocFactory()
		{
		}

		public static BlocFactory GetInstance()
		{
			if (instance == null) {
				lock (padlock) {
					if (instance == null) {
						instance = new BlocFactory();
					}
				}
			}
			return instance;
		}

		public void setContent(ContentManager Content)
		{
			if (instance == null) {
				lock (padlock) {
					if (instance == null) {
						instance = new BlocFactory();
					}
				}
			}
			this.Content = new ContentManager(Content.ServiceProvider, @"Content\Blocs");
		}

		public Bloc CreateBlock(int posX, int posY, IDBlock ID, World myWorld)
		{
			Texture2D texture;
			bool blocking = false;
			if (Content == null) {
				Console.WriteLine("Content not found {X : " + posX + "; Y : " + posY + "}");
				ID = IDBlock.NONE;
			}

			switch (ID) {
				default:
				case IDBlock.NONE:
					return new Bloc(posX, posY, ID, blocking, myWorld);
				case IDBlock.DIRT:
					texture = Content.Load<Texture2D>("dirt");
					blocking = true;
					break;
				case IDBlock.GRASS:
					texture = Content.Load<Texture2D>("grass");
					blocking = true;
					break;
			}

			return new Bloc(texture, posX, posY, ID, blocking, myWorld);
		}
	}

	public enum IDBlock
	{
		NONE = 0,
		DIRT = 1,
		GRASS = 2
	}
}
