using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameReload;

namespace MonoGame.Udemy.First;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D _daffySprite;
    Vector2 _daffySpritePosition;

    Texture2D _minnieSprite;
    Vector2 _minnieSpritePosition;


    Vector2 _spriteVelocity = new(5, 1);

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
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _daffySpritePosition += _spriteVelocity;
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
            _spriteBatch.Draw(_daffySprite, _daffySpritePosition, Color.White);
            _spriteBatch.Draw(_minnieSprite, _minnieSpritePosition, Color.White);
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
