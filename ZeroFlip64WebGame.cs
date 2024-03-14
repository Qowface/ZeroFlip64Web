using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using ZeroFlip64Web.GameLogic;
using ZeroFlip64Web.Helpers;
using ZeroFlip64Web.States;

namespace ZeroFlip64Web
{
    public class ZeroFlip64WebGame : Game
    {
        public const int GameWidth = 64;
        public const int GameHeight = 64;
        public const int GameScale = 8;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private RenderTarget2D _nativeRenderTarget;
        private Rectangle _actualScreenRectangle;

        public static Dictionary<string, Texture2D> Textures;
        public static Dictionary<string, SoundEffect> Sounds;

        public static Input Input;

        public static StateManager States;

        public ZeroFlip64WebGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _nativeRenderTarget = new RenderTarget2D(GraphicsDevice, GameWidth, GameHeight);
            _actualScreenRectangle = new Rectangle(x: 0, y: 0, width: GameWidth * GameScale, height: GameHeight * GameScale);
            graphics.PreferredBackBufferWidth = GameWidth * GameScale;
            graphics.PreferredBackBufferHeight = GameHeight * GameScale;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Textures = new Dictionary<string, Texture2D>();
            Textures.Add("0", Content.Load<Texture2D>("0"));
            Textures.Add("1", Content.Load<Texture2D>("1"));
            Textures.Add("2", Content.Load<Texture2D>("2"));
            Textures.Add("3", Content.Load<Texture2D>("3"));
            Textures.Add("back", Content.Load<Texture2D>("back"));
            Textures.Add("cursor", Content.Load<Texture2D>("cursor"));
            Textures.Add("gameover", Content.Load<Texture2D>("gameover"));
            Textures.Add("nums", Content.Load<Texture2D>("nums"));
            Textures.Add("restart", Content.Load<Texture2D>("restart"));
            Textures.Add("start", Content.Load<Texture2D>("start"));
            Textures.Add("title", Content.Load<Texture2D>("title"));
            Textures.Add("youwin", Content.Load<Texture2D>("youwin"));

            Sounds = new Dictionary<string, SoundEffect>();
            Sounds.Add("ding", Content.Load<SoundEffect>("ding"));
            Sounds.Add("lose", Content.Load<SoundEffect>("lose"));
            Sounds.Add("win", Content.Load<SoundEffect>("win"));

            Input = new Input();

            States = new StateManager();
            States.Push(new MenuState());
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = default;
            try { gamePadState = GamePad.GetState(PlayerIndex.One); }
            catch (NotImplementedException) { /* ignore gamePadState */ }

            if (keyboardState.IsKeyDown(Keys.Escape) ||
                keyboardState.IsKeyDown(Keys.Back) ||
                gamePadState.Buttons.Back == ButtonState.Pressed)
            {
                try { Exit(); }
                catch (PlatformNotSupportedException) { /* ignore */ }
            }

            Input.Update(gameTime);

            States.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_nativeRenderTarget);
            GraphicsDevice.Clear(new Color(27, 38, 50));
            spriteBatch.Begin();

            States.Draw(gameTime, spriteBatch);

            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(_nativeRenderTarget, _actualScreenRectangle, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
