using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameProjectWindows.GameObjects.MovingObjects
{
	public abstract class Movables : GameObjects
	{
		protected World myWorld;
		public int size;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Texture"></param>
		/// <param name="posX"></param>
		/// <param name="posY"></param>
		/// <param name="size">Texture height (blocs)</param>
		public Movables(World myWorld, Texture2D Texture, int posX, int posY, int size = 2)
		{
			this.Texture = Texture;
			this.myWorld = myWorld;
			this.size = size;

			this.PositionRect = new Rectangle(posX, posY, myWorld.scale,  myWorld.scale * size);
		}
	}
}
