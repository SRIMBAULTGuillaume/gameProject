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
		public static Bloc Create(enumIDBloc id, ContentManager Content)
		{
			switch (id)
			{
				case enumIDBloc.DIRT:
					return new DirtBloc(Content);
				default:
					return new SkyBloc(Content);
			}
		}

		
	}

	public enum enumIDBloc
	{
		NONE = 0,
		DIRT = 1
	}
}
