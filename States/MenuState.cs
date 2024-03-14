using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZeroFlip64Web.States
{
    class MenuState : IState
    {
        private Texture2D _titleTexture;
        private Texture2D _startTexture;

        public MenuState()
        {
            _titleTexture = ZeroFlip64WebGame.Textures["title"];
            _startTexture = ZeroFlip64WebGame.Textures["start"];
        }
        
        public void Update(GameTime gameTime)
        {
            if (ZeroFlip64WebGame.Input.WasKeyJustDown(Keys.Space))
            {
                ZeroFlip64WebGame.Sounds["ding"].Play(0.5f, 0.0f, 0.0f);
                ZeroFlip64WebGame.States.Set(new PlayState());
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_titleTexture, new Vector2(0, 16), Color.White);
            spriteBatch.Draw(_startTexture, new Vector2(10, 42), Color.White);
        }
    }
}
