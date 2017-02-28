using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace NoIdea.GameObjects
{
	class Player
	{
		private Vector2 position;
		private Texture2D texture;

		private enumDirection Direction;
		public enumDirection direction
		{
			get { return Direction; }
			set { Direction = value; }
		}
		
		private World world;
		
		private Vector2 worldSize;

		//Jumping
		private long jumpTime = 0;
		private bool isInJump = true;
		private Vector2 startingPosition;

		private Vector2 jumpSpeed;

		public Player(int radius, Color color, World world, int width, int height, ContentManager content)
		{
			this.world = world;
			this.worldSize = new Vector2(width, height);

			//texture = content.Load<Texture2D>("player");
			texture = content.Load<Texture2D>("hero");

			position = new Vector2(0, height);
			startingPosition = position;
		}

		public void Jump()
		{
			if (!isInJump) {
				startingPosition = position;
				jumpTime = 0;

				isInJump = true;
				jumpSpeed = new Vector2(0, world.scale);

			}
		}

		public void Update(GameTime gameTime)
		{
			jumpTime += (int)gameTime.ElapsedGameTime.Milliseconds;

			float t = (float)jumpTime / 1000 * 7;

			if (isInJump) {
				position.Y = ((-(1 / (float)2) * world.g * (float)Math.Pow(t, 2)) + (jumpSpeed.Y * t) + startingPosition.Y);
			}
			position.X += (float)gameTime.ElapsedGameTime.Milliseconds/1000 * (int)direction;

			if (position.X < 1)
				position.X = 0;
			else if (position.X >= worldSize.X - texture.Width)
				position.X = worldSize.X - texture.Width;
			else if (world.MyMap[(int)Math.Floor((position.X - 1) / world.scale), (int)Math.Floor(position.Y / world.scale)] != null ||
					 world.MyMap[(int)Math.Floor((position.X + texture.Width) / world.scale), (int)Math.Floor(position.Y / world.scale)] != null) {
				switch (direction){
					case enumDirection.LEFT:
						Console.WriteLine("On est dans un block (coté gauche)");
						position.X = (int)Math.Floor(position.X) + ((int)Math.Floor(position.X) % world.scale);
						break;
					case enumDirection.RIGHT:
						Console.WriteLine("On est dans un block (coté droit)");
						position.X = (int)Math.Floor(position.X) - (int)Math.Floor(position.X) % world.scale + world.scale - texture.Width;
						break;
					case enumDirection.NONE:
						break;
				}
			}

			//If the player is in a block (during a jump) we block him on the ground
			if ((world.MyMap[(int)Math.Floor(position.X / world.scale), (int)Math.Floor(position.Y / world.scale)] != null ||
				 world.MyMap[(int)Math.Floor((position.X + texture.Width - 1) / world.scale), (int)Math.Floor(position.Y / world.scale)] != null) && isInJump) {
				position.Y = ((int)Math.Floor(position.Y / world.scale) + 1) * (world.scale);
				isInJump = false;
			}

			if ((world.MyMap[(int)Math.Floor(position.X / world.scale), (int)Math.Floor((position.Y-1) / world.scale)] == null &&
				 world.MyMap[(int)Math.Floor((position.X + texture.Width - 1) / world.scale), (int)Math.Floor((position.Y - 1) / world.scale)] == null) && !isInJump) {
				startingPosition = position;
				jumpTime = 0;

				isInJump = true;
				jumpSpeed = new Vector2(0, 0);
			}
			


			if (position.Y < 0) {
				position.Y = 0;
				isInJump = false;
			} else if (position.Y > worldSize.Y - texture.Height) {
				position.Y = worldSize.Y - texture.Height;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Vector2 reversedPos = new Vector2(position.X, worldSize.Y - position.Y - texture.Height);
			spriteBatch.Draw(texture, reversedPos, Color.White);
		}

		public override string ToString()
		{
			return "X = " + position.X + "; Y = " + position.Y;
		}
	}

	public enum enumDirection
	{
		//value in pixel/s
		NONE = 0,
		RIGHT = 100,
		LEFT = -100
		
	}
}
