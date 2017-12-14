using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NoIdea.GameObjects
{
    public class Player
    {
        private Texture2D _texture;
        public Texture2D Texture
        {
            get { return _texture; }
            private set { _texture = value; }
        }

        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            private set { _position = value; }
        }
        public Vector2 PositionCenter
        {
            get { return new Vector2((RealPosition.X + ((float) Texture.Width / world.scale) / 2), (RealPosition.Y + ((float) Texture.Height / world.scale) / 2)); }
        }
        #region SET POSITION (X & Y)
        private void SetX(float value)
        {
            if (value < 1)
                value = 0;
            else if (value >= world.sizePx.X - Texture.Width)
                value = world.sizePx.X - Texture.Width;
            /*else if (world.MyMap[(int)Math.Floor((value - 1) / world.scale), (int)Math.Floor(Position.Y / world.scale)].blocking ||
					 world.MyMap[(int)Math.Floor((value + Texture.Width) / world.scale), (int)Math.Floor(Position.Y / world.scale)].blocking ||
					 world.MyMap[(int)Math.Floor((value - 1) / world.scale), (int)Math.Floor((Position.Y + Texture.Height - 1) / world.scale)].blocking ||
					 world.MyMap[(int)Math.Floor((value + Texture.Width) / world.scale), (int)Math.Floor((Position.Y + Texture.Height - 1) / world.scale)].blocking ||
					 world.MyMap[(int)Math.Floor((value - 1) / world.scale), (int)Math.Floor((Position.Y + Texture.Height/2) / world.scale)].blocking ||
					 world.MyMap[(int)Math.Floor((value + Texture.Width) / world.scale), (int)Math.Floor((Position.Y + Texture.Height/2) / world.scale)].blocking  ) {
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
			}*/

            _position = new Vector2(value, _position.Y);
        }
        private void SetY(float value)
        {
            if (value <= 0) {
                value = 0;
                IsJumping = false;
            }

            //If the player touch a block (ceiling), we block him and make him fall
            /*if ((value + Texture.Height >= world.sizePx.Y ||
				 world.MyMap[(int)Math.Floor(Position.X / world.scale), (int)Math.Floor((value + Texture.Height) / world.scale)].blocking ||
				 world.MyMap[(int)Math.Floor((Position.X + Texture.Width - 1) / world.scale), (int)Math.Floor((value + Texture.Height) / world.scale)].blocking) && IsJumping) {
				value = ((int)Math.Floor((value + Texture.Height) / world.scale)) * (world.scale) - Texture.Height;
				IsJumping = false;
				this.Jump(new Vector2(0, 0));
			}

			//If the player is in a block (during a jump), we block him on the ground
			if ((world.MyMap[(int)Math.Floor(Position.X / world.scale), (int)Math.Floor(value / world.scale)].blocking ||
				 world.MyMap[(int)Math.Floor((Position.X + Texture.Width - 1) / world.scale), (int)Math.Floor(value / world.scale)].blocking) && IsJumping) {
				value = ((int)Math.Floor(value / world.scale) + 1) * (world.scale);
				IsJumping = false;
			}*/

            if (value <= 0) {
                value = 0;
                IsJumping = false;
            } else if (value > world.sizePx.Y - Texture.Height) {
                value = world.sizePx.Y - Texture.Height;
            }

            _position = new Vector2(_position.X, value);
        }
        #endregion

        private Vector2 _realPosition;
        public Vector2 RealPosition
        {
            get { return _realPosition; }
            private set { _realPosition = value; }
        }

        private void SetRX(float value)
        {
            if (value < 0)
                value = 0;
            else if (value > world.size.X - ((float) Texture.Width / world.scale))
                value = world.size.X - ((float) Texture.Width / world.scale);
            else if (world.MyMap[(int) Math.Floor(value - 1 / world.scale), (int) Math.Floor(RealPosition.Y)].blocking ||
                     world.MyMap[(int) Math.Floor(value + Texture.Width / (float) world.scale), (int) Math.Floor(RealPosition.Y)].blocking ||
                     world.MyMap[(int) Math.Floor(value - 1 / world.scale), (int) Math.Floor(RealPosition.Y + (Texture.Height - 1) / world.scale)].blocking ||
                     world.MyMap[(int) Math.Floor(value + Texture.Width / (float) world.scale), (int) Math.Floor(RealPosition.Y + (Texture.Height - 1) / world.scale)].blocking ||
                     world.MyMap[(int) Math.Floor(value - 1 / world.scale), (int) Math.Floor(RealPosition.Y + (Texture.Height / 2) / world.scale)].blocking ||
                     world.MyMap[(int) Math.Floor(value + Texture.Width / (float) world.scale), (int) Math.Floor(RealPosition.Y + (Texture.Height / 2) / world.scale)].blocking) {
                switch (Direction) {
                    case EDirection.LEFT:
                        value = (int) Math.Ceiling(value);
                        break;
                    case EDirection.RIGHT:
                        value = (int) Math.Floor(value) + (1 - Texture.Width / (float) world.scale);
                        break;
                    case EDirection.NONE:
                        break;
                }
                Console.WriteLine(this);
            }

            RealPosition = new Vector2(value, RealPosition.Y);
        }
        private void SetRY(float value)
        {
            if (value <= 0) {
                value = 0;
                IsJumping = false;
            }

            if ((world.MyMap[(int) Math.Floor(RealPosition.X), (int) Math.Floor(value)].blocking ||
                 world.MyMap[(int) Math.Floor(RealPosition.X + (Texture.Width - 1) / world.scale), (int) Math.Floor(value)].blocking) && IsJumping) {
                value = ((int) Math.Floor(value) + 1);
                IsJumping = false;
            }

            if (value <= 0) {
                value = 0;
                IsJumping = false;
            } else if (value > world.size.Y - ((float) Texture.Height / world.scale)) {
                value = world.size.Y - ((float) Texture.Height / world.scale);
            } else {
                Console.WriteLine(this);
            }

            RealPosition = new Vector2(RealPosition.X, value);
        }


        private EDirection _direction;
        public EDirection Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        private float _m;
        public float M
        {
            get { return _m; }
            private set { _m = value; }
        }

        private World world;

        //Jumping
        private long jumpTime = 0;
        private bool _isJumping;
        public bool IsJumping
        {
            get { return _isJumping; }
            private set {
                if (!_isJumping && value) {
                    realStartingPosition = RealPosition;
                    jumpTime = 0;
                }
                _isJumping = value;
            }
        }
        private Vector2 startingPosition;
        private Vector2 realStartingPosition;
        private Vector2 jumpSpeed;

        public Player(Texture2D texture, World world)
        {
            this.world = world;

            this.Texture = texture;

            Position = new Vector2(1, 1);
            RealPosition = new Vector2(1, world.size.Y - 4);
            M = 60f;
            startingPosition = Position;
            realStartingPosition = RealPosition;
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
            jumpTime += (int) gameTime.ElapsedGameTime.Milliseconds;

            //float t = (float)jumpTime / 1000;
            float t = (float) jumpTime / 750;

            if (IsJumping) {
                SetRY(((-(1 / (float) 2) * world.g * (float) Math.Pow(t, 2)) + (jumpSpeed.Y * t) + realStartingPosition.Y));
            }

            SetRX(RealPosition.X + (float) gameTime.ElapsedGameTime.Milliseconds / 1000 * (int) Direction / world.scale);



            //If there is no bloc under the player, he falls
            if (RealPosition.Y > 0 &&
                !world.MyMap[(int) Math.Floor(RealPosition.X), (int) Math.Ceiling(RealPosition.Y - 1)].blocking &&
                !world.MyMap[(int) Math.Ceiling(RealPosition.X), (int) Math.Ceiling(RealPosition.Y - 1)].blocking) {

                this.Jump(new Vector2(0, 0));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 reversedPos = new Vector2(RealPosition.X * world.scale, (world.size.Y - RealPosition.Y) * world.scale - Texture.Height);

            //Console.WriteLine(this);
            spriteBatch.Draw(Texture, reversedPos, Color.White);
        }

        public override string ToString()
        {
            return "Player : {X= " + RealPosition.X + "; Y = " + RealPosition.Y + "}";
        }
    }

    public enum EDirection
    {
        //value in pixel/s
        NONE = 0,
        RIGHT = 120,
        LEFT = -120

    }
}
