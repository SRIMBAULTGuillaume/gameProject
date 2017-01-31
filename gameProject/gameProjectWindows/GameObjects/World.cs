using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameProjectWindows.GameObjects
{
	public class World : GameObjects
	{
		public Bloc[,] myMap;

		public const int scale = 32;
		public int width;
		public int height;

		public World(int width, int height, ContentManager Content)
		{
			this.width = width / scale;
			this.height = height / scale;

			Texture2D dirtTexture = Content.Load<Texture2D>("bloc_dirt");
			Texture2D skyTexture = Content.Load<Texture2D>("bloc_sky");

			myMap = new Bloc[this.width, this.height];

			for (int i = 0; i<this.height; i++)
			{
				for (int j = 0; j<this.width; j++)
				{
					if (i < 4)
					{
						myMap[j, i-] = new Bloc(dirtTexture);
					} else
					{
						myMap[j, i] = new Bloc(skyTexture);
					}
					
				}
			}

		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			for (int i = 0; i < this.height; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					spriteBatch.Draw(myMap[j, i].Texture, new Rectangle(j*scale, i*scale, scale, scale), Color.White); 
				}
			}
		}
	}
}
