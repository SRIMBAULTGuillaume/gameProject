using gameProjectWindows.GameObjects.MovingObjects;
using Microsoft.Xna.Framework;
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
		protected World myWorld;

		public enumIDBloc ID = enumIDBloc.NONE;
		public Boolean blocking;

		#region Constructors
		public Bloc()
		{
			
		}

		public Bloc (World myWorld)
		{
			this.myWorld = myWorld;
		}
		#endregion

		public bool IsCollided(Point point)
		{
			Vector2 vector = new Vector2(point.X, point.Y);
			return CollisionAdapter(vector);
		}
		public bool IsCollided(Vector2 point)
		{
			return CollisionAdapter(point);
		}

		public bool CollisionAdapter(Vector2 point)
		{
			if (point.X > this.PositionRect.X && point.X < this.PositionRect.X + this.PositionRect.Size.X &&
					point.Y > this.PositionRect.Y && point.Y <= this.PositionRect.Y + this.PositionRect.Size.Y) {
				return true;
			} else {
				return false;
			}
		}

		public bool IsCollided(Movables mobile)
		{
			Point[] mobileCorners = new Point[] {new Point(mobile.PositionRect.X, mobile.PositionRect.Y),
												 new Point(mobile.PositionRect.X, mobile.PositionRect.Y + mobile.PositionRect.Size.Y),
												 new Point(mobile.PositionRect.X + mobile.PositionRect.Size.X, mobile.PositionRect.Y),
												 new Point(mobile.PositionRect.X + mobile.PositionRect.Size.X, mobile.PositionRect.Y + mobile.PositionRect.Size.Y)};
			foreach (Point currentPoint in mobileCorners) {
				if (currentPoint.X > this.PositionRect.X && currentPoint.X <= this.PositionRect.X + this.PositionRect.Size.X &&
					currentPoint.Y > this.PositionRect.Y && currentPoint.Y <= this.PositionRect.Y + this.PositionRect.Size.Y) {
					return true;
				}
			}
			return false;
		}

		public override string ToString()
		{
			return ID.ToString() + " at X = " + PositionRect.X + ", Y = " + PositionRect.Y;
		}

	}
}
