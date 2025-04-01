using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Udemy.BigGreenHead;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _greenHeadTexture;

    private Vector2 _spriteLocation = new(0, 0);
    private Vector2 _spriteVelocity = new(5, 2);


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

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        _greenHeadTexture = Content.Load<Texture2D>("Assets/Images/greenhead");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        //
        // _spriteLocation += new Vector2(1, .5f);

        _spriteLocation += _spriteVelocity * (float)(gameTime.ElapsedGameTime.TotalSeconds * 60);

        if (_spriteLocation.X < 0 || ((_spriteLocation.X + _greenHeadTexture.Width)  > _graphics.GraphicsDevice.Viewport.Width))
            _spriteVelocity.X = -_spriteVelocity.X;

        if (_spriteLocation.Y < 0 || ((_spriteLocation.Y + _greenHeadTexture.Height) > _graphics.GraphicsDevice.Viewport.Height))
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
            _spriteBatch.Draw(_greenHeadTexture, _spriteLocation, Color.White);            
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
