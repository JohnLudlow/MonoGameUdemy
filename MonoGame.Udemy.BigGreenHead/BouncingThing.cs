using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Udemy.BigGreenHead;

public class BouncingThing
{
    public required Vector2 Position { get; set; }
    public required Vector2 Velocity { get; set; }
    public required Texture2D Sprite { get; set; }
    public required Rectangle Area { get; set; }

    public void Update(GameTime gameTime)
    {
        Position += Velocity * (float)(gameTime.ElapsedGameTime.TotalSeconds * 60);

        if (Sprite is null) 
            throw new InvalidOperationException($"Cannot call {nameof(Update)} when {nameof(Sprite)} is null");

        if (Position.X < 0 || ((Position.X + Sprite.Width) > Area.Width))
        {
            Velocity = Velocity with
            {
                X = -Velocity.X
            };
        }

        if (Position.Y < 0 || ((Position.Y + Sprite.Height) > Area.Height))
        {
            Velocity = Velocity with
            {
                Y = -Velocity.Y
            };
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        ArgumentNullException.ThrowIfNull(gameTime);
        ArgumentNullException.ThrowIfNull(spriteBatch);

        if (Sprite is null) 
            throw new InvalidOperationException($"Cannot call {nameof(Draw)} when {nameof(Sprite)} is null");
        
        spriteBatch.Draw(Sprite, Position, Color.White);
    }
}