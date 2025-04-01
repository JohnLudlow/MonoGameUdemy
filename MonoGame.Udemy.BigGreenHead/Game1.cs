using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Udemy.BigGreenHead;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _greenHeadTexture;

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

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        try
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_greenHeadTexture, Vector2.Zero, Color.White);
            _spriteBatch.Draw(_greenHeadTexture, new Vector2(_greenHeadTexture.Width, _greenHeadTexture.Height), Color.White);            
            _spriteBatch.Draw(_greenHeadTexture, new Vector2(_greenHeadTexture.Width * 2, _greenHeadTexture.Height * 2), Color.Red);
            _spriteBatch.Draw(
                _greenHeadTexture, 
                new Vector2(
                    _graphics.GraphicsDevice.Viewport.Width  - _greenHeadTexture.Width, 
                    _graphics.GraphicsDevice.Viewport.Height - _greenHeadTexture.Height
                ), 
                Color.White
            );
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
