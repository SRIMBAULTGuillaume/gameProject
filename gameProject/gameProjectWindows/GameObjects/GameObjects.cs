﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameProjectWindows.GameObjects
{
	public class GameObjects
	{
		public Vector2 PositionVect;
		public Rectangle PositionRect;

		public Texture2D Texture;

		public GameObjects()
		{
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, PositionVect, Color.White);
		}
	}
}
