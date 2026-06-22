using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FarmBinisayaDirectX.Maps;

/// <summary>
/// Tileset - Manages a tileset image containing multiple tile sprites
/// A tileset is a single image with tiles arranged in a grid
/// Each tile can be accessed by its ID (index in the grid)
/// TODO: Create a tileset PNG image with your tile sprites
/// TODO: Add the tileset to Content/Sprites folder and process with MGCB
/// </summary>
public class Tileset
{
    private Texture2D _texture;
    private int _tileSize;
    private int _tilesPerRow;

    public Tileset(Texture2D texture, int tileSize)
    {
        _texture = texture;
        _tileSize = tileSize;
        _tilesPerRow = _texture.Width / tileSize;
    }

    /// <summary>
    /// Get the source rectangle for a specific tile ID
    /// Tile ID 0 is top-left, ID 1 is next to it, etc.
    /// </summary>
    public Rectangle GetTileSourceRect(int spriteId)
    {
        int x = spriteId % _tilesPerRow;
        int y = spriteId / _tilesPerRow;
        
        return new Rectangle(
            x * _tileSize,
            y * _tileSize,
            _tileSize,
            _tileSize
        );
    }

    /// <summary>
    /// Draw a specific tile from the tileset
    /// </summary>
    public void DrawTile(SpriteBatch spriteBatch, int spriteId, Rectangle destination, Color color)
    {
        Rectangle sourceRect = GetTileSourceRect(spriteId);
        spriteBatch.Draw(_texture, destination, sourceRect, color);
    }

    /// <summary>
    /// Draw a specific tile from the tileset with shadow for depth
    /// </summary>
    public void DrawTileWithShadow(SpriteBatch spriteBatch, int spriteId, Rectangle destination, Color color)
    {
        // Draw shadow
        Rectangle shadowDest = new Rectangle(destination.X + 2, destination.Y + 2, destination.Width, destination.Height);
        Rectangle sourceRect = GetTileSourceRect(spriteId);
        spriteBatch.Draw(_texture, shadowDest, sourceRect, Color.Black * 0.3f);
        
        // Draw main tile
        spriteBatch.Draw(_texture, destination, sourceRect, color);
    }
}
