using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameProjectWindows.GameObjects.StaticObjects
{
	static public class BlocFactory
	{
		public static Bloc Create(enumIDBloc id, ContentManager Content, World myWorld)
		{
			switch (id)
			{
				case enumIDBloc.DIRT:
					return new DirtBloc(Content, myWorld);
				case enumIDBloc.GRASS:
					return new GrassBloc(Content, myWorld);
				default:
					return new SkyBloc(Content, myWorld);
			}
		}

		
	}

	public enum enumIDBloc
	{
		NONE = 0,
		DIRT = 1,
		GRASS = 2
	}
}
