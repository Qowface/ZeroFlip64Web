using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZeroFlip64Web.States
{
    class EndState : IState
    {
        private Texture2D _gameoverTexture;
        private Texture2D _youwinTexture;
        private Texture2D _restartTexture;

        private bool _gameWon;

        public EndState(bool gameWon)
        {
            _gameoverTexture = ZeroFlip64WebGame.Textures["gameover"];
            _youwinTexture = ZeroFlip64WebGame.Textures["youwin"];
            _restartTexture = ZeroFlip64WebGame.Textures["restart"];
            
            _gameWon = gameWon;
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
            if (_gameWon)
            {
                spriteBatch.Draw(_youwinTexture, new Vector2(20, 16), new Color(56, 142, 60));
            }
            else
            {
                spriteBatch.Draw(_gameoverTexture, new Vector2(16, 16), new Color(211, 47, 47));
            }
            spriteBatch.Draw(_restartTexture, new Vector2(10, 46), Color.White);
        }
    }
}
