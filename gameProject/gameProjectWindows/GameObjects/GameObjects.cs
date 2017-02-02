using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameProjectWindows.GameObjects
{
	public abstract class GameObjects
	{
		private Vector2 positionVect;
		public Vector2 PositionVect
		{
			get { return positionVect; }
			set {
				isAVector = true;
				positionVect = value;
			}
		}

		private Rectangle positionRect;
		public Rectangle PositionRect
		{
			get { return positionRect; }
			set {
				isAVector = false;
				positionRect = value;
			}
		}

		private bool isAVector;

		public Texture2D Texture;

		public GameObjects()
		{
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			if (isAVector)
				spriteBatch.Draw(Texture, PositionVect, Color.White);
			else
				spriteBatch.Draw(Texture, PositionRect, Color.White);
		}
	}
}
