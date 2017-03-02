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
		private Texture2D _texture;
		public Texture2D Texture
		{
			get { return _texture; }
			private set { _texture = value; }
		}


		private Vector2 _position;
		public Vector2 Position	{
			get { return _position; }
			private set { _position = value; }
		}
		#region SET POSITION (X & Y)
		private void SetX (float value)
		{
			if (value < 1)
				value = 0;
			else if (value >= worldSize.X - Texture.Width)
				value = worldSize.X - Texture.Width;
			else if (world.MyMap[(int)Math.Floor((value - 1) / world.scale), (int)Math.Floor(Position.Y / world.scale)] != null ||
					 world.MyMap[(int)Math.Floor((value + Texture.Width) / world.scale), (int)Math.Floor(Position.Y / world.scale)] != null) {
				switch (Direction) {
					case EDirection.LEFT:
						value = (int)Math.Floor(value);
						while (value%world.scale != 0) {
							value++;
						}
						break;
					case EDirection.RIGHT:
						value = (int)Math.Floor(value);
						while (value % world.scale != 0) {
							value--;
						}
						value += (world.scale - Texture.Width);
						break;
					case EDirection.NONE:
						break;
				}
			}

			_position = new Vector2(value, _position.Y);
		}
		private void SetY (float value)
		{

			//If the player is in a block (during a jump) we block him on the ground
			if ((world.MyMap[(int)Math.Floor(Position.X / world.scale), (int)Math.Floor(value / world.scale)] != null ||
				 world.MyMap[(int)Math.Floor((Position.X + Texture.Width - 1) / world.scale), (int)Math.Floor(value / world.scale)] != null) && IsJumping) {
				value = ((int)Math.Floor(value / world.scale) + 1) * (world.scale);
				IsJumping = false;
			}

			if (value <= 0) {
				value = 0;
				IsJumping = false;
			} else if (value > worldSize.Y - Texture.Height) {
				value = worldSize.Y - Texture.Height;
			}

			_position = new Vector2(_position.X, value);
		}
		#endregion

		private EDirection _direction;
		public EDirection Direction
		{
			get { return _direction; }
			set { _direction = value; }
		}
		
		private World world;
		
		private Vector2 worldSize;

		//Jumping
		private long jumpTime = 0;
		private bool _isJumping;
		public bool IsJumping
		{
			get { return _isJumping; }
			private set {
				if (!_isJumping && value) {
					startingPosition = Position;
					jumpTime = 0;
				}
				_isJumping = value;
			}
		}
		private Vector2 startingPosition;
		private Vector2 jumpSpeed;

		public Player(int radius, Color color, World world, int width, int height, ContentManager content)
		{
			this.world = world;
			this.worldSize = new Vector2(width, height);

			//texture = content.Load<Texture2D>("player");
			Texture = content.Load<Texture2D>("hero");

			Position = new Vector2(0, height);
			startingPosition = Position;
		}

		public void Jump(Vector2 jumpSpeed)
		{
			if (!IsJumping) {
				IsJumping = true;
				this.jumpSpeed = jumpSpeed;
			}
		}

		public void Update(GameTime gameTime)
		{
			jumpTime += (int)gameTime.ElapsedGameTime.Milliseconds;

			float t = (float)jumpTime / 1000 * 8;

			if (IsJumping) {
				SetY((-(1 / (float)2) * world.g * (float)Math.Pow(t, 2)) + (jumpSpeed.Y * t) + startingPosition.Y);
			}
			SetX(Position.X + (float)gameTime.ElapsedGameTime.Milliseconds/1000 * (int)Direction);
			
			//If there is no bloc under the player, he falls
			if ((world.MyMap[(int)Math.Floor(Position.X / world.scale), (int)Math.Floor((Position.Y-1) / world.scale)] == null &&
				 world.MyMap[(int)Math.Floor((Position.X + Texture.Width - 1) / world.scale), (int)Math.Floor((Position.Y - 1) / world.scale)] == null) && !IsJumping) {

				this.Jump(new Vector2(0, 0));
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Vector2 reversedPos = new Vector2(Position.X, worldSize.Y - Position.Y - Texture.Height);
			spriteBatch.Draw(Texture, reversedPos, Color.White);
		}

		public override string ToString()
		{
			return "X = " + Position.X + "; Y = " + Position.Y;
		}
	}

	public enum EDirection
	{
		//value in pixel/s
		NONE = 0,
		RIGHT = 100,
		LEFT = -100
		
	}
}
