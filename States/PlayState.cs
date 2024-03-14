using ZeroFlip64Web.GameLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZeroFlip64Web.States
{
    class PlayState : IState
    {
        private Texture2D[] _tileTextures;
        private Texture2D _backTexture;
        private Texture2D _numsTexture;
        private Texture2D _cursorTexture;

        private SoundEffectInstance _winSound;
        private SoundEffectInstance _loseSound;

        private Board _board;
        private Vector2 _cursor;

        private bool _gameActive;

        public PlayState()
        {
            _tileTextures = new Texture2D[4];
            for (int i = 0; i < _tileTextures.Length; i++)
            {
                _tileTextures[i] = ZeroFlip64WebGame.Textures[i+""];
            }
            _backTexture = ZeroFlip64WebGame.Textures["back"];
            _numsTexture = ZeroFlip64WebGame.Textures["nums"];
            _cursorTexture = ZeroFlip64WebGame.Textures["cursor"];

            _winSound = ZeroFlip64WebGame.Sounds["win"].CreateInstance();
            _loseSound = ZeroFlip64WebGame.Sounds["lose"].CreateInstance();

            _board = new Board(5, 5);
            _cursor = new Vector2(2, 2);

            _gameActive = true;
        }
        
        public void Update(GameTime gameTime)
        {
            if (_board.Won)
            {
                if (_winSound.State == SoundState.Stopped) ZeroFlip64WebGame.States.Set(new EndState(true));
            }
            else if (_board.Lost)
            {
                if (_loseSound.State == SoundState.Stopped) ZeroFlip64WebGame.States.Set(new EndState(false));
            }

            if (!_gameActive) return;

            if (ZeroFlip64WebGame.Input.WasKeyJustDown(Keys.Right) && _cursor.X < _board.Cols - 1) _cursor.X++;
            if (ZeroFlip64WebGame.Input.WasKeyJustDown(Keys.Left) && _cursor.X > 0) _cursor.X--;
            if (ZeroFlip64WebGame.Input.WasKeyJustDown(Keys.Down) && _cursor.Y < _board.Rows - 1) _cursor.Y++;
            if (ZeroFlip64WebGame.Input.WasKeyJustDown(Keys.Up) && _cursor.Y > 0) _cursor.Y--;
            if (ZeroFlip64WebGame.Input.WasKeyJustDown(Keys.Space))
            {
                bool flipped = _board.FlipTile((int)_cursor.Y, (int)_cursor.X);
                if (_board.Won)
                {
                    _winSound.Play();
                    _gameActive = false;
                }
                else if (_board.Lost)
                {
                    _loseSound.Play();
                    _gameActive = false;
                }
                else if (flipped)
                {
                    ZeroFlip64WebGame.Sounds["ding"].Play(0.5f, 0.0f, 0.0f);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int row = 0; row < _board.Rows; row++)
            {
                for (int col = 0; col < _board.Cols; col++)
                {
                    Tile tile = _board.Tiles[row, col];
                    Texture2D texture = tile.Flipped ? _tileTextures[tile.Value] : _backTexture;
                    spriteBatch.Draw(texture, new Vector2(3 + col * 10, 13 + row * 10), Color.White);
                }
            }

            for (int row = 0; row < _board.Rows; row++)
            {
                DrawCounter(spriteBatch, _board.RowCounters[row], new Vector2(53, 13 + row * 10));
            }
            for (int col = 0; col < _board.Cols; col++)
            {
                DrawCounter(spriteBatch, _board.ColCounters[col], new Vector2(3 + col * 10, 3));
            }

            spriteBatch.Draw(_cursorTexture, new Vector2(2 +_cursor.X * 10, 12 + _cursor.Y * 10), Color.White);
        }

        public void DrawCounter(SpriteBatch spriteBatch, Counter counter, Vector2 position)
        {
            DrawNumber(spriteBatch, counter.Total / 10, position, Color.White);
            DrawNumber(spriteBatch, counter.Total % 10, Vector2.Add(position, new Vector2(4, 0)), Color.White);
            DrawNumber(spriteBatch, counter.ZeroCount, Vector2.Add(position, new Vector2(2, 4)), new Color(211, 47, 47));
        }

        public void DrawNumber(SpriteBatch spriteBatch, int number, Vector2 position, Color color)
        {
            Rectangle rectangle = new Rectangle(number * 3, 0, 3, 4);
            spriteBatch.Draw(_numsTexture, position, rectangle, color);
        }
    }
}
