using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoIdea
{
    class World_Generator
    {
		long seed;
		int height;

		public World_Generator(long seed, int height)
		{
			this.seed = seed;
			this.height = height;
		}

		private int random(long x, int range)
		{
			return (int)(((x + seed) ^ 5) % range);
		}

		public int getNoise(int x, int range)
		{
			int chunkSize = 16;

			float noise = 0;

			range /= 2;

			while (chunkSize > 0) {
				int chunkIndex = x / chunkSize;

				float prog = (x % chunkSize) / (chunkSize * 1f);

				float left_random = random(chunkIndex, range);
				float right_random = random(chunkIndex + 1, range);


				noise += (1 - prog) * left_random + prog * right_random;

				chunkSize /= 2;
				range /= 2;

				range = Math.Max(1, range);
			}

			int returnValue = (int)Math.Round(noise);

			return returnValue;
		}
	}
}
