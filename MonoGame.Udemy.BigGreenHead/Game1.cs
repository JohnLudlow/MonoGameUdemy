using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MonoGame.Udemy.BigGreenHead;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch? _spriteBatch;

    private BouncingThing[]? _bouncingHeads;

    private Texture2D? _background;

    private Song _song;

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

        _bouncingHeads = [.. GenerateHeads(20, _graphics.GraphicsDevice.Viewport.Bounds)];

        base.Initialize();
    }

    private IEnumerable<BouncingThing> GenerateHeads(int count, Rectangle area)
    {
        var rand = new Random();

        for (var i = 0; i < count; i++)
        {
            yield return new BouncingThing
            {
                Area = _graphics.GraphicsDevice.Viewport.Bounds,
                
                Position = new Vector2(
                    rand.Next(area.Left, area.Right), 
                    rand.Next(area.Top, area.Bottom)
                ),

                Velocity = new Vector2(rand.Next(2, 11), rand.Next(2, 11)),
                Sprite = Content.Load<Texture2D>("Assets/Images/greenhead"),
                
                Color = new Color(
                    r: rand.Next(0, 256),
                    g: rand.Next(0, 256),
                    b: rand.Next(0, 256)
                ),
            };
        }
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _background = Content.Load<Texture2D>("Assets/Images/sjb");
        _song = Content.Load<Song>("Assets/Sounds/wakingup");
        MediaPlayer.Play(_song);
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

        foreach (var head in _bouncingHeads)
        {
            head.Update(gameTime);
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
            _spriteBatch.Draw(_background, _graphics.GraphicsDevice.Viewport.Bounds, Color.White);

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
