using Microsoft.Xna.Framework;
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
		public Vector2 PositionCenter
		{
			get { return new Vector2(_position.X - (myWorld.scale / 2), _position.Y - (myWorld.scale / 2)); }
		}
		
		private World myWorld;

		private IDBlock _ID;
		public IDBlock ID {
			get {
				return _ID;
			}
			set {
				switch (value) {
					case IDBlock.NONE:
						this.blocking = false;
						break;
					case IDBlock.DIRT:
					case IDBlock.GRASS:
						this.blocking = true;
						break;
					default:
						this.blocking = true;
						break;
				}
				_ID = value;
			}
		}

		public Boolean blocking;
		
		public Bloc(int posX, int posY, IDBlock ID, World myWorld)
		{

			Position = new Vector2(posX * myWorld.scale, posY * myWorld.scale);

			this.ID = ID;
			this.myWorld = myWorld;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Texture2D texture;

			switch(ID) {
				case IDBlock.NONE:
					texture = null;
					break;
				case IDBlock.DIRT:
					texture = myWorld.textureDirt;
					break;
				case IDBlock.GRASS:
					texture = myWorld.textureGrass;
					break;
				default:
					texture = null;
					break;
			}

			if (texture != null) {
				Vector2 reversedPos = new Vector2(Position.X, myWorld.sizePx.Y - Position.Y - texture.Height);

				spriteBatch.Draw(texture, reversedPos, Color.White);
			}
		}

		public override string ToString()
		{
			return (ID.ToString() + " : {X = " + Position.X + "; Y = " + Position.Y + "}" + (blocking?"BLOCKING":"NOT BLOCKING"));
		}
	}

	public enum IDBlock
	{
		NONE = 0,
		DIRT = 1,
		GRASS = 2
	}
}
