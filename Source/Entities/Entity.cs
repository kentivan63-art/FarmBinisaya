using Microsoft.Xna.Framework;

namespace FarmBinisayaDirectX.Entities;

/// <summary>
/// Entity - Base class for all game objects (player, NPCs, crops, tools, etc.)
/// TODO: Inherit from this class for specific game objects
/// TODO: Add collision detection logic
/// TODO: Add animation support
/// </summary>
public class Entity
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public float Rotation { get; set; }
    public float Scale { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; }

    // TODO: Add these properties when you have sprite assets loaded
    // public Sprite Sprite { get; set; }
    // public Rectangle BoundingBox => new Rectangle(
    //     (int)Position.X, (int)Position.Y,
    //     (int)(Sprite.Width * Scale), (int)(Sprite.Height * Scale)
    // );

    public Entity()
    {
        Position = Vector2.Zero;
        Velocity = Vector2.Zero;
        Rotation = 0f;
        Scale = 1f;
        IsActive = true;
        Name = "Entity";
    }

    /// <summary>
    /// Update entity logic (movement, AI, etc.)
    /// TODO: Override this in derived classes for specific behavior
    /// TODO: Add deltaTime parameter for frame-rate independent updates
    /// </summary>
    public virtual void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Position += Velocity * deltaTime;
    }

    /// <summary>
    /// Draw the entity
    /// TODO: Implement sprite rendering when assets are loaded
    /// TODO: Add spriteBatch parameter
    /// </summary>
    public virtual void Draw()
    {
        // TODO: Add drawing code here
        // Example: Sprite?.Draw(spriteBatch, Position, Rotation, Scale);
    }

    /// <summary>
    /// Check collision with another entity
    /// TODO: Implement collision detection
    /// TODO: Use bounding box or circle collision
    /// </summary>
    public virtual bool CollidesWith(Entity other)
    {
        // TODO: Implement collision logic
        return false;
    }
}
