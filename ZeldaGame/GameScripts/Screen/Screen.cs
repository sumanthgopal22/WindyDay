using System;
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
        private readonly Game _game;

        public ExitGameCommand(Game game)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public void Execute() => _game.Exit();
    }

    public class StartGameCommand : ICommand
    {
        private readonly Game1 _game;

        public StartGameCommand(Game1 game)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public void Execute() => _game.StartGame();
    }

    // Store menu items
    public record MenuItem(string Label, ICommand Command);

    public class MainMenuScreen
    {
        private readonly List<MenuItem> _items = new();
        private int _selectedIndex;

        private KeyboardState _previousKeyboardState;

        private SpriteFont _titleFont;
        private SpriteFont _itemFont;
        private Texture2D _background;
        private SoundEffect _navigateSound;
        private Song _backgroundMusic;
        private Texture2D _pixel;

        // Position and layout consts
        private static readonly Vector2 TitlePosition = new(100, 50);
        private static readonly Vector2 MenuPosition = new(100, 150);
        private const float ItemSpacing = 40f;
        private const float MarkerOffset = 24f;

        public MainMenuScreen() { }

        public void AddItem(string label, ICommand command)
        {
            if (string.IsNullOrWhiteSpace(label))
                throw new ArgumentException("Label cannot be empty.", nameof(label));

            _items.Add(new MenuItem(label, command ?? throw new ArgumentNullException(nameof(command))));
        }

        public void SelectNext()
        {
            if (_items.Count == 0) return;

            _selectedIndex = (_selectedIndex + 1) % _items.Count;
            _navigateSound?.Play();
        }

        public void SelectPrevious()
        {
            if (_items.Count == 0) return;

            _selectedIndex = (_selectedIndex - 1 + _items.Count) % _items.Count;
            _navigateSound?.Play();
        }

        public void ExecuteSelected()
        {
            if (_items.Count > 0 && _selectedIndex >= 0 && _selectedIndex < _items.Count)
            {
                _items[_selectedIndex].Command.Execute();
            }
        }

        public void LoadContent(ContentManager content, GraphicsDevice device = null)
        {
            _titleFont = content.Load<SpriteFont>("MIDELTANK_Demo");
            _itemFont = content.Load<SpriteFont>("AppleGaramond");
            _background = content.Load<Texture2D>("zelda_bg");
            _navigateSound = content.Load<SoundEffect>("Blip7");
            _backgroundMusic = content.Load<Song>("MainMenuMusic");

            GraphicsDevice targetDevice = device ?? ((IGraphicsDeviceService)content.ServiceProvider.GetService(typeof(IGraphicsDeviceService)))?.GraphicsDevice;

            if (targetDevice != null)
            {
                _pixel = new Texture2D(targetDevice, 1, 1);
                _pixel.SetData(new[] { Color.White });
            }

            PlayMusic();
            _previousKeyboardState = Keyboard.GetState();
        }

        public void PlayMusic()
        {
            if (_backgroundMusic == null) return;

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.4f;
            MediaPlayer.Play(_backgroundMusic);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            if (IsKeyPressed(currentKeyboardState, Keys.Down) || IsKeyPressed(currentKeyboardState, Keys.S))
            {
                SelectNext();
            }
            else if (IsKeyPressed(currentKeyboardState, Keys.Up) || IsKeyPressed(currentKeyboardState, Keys.W))
            {
                SelectPrevious();
            }
            else if (IsKeyPressed(currentKeyboardState, Keys.Enter) || IsKeyPressed(currentKeyboardState, Keys.Space))
            {
                ExecuteSelected();
            }

            _previousKeyboardState = currentKeyboardState;
        }

        private bool IsKeyPressed(KeyboardState currentState, Keys key)
        {
            return currentState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
        }

        // Draw all parts of the menu
        public void Draw(SpriteBatch spriteBatch)
        {
            DrawBackground(spriteBatch);
            DrawTitle(spriteBatch);
            DrawItems(spriteBatch);
        }

        private void DrawBackground(SpriteBatch spriteBatch)
        {
            if (_background == null) return;

            spriteBatch.Draw(
                _background,
                spriteBatch.GraphicsDevice.Viewport.Bounds,
                Color.White
            );
        }

        private void DrawTitle(SpriteBatch spriteBatch)
        {
            if (_titleFont == null) return;

            spriteBatch.DrawString(
                _titleFont,
                "Welcome",
                TitlePosition,
                Color.Black,
                0f,
                Vector2.Zero,
                2f,
                SpriteEffects.None,
                0f
            );
        }

        private void DrawItems(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                Vector2 itemPos = new(MenuPosition.X, MenuPosition.Y + (i * ItemSpacing));
                bool isSelected = (i == _selectedIndex);
                Color textColor = isSelected ? Color.BlueViolet : Color.Black;

                if (isSelected && _itemFont != null)
                {
                    Vector2 markerPos = new(itemPos.X - MarkerOffset, itemPos.Y);
                    spriteBatch.DrawString(_itemFont, ">", markerPos, Color.BlueViolet, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                }

                if (_titleFont != null)
                {
                    spriteBatch.DrawString(_titleFont, _items[i].Label, itemPos, textColor, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                }
            }
        }
    }
}