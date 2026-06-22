using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FarmBinisayaDirectX.Maps;

public class TileMap
{
    public int TileSize { get; set; } = 48;
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int PixelWidth => Width * TileSize;
    public int PixelHeight => Height * TileSize;
    private Tile[,] _tiles;

    public TileMap(int width, int height)
    {
        Width = width;
        Height = height;
        _tiles = new Tile[width, height];
        GenerateMap();
    }

    private void GenerateMap()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                TileType type = DetermineTileType(x, y);
                int spriteId = GetSpriteIdForTileType(type);
                _tiles[x, y] = new Tile(type, new Point(x, y), spriteId);
            }
        }
    }

    /// <summary>
    /// Get sprite ID for each tile type
    /// Sprite IDs correspond to individual tile sprites loaded in Game1
    /// 0 = untiltedSoil (for Grass and Farmland), 1 = tiltedSoil (for tilled Farmland)
    /// Other tile types use color rendering for now
    /// </summary>
    private int GetSpriteIdForTileType(TileType type, bool isTilled = false)
    {
        return type switch
        {
            TileType.Grass => 0,      // Grass uses untilted soil sprite
            TileType.Farmland => isTilled ? 1 : 0,  // 0 = untiltedSoil, 1 = tiltedSoil
            _ => -1  // Use color rendering for other types
        };
    }

    private TileType DetermineTileType(int x, int y)
    {
        // Create a simple map layout similar to Stardew Valley
        // Water in the middle
        if (x >= Width / 2 - 2 && x <= Width / 2 + 2 && y >= Height / 2 - 1 && y <= Height / 2 + 1)
            return TileType.Water;

        // Path through the map
        if (y == Height / 2 || x == Width / 2)
            return TileType.Path;

        // Farmland patches
        if ((x >= 2 && x <= 5 && y >= 2 && y <= 5) || 
            (x >= Width - 6 && x <= Width - 3 && y >= Height - 6 && y <= Height - 3))
            return TileType.Farmland;

        // Random stones
        if (x == 3 && y == 3 || x == Width - 4 && y == Height - 4)
            return TileType.Stone;

        // Default to grass
        return TileType.Grass;
    }

    public Tile GetTile(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
            return null;
        return _tiles[x, y];
    }

    public Tile GetTileAtWorldPosition(Vector2 worldPosition)
    {
        int gridX = (int)(worldPosition.X / TileSize);
        int gridY = (int)(worldPosition.Y / TileSize);
        return GetTile(gridX, gridY);
    }

    public bool IsAreaWalkable(Rectangle area)
    {
        int left = area.Left / TileSize;
        int right = (area.Right - 1) / TileSize;
        int top = area.Top / TileSize;
        int bottom = (area.Bottom - 1) / TileSize;

        for (int x = left; x <= right; x++)
        {
            for (int y = top; y <= bottom; y++)
            {
                Tile tile = GetTile(x, y);
                if (tile == null || !tile.Walkable)
                    return false;
            }
        }

        return true;
    }

    public void SetTileType(int x, int y, TileType type)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            _tiles[x, y].Type = type;
            _tiles[x, y].Walkable = type != TileType.Water && type != TileType.Stone;
            // Update sprite ID based on new tile type
            _tiles[x, y].SpriteId = GetSpriteIdForTileType(type, _tiles[x, y].IsTilled);
        }
    }

    public void TillTile(int x, int y)
    {
        var tile = GetTile(x, y);
        if (tile != null && (tile.Type == TileType.Grass || tile.Type == TileType.Dirt || tile.Type == TileType.Farmland))
        {
            tile.Type = TileType.Farmland;
            tile.IsTilled = true;
            // Update sprite ID to tilted soil (sprite ID 1)
            tile.SpriteId = 1;
        }
    }

    public void WaterTile(int x, int y)
    {
        var tile = GetTile(x, y);
        if (tile != null && tile.Type == TileType.Farmland && tile.IsTilled)
        {
            tile.IsWatered = true;
        }
    }
}
