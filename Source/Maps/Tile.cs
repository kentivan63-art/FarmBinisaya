using Microsoft.Xna.Framework;

namespace FarmBinisayaDirectX.Maps;

public enum TileType
{
    Grass,
    Dirt,
    Water,
    Stone,
    Farmland,
    Path
}

public class Tile
{
    public TileType Type { get; set; }
    public Point GridPosition { get; set; }
    public bool Walkable { get; set; }
    public bool IsTilled { get; set; }
    public bool IsWatered { get; set; }

    public Tile(TileType type, Point gridPosition)
    {
        Type = type;
        GridPosition = gridPosition;
        Walkable = type != TileType.Water && type != TileType.Stone;
        IsTilled = false;
        IsWatered = false;
    }

    public Color GetColor()
    {
        return Type switch
        {
            TileType.Grass => new Color(76, 153, 0),
            TileType.Dirt => new Color(139, 69, 19),
            TileType.Water => new Color(30, 144, 255),
            TileType.Stone => new Color(128, 128, 128),
            TileType.Farmland => IsWatered ? new Color(101, 67, 33) : new Color(139, 69, 19),
            TileType.Path => new Color(194, 178, 128),
            _ => Color.Magenta
        };
    }
}
