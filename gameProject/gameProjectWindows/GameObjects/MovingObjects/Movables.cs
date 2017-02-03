using gameProjectWindows.GameObjects.StaticObjects;
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
		public enumDirection direction = enumDirection.NONE;

		protected int maximumSpeed;

		public Vector2 speed;

		/// <summary>
		/// Constructor
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

			maximumSpeed = myWorld.scale / 16;

			this.PositionRect = new Rectangle(posX, posY, myWorld.scale,  myWorld.scale * size);
		}

		protected Bloc GetBlocUnderMe()
		{
			return myWorld.myMap[this.positionRect.X / myWorld.scale, (myWorld.height) - (this.positionRect.Y/myWorld.scale)-3];
		}

		protected Bloc GetSolidBlocUnderMe()
		{
			for (int i = (myWorld.height) - (this.positionRect.Y / myWorld.scale) - 3; i >= 0; i--) {
				if (myWorld.myMap[this.positionRect.X / myWorld.scale, i].ID == enumIDBloc.DIRT) {
					return (myWorld.myMap[this.positionRect.X / myWorld.scale, i]);
				}
			}
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="scale">Permit to choose the number of pixels traveled during the deplacement</param>
		public void SetHorizontalSpeed()
		{
			switch (this.direction) {
				case enumDirection.LEFT:
					if (this.speed.X >= 0) {
						this.speed.X = -0.4F * maximumSpeed;
					} else if (-this.speed.X < maximumSpeed) {
						this.speed.X -= 0.1F * maximumSpeed;
					}
					break;
				case enumDirection.RIGHT:
					if (this.speed.X <= 0){
						this.speed.X = 0.4F * maximumSpeed;
					} else if (this.speed.X < maximumSpeed) {
						this.speed.X += 0.1F * maximumSpeed;
					}
					
					break;
				default:
					this.speed.X = 0;
					break;
			}

			if (Math.Abs(this.speed.X) > maximumSpeed) {
				this.speed.X = (this.speed.X > 0) ? maximumSpeed : -maximumSpeed;
			}


		}

		public void Jump()
		{
			if (GetBlocUnderMe().ID == enumIDBloc.DIRT)
				this.speed.Y = myWorld.scale + myWorld.scale/4;
		}

		public void Update(GameTime gameTime)
		{
			if (this.positionRect.X != 0 || this.positionRect.Y != 0) {
				if (GetBlocUnderMe().ID != enumIDBloc.DIRT)
					//this.speed += myWorld.g;
					//this.speed += new Vector2(0, -myWorld.scale);
					this.speed += new Vector2(0, -(GetSolidBlocUnderMe().PositionRect.Y - this.positionRect.Y - myWorld.scale * 2));
				
				Console.WriteLine(GetBlocUnderMe());
				Console.WriteLine("My pos X = " + this.positionRect.X + ", Y = " + this.positionRect.Y);
				Console.WriteLine(GetSolidBlocUnderMe().PositionRect.Y - this.positionRect.Y - myWorld.scale*2);
			}

			Move();
		}

		private void Move()
		{
			this.positionRect.X += (int)this.speed.X;
			if (positionRect.X > myWorld.width * (myWorld.scale - 1))
				positionRect.X = myWorld.width * (myWorld.scale - 1);
			else if (positionRect.X < 0)
				positionRect.X = 0;
			this.positionRect.Y -= (int)this.speed.Y;

			if (GetBlocUnderMe().ID == enumIDBloc.DIRT)
				this.speed.Y = 0;
			this.speed.Y = 0;
		}

		public enum enumDirection
		{
			NONE = -1,
			LEFT = 0,
			RIGHT = 1
		}
	}
}
