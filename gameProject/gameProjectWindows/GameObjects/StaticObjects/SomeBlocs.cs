using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameProjectWindows.GameObjects.StaticObjects
{
	class SkyBloc : Bloc
	{
		public SkyBloc(ContentManager Content, World myWorld) : base(myWorld)
		{
			this.Texture = Content.Load<Texture2D>("bloc_sky");
			this.ID = enumIDBloc.NONE;
			this.blocking = false;
		}
	}
	
	class DirtBloc : Bloc
	{
		public DirtBloc(ContentManager Content, World myWorld) : base(myWorld)
		{
			this.Texture = Content.Load<Texture2D>("bloc_dirt");
			this.ID = enumIDBloc.DIRT;
			this.blocking = true;
		}
	}

	class GrassBloc : Bloc
	{
		public GrassBloc(ContentManager Content, World myWorld) : base(myWorld)
		{
			this.Texture = Content.Load<Texture2D>("bloc_grass");
			this.ID = enumIDBloc.GRASS;
			this.blocking = true;
		}
	}
}
