using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ZeroFlip64Web.Helpers
{
    public class Input
    {
        private KeyboardState _currentKeys;
        private KeyboardState _previousKeys;

        public Input()
        {
            _currentKeys = _previousKeys = Keyboard.GetState();
        }

        public void Update(GameTime gameTime)
        {
            _previousKeys = _currentKeys;
            _currentKeys = Keyboard.GetState();
        }

        public bool WasKeyJustDown(Keys key)
        {
            return _currentKeys.IsKeyDown(key) && !_previousKeys.IsKeyDown(key);
        }
    }
}
