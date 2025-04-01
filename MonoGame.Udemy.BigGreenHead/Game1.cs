using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Udemy.BigGreenHead;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch? _spriteBatch;

    private BouncingThing[]?_bouncingHeads;


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

        _bouncingHeads = [
            new BouncingThing
            {
                Area = _graphics.GraphicsDevice.Viewport.Bounds,
                Position = new Vector2(0, 0),
                Velocity = new Vector2(5, 2),
                Sprite = Content.Load<Texture2D>("Assets/Images/greenhead")
            },

            new BouncingThing
            {
                Area = _graphics.GraphicsDevice.Viewport.Bounds,
                Position = new Vector2(500, 200),
                Velocity = new Vector2(-5, -2),
                Sprite = Content.Load<Texture2D>("Assets/Images/greenhead")
            }
        ];

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (_bouncingHeads is null)
            throw new InvalidOperationException($"Cannot call {nameof(Update)} before {nameof(Initialize)}");

        // TODO: Add your update logic here
        //
        // _spriteLocation += new Vector2(1, .5f);

        foreach (var v in _bouncingHeads)
        {
            v.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        if (_spriteBatch is null) throw new InvalidOperationException($"Cannot draw when {nameof(_spriteBatch)} is null");

        try
        {
            _spriteBatch.Begin();

            foreach (var bouncer in _bouncingHeads ?? [])
            {
                bouncer.Draw(gameTime, _spriteBatch);
            }
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
        _spriteBatch?.Dispose();

        base.Dispose(disposing);
    }
}
