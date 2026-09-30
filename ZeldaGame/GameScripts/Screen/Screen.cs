using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ZeldaGame
{
    public class ExitGameCommand : ICommand
    {
        private readonly Game1 _game;

        public ExitGameCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.Exit();
        }
    }

    public class StartGameCommand : ICommand
    {
        private readonly Game1 _game;

        public StartGameCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.StartGame();
        }
    }

    public class MainMenuScreen
    {
        private readonly List<(string Label, ICommand Command)> _items = new();
        private int _selectedIndex = 0;

        private KeyboardState _previousKeyboardState;

        private SpriteFont _font1;
        private SpriteFont _font2;
        private Texture2D _background;
        private SoundEffect _soundEffect;
        private Song _music;
        private Texture2D _pixel;

        private Vector2 _menuPosition = new Vector2(100, 100);
        private Vector2 _titlePosition = new Vector2(100, 50);

        public MainMenuScreen() { }

        public void AddItem(string label, ICommand command)
        {
            _items.Add((label, command));
        }

        public void SelectNext()
        {
            if (_items.Count > 0)
            {
                _selectedIndex = (_selectedIndex + 1) % _items.Count;
                _soundEffect?.Play();
            }
        }

        public void SelectPrevious()
        {
            if (_items.Count > 0)
            {
                _selectedIndex = (_selectedIndex - 1 + _items.Count) % _items.Count;
                _soundEffect?.Play();
            }
        }

        public void ExecuteSelected()
        {
            if (_items.Count > 0 && _selectedIndex < _items.Count)
            {
                _items[_selectedIndex].Command.Execute();
            }
        }

        public void LoadContent(ContentManager content, GraphicsDevice device)
        {
            _font1 = content.Load<SpriteFont>("MIDELTANK_Demo");
            _font2 = content.Load<SpriteFont>("AppleGaramond");
            _background = content.Load<Texture2D>("zelda_bg");
            _soundEffect = content.Load<SoundEffect>("Blip7");
            _music = content.Load<Song>("MainMenuMusic");

            GraphicsDevice targetDevice = device ?? ((IGraphicsDeviceService)content.ServiceProvider.GetService(typeof(IGraphicsDeviceService)))?.GraphicsDevice;

            if (targetDevice != null)
            {
                _pixel = new Texture2D(targetDevice, 1, 1);
                _pixel.SetData(new[] { Color.White });
            }

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.4f;
            MediaPlayer.Play(_music);

            _previousKeyboardState = Keyboard.GetState();
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Detect down
            bool downPressed = (currentKeyboardState.IsKeyDown(Keys.Down) && _previousKeyboardState.IsKeyUp(Keys.Down)) ||
                                 (currentKeyboardState.IsKeyDown(Keys.S) && _previousKeyboardState.IsKeyUp(Keys.S));

            // Detect up
            bool upPressed = (currentKeyboardState.IsKeyDown(Keys.Up) && _previousKeyboardState.IsKeyUp(Keys.Up)) ||
                               (currentKeyboardState.IsKeyDown(Keys.W) && _previousKeyboardState.IsKeyUp(Keys.W));

            // Detect enter
            bool enterPressed = currentKeyboardState.IsKeyDown(Keys.Enter) && _previousKeyboardState.IsKeyUp(Keys.Enter);

            if (downPressed)
            {
                SelectNext();
            }
            else if (upPressed)
            {
                SelectPrevious();
            }
            else if (enterPressed)
            {
                ExecuteSelected();
            }

            _previousKeyboardState = currentKeyboardState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_background != null)
            {
                spriteBatch.Draw(
                    _background,
                    new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height),
                    Color.White
                );
            }

            if (_font1 != null)
            {
                spriteBatch.DrawString(
                    _font1,
                    "Welcome",
                    _titlePosition,
                    Color.Black,
                    0f,
                    Vector2.Zero,
                    2f,
                    SpriteEffects.None,
                    0f
                );
            }

            for (int i = 0; i < _items.Count; i++)
            {
                Vector2 pos = new Vector2(_menuPosition.X, _menuPosition.Y + i * 40f);
                bool selected = i == _selectedIndex;

                if (selected && _font2 != null)
                {
                    Vector2 markerPos = new Vector2(pos.X - 24f, pos.Y);
                    spriteBatch.DrawString(
                        _font2,
                        ">",
                        markerPos,
                        Color.BlueViolet,
                        0f,
                        Vector2.Zero,
                        1.5f,
                        SpriteEffects.None,
                        0f
                    );
                }

                if (_font1 != null)
                {
                    spriteBatch.DrawString(
                        _font1,
                        _items[i].Label,
                        pos,
                        selected ? Color.BlueViolet : Color.Black,
                        0f,
                        Vector2.Zero,
                        1.5f,
                        SpriteEffects.None,
                        0f
                    );
                }
            }
        }
    }
}