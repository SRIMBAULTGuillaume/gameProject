using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NoIdea
{
    public class Camera
    {
        public Matrix transform;
        Viewport view;
        public Vector2 center;

        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void Update(GameTime gameTime, Game game)
        {
            center = new Vector2(game.world.player.RealPosition.X * game.world.scale + (game.world.player.Texture.Width / 2) - (1024 / 2), 0);
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }
    }
}
