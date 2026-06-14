using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FarmBinisayaDirectX.Entities;

/// <summary>
/// AssetLoader - Handles loading and managing game assets (textures, sprites, sounds, etc.)
/// TODO: Add methods to load different asset types (sounds, music, fonts, etc.)
/// TODO: Implement asset caching to avoid reloading the same assets
/// TODO: Add error handling for missing assets
/// </summary>
public class AssetLoader
{
    private readonly ContentManager _content;
    private readonly GraphicsDevice _graphicsDevice;
    private Dictionary<string, Texture2D> _textureCache;

    public AssetLoader(ContentManager content, GraphicsDevice graphicsDevice)
    {
        _content = content;
        _graphicsDevice = graphicsDevice;
        _textureCache = new Dictionary<string, Texture2D>();
    }

    /// <summary>
    /// Load a texture from the Content folder
    /// Usage: LoadTexture("Sprites/face") looks for Content/Sprites/face.xnb
    /// The MGCB editor processes PNG files into .xnb files
    /// Call this method in Game.LoadContent() to preload assets
    /// </summary>
    public Texture2D LoadTexture(string assetName)
    {
        if (_textureCache.ContainsKey(assetName))
            return _textureCache[assetName];

        // Load the actual texture from Content folder
        Texture2D texture = _content.Load<Texture2D>(assetName);
        _textureCache[assetName] = texture;
        return texture;
    }

    /// <summary>
    /// Creates a fallback colored texture when actual assets aren't loaded yet
    /// TODO: Remove this method once you have real sprite assets
    /// </summary>
    private Texture2D CreateFallbackTexture(string assetName)
    {
        // Create different colors based on asset name for testing
        Color color = assetName.ToLower() switch
        {
            "player" => Color.Red,
            "npc" => Color.Blue,
            "crop" => Color.Green,
            "tool" => Color.Yellow,
            _ => Color.White
        };

        Texture2D texture = new Texture2D(_graphicsDevice, 32, 32);
        Color[] data = new Color[32 * 32];
        for (int i = 0; i < data.Length; i++)
            data[i] = color;
        texture.SetData(data);
        return texture;
    }

    /// <summary>
    /// Unload all cached assets
    /// TODO: Call this in Game.UnloadContent() when switching scenes or exiting
    /// </summary>
    public void UnloadAll()
    {
        _textureCache.Clear();
        _content.Unload();
    }
}
