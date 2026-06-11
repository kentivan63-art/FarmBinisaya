using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FarmBinisayaDirectX.Maps;

public class TileMap
{
    public int TileSize { get; set; } = 48;
    public int Width { get; private set; }
    public int Height { get; private set; }
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
                _tiles[x, y] = new Tile(type, new Point(x, y));
            }
        }
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

    public void SetTileType(int x, int y, TileType type)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            _tiles[x, y].Type = type;
            _tiles[x, y].Walkable = type != TileType.Water && type != TileType.Stone;
        }
    }

    public void TillTile(int x, int y)
    {
        var tile = GetTile(x, y);
        if (tile != null && tile.Type == TileType.Grass || tile.Type == TileType.Dirt)
        {
            tile.Type = TileType.Farmland;
            tile.IsTilled = true;
        }
    }

    public void WaterTile(int x, int y)
    {
        var tile = GetTile(x, y);
        if (tile != null && tile.Type == TileType.Farmland)
        {
            tile.IsWatered = true;
        }
    }
}
