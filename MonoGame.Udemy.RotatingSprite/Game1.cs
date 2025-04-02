using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Udemy.RotatingSprite;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _texture2D;
    private float _rotation;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
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
        _texture2D = Content.Load<Texture2D>("Assets/Images/LudwigVonDrake");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        _rotation += 0.01f;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();

        var location = GraphicsDevice.Viewport.Bounds.Center.ToVector2();
        var origin = _texture2D.Bounds.Center.ToVector2();

        _spriteBatch.Draw(
            _texture2D, location, _texture2D.Bounds, Color.White, _rotation, origin,
            
            1.0f, SpriteEffects.None, 1.0f
        );
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
