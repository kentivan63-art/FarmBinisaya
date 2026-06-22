using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FarmBinisayaDirectX.Maps;
using FarmBinisayaDirectX.Entities;
using FarmBinisayaDirectX.Core;

namespace FarmBinisayaDirectX;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private TileMap _tileMap;
    private Texture2D _tileTexture;
    private Player _player;
    private Camera2D _camera;
    private KeyboardState _previousKeyboardState;

    // Asset loading
    private AssetLoader _assetLoader;
    private Texture2D _faceSprite;
    private Texture2D _coconutTreeSprite;
    
    // Individual tile sprites
    private Texture2D _tiltedSoilSprite;
    private Texture2D _untiltedSoilSprite;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        // Set window size
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        _tileMap = new TileMap(40, 30);
        _player = new Player
        {
            Position = new Vector2(_tileMap.TileSize * 4 + 8, _tileMap.TileSize * 4 + 4)
        };
        _camera = new Camera2D();
        // AssetLoader needs GraphicsDevice, so initialize in LoadContent instead
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        // Initialize AssetLoader with GraphicsDevice
        _assetLoader = new AssetLoader(Content, GraphicsDevice);
        
        // Create a simple white texture for drawing tiles
        _tileTexture = new Texture2D(GraphicsDevice, 1, 1);
        _tileTexture.SetData(new[] { Color.White });

        // Load the face sprite from Content/Sprites/face.xnb
        // The MGCB editor processed face.png into face.xnb
        _faceSprite = _assetLoader.LoadTexture("Sprites/face");

        // Load the coconut tree sprite from Content/Sprites/coconut_tree.xnb
        _coconutTreeSprite = _assetLoader.LoadTexture("Sprites/coconut_tree");

        // Load individual tile sprites
        _tiltedSoilSprite = _assetLoader.LoadTexture("Sprites/tiltedSoil");
        _untiltedSoilSprite = _assetLoader.LoadTexture("Sprites/untiltedSoil");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        KeyboardState keyboardState = Keyboard.GetState();

        _player.Update(gameTime, _tileMap);

        Point facingTile = _player.GetFacingTilePosition(_tileMap.TileSize);
        if (IsNewKeyPress(keyboardState, Keys.Space))
        {
            _tileMap.TillTile(facingTile.X, facingTile.Y);
        }

        if (IsNewKeyPress(keyboardState, Keys.E))
        {
            _tileMap.WaterTile(facingTile.X, facingTile.Y);
        }

        _camera.Follow(_player.Center, GraphicsDevice.Viewport, _tileMap.PixelWidth, _tileMap.PixelHeight);
        _previousKeyboardState = keyboardState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(transformMatrix: _camera.GetTransform(), samplerState: SamplerState.PointClamp);

        // Draw all tiles with depth effect
        for (int x = 0; x < _tileMap.Width; x++)
        {
            for (int y = 0; y < _tileMap.Height; y++)
            {
                var tile = _tileMap.GetTile(x, y);
                if (tile != null)
                {
                    Rectangle destination = new Rectangle(
                        x * _tileMap.TileSize,
                        y * _tileMap.TileSize,
                        _tileMap.TileSize,
                        _tileMap.TileSize
                    );
                    
                    // Use individual tile sprites for farmland, color rendering for others
                    if (tile.SpriteId == 0)
                    {
                        // Draw untilted soil sprite
                        if (_untiltedSoilSprite != null)
                            DrawTileSpriteWithShadow(_untiltedSoilSprite, destination);
                        else
                            System.Diagnostics.Debug.WriteLine("untiltedSoilSprite is null!");
                    }
                    else if (tile.SpriteId == 1)
                    {
                        // Draw tilted soil sprite
                        if (_tiltedSoilSprite != null)
                            DrawTileSpriteWithShadow(_tiltedSoilSprite, destination);
                        else
                            System.Diagnostics.Debug.WriteLine("tiltedSoilSprite is null!");
                    }
                    else
                    {
                        // Fallback: Draw base tile color with slight variation for depth
                        Color baseColor = tile.GetColor();
                        Color shadowColor = new Color(
                            (byte)(baseColor.R * 0.8f),
                            (byte)(baseColor.G * 0.8f),
                            (byte)(baseColor.B * 0.8f)
                        );
                        
                        // Draw shadow offset for 3D effect
                        Rectangle shadowRect = new Rectangle(
                            destination.X + 2,
                            destination.Y + 2,
                            destination.Width,
                            destination.Height
                        );
                        _spriteBatch.Draw(_tileTexture, shadowRect, shadowColor);
                        
                        // Draw main tile
                        _spriteBatch.Draw(_tileTexture, destination, baseColor);
                        
                        // Draw tile border with lighter color for highlight
                        DrawTileBorder(destination, Color.White * 0.2f);
                    }
                }
            }
        }

        // Draw coconut tree sprite at position (300, 200) with shadow
        if (_coconutTreeSprite != null)
        {
            // Draw shadow
            _spriteBatch.Draw(_coconutTreeSprite, new Vector2(305, 205), Color.Black * 0.3f);
            // Draw sprite
            _spriteBatch.Draw(_coconutTreeSprite, new Vector2(300, 200), Color.White);
        }

        DrawFacingTileHighlight();
        _player.Draw(_spriteBatch, _faceSprite, _tileTexture);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawTileBorder(Rectangle rect, Color color)
    {
        _spriteBatch.Draw(_tileTexture, new Rectangle(rect.X, rect.Y, rect.Width, 2), color);
        _spriteBatch.Draw(_tileTexture, new Rectangle(rect.X, rect.Y + rect.Height - 2, rect.Width, 2), color);
        _spriteBatch.Draw(_tileTexture, new Rectangle(rect.X, rect.Y, 2, rect.Height), color);
        _spriteBatch.Draw(_tileTexture, new Rectangle(rect.X + rect.Width - 2, rect.Y, 2, rect.Height), color);
    }

    /// <summary>
    /// Draw a tile sprite scaled to fit the tile size exactly
    /// </summary>
    private void DrawTileSpriteWithShadow(Texture2D sprite, Rectangle destination)
    {
        // Draw sprite scaled to fit the tile size exactly (no gaps)
        // This ensures sprites of different sizes all fit the 48x48 tile grid
        _spriteBatch.Draw(sprite, destination, Color.White);
    }

    private void DrawFacingTileHighlight()
    {
        Point facingTile = _player.GetFacingTilePosition(_tileMap.TileSize);
        if (_tileMap.GetTile(facingTile.X, facingTile.Y) == null)
            return;

        Rectangle rect = new Rectangle(
            facingTile.X * _tileMap.TileSize,
            facingTile.Y * _tileMap.TileSize,
            _tileMap.TileSize,
            _tileMap.TileSize
        );

        _spriteBatch.Draw(_tileTexture, rect, Color.Yellow * 0.25f);
        DrawTileBorder(rect, Color.Yellow * 0.75f);
    }

    private bool IsNewKeyPress(KeyboardState keyboardState, Keys key)
    {
        return keyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
    }
}
