using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FarmBinisayaDirectX.Entities;

/// <summary>
/// Player - Represents the player character in the game
/// TODO: Add sprite/texture for the player character
/// TODO: Implement movement with keyboard input
/// TODO: Add animation states (idle, walking, farming, etc.)
/// TODO: Add inventory system
/// TODO: Add interaction with tiles (farming, watering, etc.)
/// </summary>
public class Player : Entity
{
    public float Speed { get; set; }
    public int Health { get; set; }
    public int Energy { get; set; }
    
    // TODO: Add these when you have sprite assets
    // private Sprite _idleSprite;
    // private Sprite _walkSprite;
    // private Sprite _farmSprite;

    public Player()
    {
        Name = "Player";
        Speed = 200f; // Pixels per second
        Health = 100;
        Energy = 100;
        Scale = 1f;
    }

    /// <summary>
    /// Handle player input and movement
    /// TODO: Add diagonal movement normalization
    /// TODO: Add collision detection with tiles and other entities
    /// TODO: Add farming tool interaction
    /// </summary>
    public override void Update(GameTime gameTime)
    {
        KeyboardState keyboard = Keyboard.GetState();
        Vector2 movement = Vector2.Zero;

        // Movement input
        if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
            movement.Y -= 1;
        if (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down))
            movement.Y += 1;
        if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
            movement.X -= 1;
        if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
            movement.X += 1;

        // Normalize movement vector
        if (movement != Vector2.Zero)
            movement.Normalize();

        // Apply speed
        Velocity = movement * Speed;

        // Call base update to apply velocity
        base.Update(gameTime);

        // TODO: Add tool interaction
        // if (keyboard.IsKeyDown(Keys.Space))
        //     UseTool();
    }

    /// <summary>
    /// Use the currently equipped tool
    /// TODO: Implement tool system (hoe, watering can, seeds, etc.)
    /// TODO: Check if player is facing a valid tile
    /// TODO: Consume energy when using tools
    /// </summary>
    public void UseTool()
    {
        // TODO: Implement tool usage logic
        // Example: Till the tile in front of player
        // Example: Water the tile in front of player
    }

    /// <summary>
    /// Get the tile position the player is facing
    /// TODO: Implement based on player rotation or last movement direction
    /// </summary>
    public Vector2 GetFacingPosition()
    {
        // TODO: Calculate which tile the player is facing
        // This depends on player direction/rotation
        return Position;
    }

    /// <summary>
    /// Draw the player
    /// TODO: Render sprite based on current animation state
    /// TODO: Add spriteBatch parameter
    /// </summary>
    public override void Draw()
    {
        // TODO: Add sprite rendering
        // Example: _currentSprite.Draw(spriteBatch, Position, Rotation, Scale);
        base.Draw();
    }
}
