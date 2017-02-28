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
		public Vector2 position
		{
			get { return _position; }
			private set { _position = value; }
		}

		private Texture2D texture;

		private World myWorld;

		public Boolean blocking;
		
		/// <param name="posX">Position in the 2D array</param>
		/// <param name="posY">Position in the 2D array</param>
		public Bloc(ContentManager Content, int posX, int posY, World myWorld)
		{
			this.texture = Content.Load<Texture2D>("bloc_dirt");
			this.blocking = true;
			this.myWorld = myWorld;

			position = new Vector2(posX * myWorld.scale, posY * myWorld.scale);
		}

		public void Draw(SpriteBatch spriteBatch)
		{

			Vector2 reversedPos = new Vector2(position.X, (myWorld.height* myWorld.scale) - position.Y - texture.Height);

			spriteBatch.Draw(texture, reversedPos, Color.White);
		}

		public override string ToString()
		{
			return "X = " + position.X + "; Y = " + position.Y + "; BLOCKING : " + blocking;
		}
	}
}
