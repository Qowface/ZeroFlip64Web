using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ZeroFlip64Web.States
{
    public class StateManager : IState
    {
        private Stack<IState> _states;

        public StateManager()
        {
            _states = new Stack<IState>();
        }

        public void Push(IState state)
        {
            _states.Push(state);
        }

        public IState Pop()
        {
            return _states.Pop();
        }

        public IState Set(IState state)
        {
            IState previousState = Pop();
            Push(state);
            return previousState;
        }
        
        public void Update(GameTime gameTime)
        {
            _states.Peek().Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _states.Peek().Draw(gameTime, spriteBatch);
        }
    }
}
