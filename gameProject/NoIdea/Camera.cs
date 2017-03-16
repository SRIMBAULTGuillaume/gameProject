using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoIdea
{
	class Camera
	{
		public Matrix transform;
		Viewport view;
		Vector2 center;

		public Camera(Viewport newView)
		{
			view = newView;
		}

		public void Update(GameTime gameTime, Game game)
		{
			center = new Vector2(game.world.player.Position.X + (game.world.player.Texture.Width / 2) - (game.world.sizePx.X/2), 0);
			transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
		}
	}
}
