using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGameReload;

namespace MonoGame.Udemy.First;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _daffySprite;
    private Vector2 _daffySpritePosition;

    private Texture2D _minnieSprite;
    private Vector2 _minnieSpritePosition;

    private SpriteFont _scoreFont;

    private int _score;

    private Vector2 _spriteVelocity = new(5, 1);

    private Song _introSong;
    private SoundEffect _arrowSoundEffect;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferHeight = 1200,
            PreferredBackBufferWidth = 1900
        };

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        Reloader.Initialize(Content, GraphicsDevice, TargetPlatform.DesktopGL);

        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _daffySprite = Content.Load<Texture2D>("Assets/Images/daffy");        
        _minnieSprite = Content.Load<Texture2D>("Assets/Images/cartoon");        
        _scoreFont = Content.Load<SpriteFont>("Assets/Fonts/ScoreFont");
        _introSong = Content.Load<Song>("Assets/Sounds/intro");
        _arrowSoundEffect = Content.Load<SoundEffect>("Assets/Sounds/arrow");

        MediaPlayer.Play(_introSong);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        // TODO: Add your update logic here

        _daffySpritePosition += _spriteVelocity;

        if (_daffySpritePosition.X + _daffySprite.Width  > _graphics.GraphicsDevice.Viewport.Width)
        {
            _spriteVelocity.X = -_spriteVelocity.X;
            _score++;
            _arrowSoundEffect.Play();
        }

        if (_daffySpritePosition.X                       < 0)
            _spriteVelocity.X = -_spriteVelocity.X;

        if (_daffySpritePosition.Y + _daffySprite.Height > _graphics.GraphicsDevice.Viewport.Height)
            _spriteVelocity.Y = -_spriteVelocity.Y;

        if (_daffySpritePosition.Y                       < 0)
            _spriteVelocity.Y = -_spriteVelocity.Y;


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        try
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_daffySprite, _daffySpritePosition, Color.White);
            _spriteBatch.Draw(_minnieSprite, _minnieSpritePosition, Color.White);
            _spriteBatch.DrawString(_scoreFont, _score.ToString(CultureInfo.DefaultThreadCurrentUICulture), new Vector2(10, 10), Color.White);
        }
        finally
        {
            _spriteBatch.End();
        }


        base.Draw(gameTime);
    }

    protected override void Dispose(bool disposing)
    {
        _graphics.Dispose();
        _graphics = null;

        _spriteBatch.Dispose();
        _spriteBatch = null;

        base.Dispose(disposing);
    }
}
