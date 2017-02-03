using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameProjectWindows.GameObjects.StaticObjects
{
	public class Bloc : GameObjects
	{
		public enumIDBloc ID = enumIDBloc.NONE;
		public Boolean blocking;

		public Bloc()
		{
			
		}

		public override string ToString()
		{
			return ID.ToString() + " at X = " + PositionRect.X + ", Y = " + PositionRect.Y;
		}

	}
}
