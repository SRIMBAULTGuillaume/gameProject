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

		public const int scale = 16;
		public int width;
		public int height;

		public World(int width, int height, ContentManager Content)
		{
			this.width = width / scale;
			this.height = height / scale;

			Texture2D dirtTexture = Content.Load<Texture2D>("bloc_dirt");
			Texture2D skyTexture = Content.Load<Texture2D>("bloc_sky");
			IEnumerable<int> myRandomList = generateRandom(this.width, 4);

			myMap = new Bloc[this.width, this.height];

			for (int i = 0; i<this.width; i++)
			{
				for (int j = 0; j<this.height; j++)
				{
					if (j <= myRandomList.ElementAt<int>(i))
					{
						myMap[i, j] = new Bloc(dirtTexture);
					} else
					{
						myMap[i, j] = new Bloc(skyTexture);
					}
					myMap[i, j].PositionRect = new Rectangle(i * scale, (this.height - 1 - j) * scale, scale, scale);
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					//spriteBatch.Draw(myMap[i, j].Texture, new Rectangle(i*scale, (this.height-1-j)*scale, scale, scale), Color.White);
					spriteBatch.Draw(myMap[i, j].Texture, myMap[i, j].PositionRect, Color.White);
				}
			}
		}

		private IEnumerable<int> generateRandom(int width, int start)
		{
			Random myRand = new Random();
			int number = start + myRand.Next(-1, 1);
			/*int previousNumber = number + myRand.Next(-1, 1);
			int previousPreviousNumber;*/

			const int min = 0;
			const int max = 6;

			for (int i = 0; i<width; i++)
			{
				/*previousPreviousNumber = previousNumber;
				previousNumber = number;*/
				if (myRand.Next(0, 2)==1)
					number = number + myRand.Next(-1, 2);
				if (number < min)
					number = min;
				else if (number > max)
					number = max;
				yield return number;
			}

		}
	}
}
