using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeroFlip64Web.States
{
    public interface IState
    {
        void Update(GameTime gameTime);

        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
