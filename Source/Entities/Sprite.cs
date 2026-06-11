using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FarmBinisayaDirectX.Entities;

/// <summary>
/// Sprite - Represents a 2D sprite/texture that can be drawn
/// TODO: Add animation support (multiple frames, sprite sheets)
/// TODO: Add origin/pivot point support
/// TODO: Add color tinting support
/// TODO: Add sprite effects (flip horizontal/vertical)
/// </summary>
public class Sprite
{
    public Texture2D Texture { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Vector2 Origin { get; set; }
    public Color Color { get; set; }
    public SpriteEffects Effects { get; set; }

    // TODO: Add animation properties
    // public int CurrentFrame { get; set; }
    // public int TotalFrames { get; set; }
    // public float FrameTime { get; set; }
    // public float AnimationTimer { get; set; }

    public Sprite(Texture2D texture)
    {
        Texture = texture;
        Width = texture.Width;
        Height = texture.Height;
        Origin = Vector2.Zero;
        Color = Color.White;
        Effects = SpriteEffects.None;
    }

    /// <summary>
    /// Draw the sprite at the specified position
    /// TODO: Add spriteBatch parameter
    /// TODO: Implement animation frame selection
    /// </summary>
    public void Draw(Vector2 position, float rotation = 0f, float scale = 1f)
    {
        // TODO: Add actual drawing code when spriteBatch is available
        // spriteBatch.Draw(Texture, position, null, Color, rotation, Origin, scale, Effects, 0f);
    }

    /// <summary>
    /// Draw the sprite in a specific rectangle (for sprite sheets)
    /// TODO: Implement for sprite sheet animation
    /// </summary>
    public void Draw(Vector2 position, Rectangle sourceRect, float rotation = 0f, float scale = 1f)
    {
        // TODO: Add drawing code with source rectangle
        // spriteBatch.Draw(Texture, position, sourceRect, Color, rotation, Origin, scale, Effects, 0f);
    }

    /// <summary>
    /// Set the origin to the center of the sprite
    /// TODO: Call this for sprites that should rotate around their center
    /// </summary>
    public void SetCenterOrigin()
    {
        Origin = new Vector2(Width / 2f, Height / 2f);
    }

    /// <summary>
    /// Update animation (if sprite has multiple frames)
    /// TODO: Implement when adding animation support
    /// </summary>
    public void Update(GameTime gameTime)
    {
        // TODO: Update animation frame based on elapsed time
        // AnimationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        // if (AnimationTimer >= FrameTime)
        // {
        //     CurrentFrame = (CurrentFrame + 1) % TotalFrames;
        //     AnimationTimer = 0f;
        // }
    }
}
