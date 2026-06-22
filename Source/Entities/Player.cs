using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FarmBinisayaDirectX.Maps;

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
    public Point FacingDirection { get; private set; } = new Point(0, 1);
    public Point Size { get; set; } = new Point(32, 40);
    public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y);
    public Vector2 Center => new Vector2(Position.X + Size.X / 2f, Position.Y + Size.Y / 2f);
    
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
    public void Update(GameTime gameTime, TileMap tileMap)
    {
        KeyboardState keyboard = Keyboard.GetState();
        Vector2 movement = Vector2.Zero;
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

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
        {
            movement.Normalize();
            FacingDirection = GetFacingDirectionFromMovement(movement);
        }

        Velocity = movement * Speed;
        MoveWithCollision(Velocity * deltaTime, tileMap);
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
    public Point GetFacingTilePosition(int tileSize)
    {
        Point currentTile = new Point((int)(Center.X / tileSize), (int)(Center.Y / tileSize));
        return currentTile + FacingDirection;
    }

    /// <summary>
    /// Draw the player
    /// TODO: Render sprite based on current animation state
    /// TODO: Add spriteBatch parameter
    /// </summary>
    public void Draw(SpriteBatch spriteBatch, Texture2D texture, Texture2D fallbackTexture)
    {
        Rectangle destination = Bounds;

        if (texture != null)
        {
            spriteBatch.Draw(texture, destination, Color.White);
            return;
        }

        spriteBatch.Draw(fallbackTexture, destination, Color.OrangeRed);
    }

    private void MoveWithCollision(Vector2 movement, TileMap tileMap)
    {
        if (movement == Vector2.Zero)
            return;

        Rectangle nextX = Bounds;
        nextX.Offset((int)movement.X, 0);
        if (tileMap.IsAreaWalkable(nextX))
            Position += new Vector2(movement.X, 0);

        Rectangle nextY = Bounds;
        nextY.Offset(0, (int)movement.Y);
        if (tileMap.IsAreaWalkable(nextY))
            Position += new Vector2(0, movement.Y);
    }

    private static Point GetFacingDirectionFromMovement(Vector2 movement)
    {
        if (System.Math.Abs(movement.X) > System.Math.Abs(movement.Y))
            return movement.X > 0 ? new Point(1, 0) : new Point(-1, 0);

        return movement.Y > 0 ? new Point(0, 1) : new Point(0, -1);
    }
}
