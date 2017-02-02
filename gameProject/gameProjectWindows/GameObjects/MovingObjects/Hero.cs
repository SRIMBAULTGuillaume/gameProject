using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace gameProjectWindows.GameObjects.MovingObjects
{
	public class Hero : Movables
	{
		public Hero(World myWorld, Texture2D Texture, int posX, int posY, int size = 2) : base(myWorld, Texture, posX, posY, size)
		{
			Rectangle position = this.SetSpawn();
			if (position != new Rectangle())
				PositionRect = position;
		}

		public Rectangle SetSpawn()
		{
			for (int i = 0; i<myWorld.height; i++)
			{
				if (myWorld.myMap[0, i].ID == StaticObjects.enumIDBloc.NONE)
				{
					return new Rectangle(0, (myWorld.height-i-size)*myWorld.scale, PositionRect.Size.X, PositionRect.Size.Y);
				}
			}
			return new Rectangle();
		}
	}
}
