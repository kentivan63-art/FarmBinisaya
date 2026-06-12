using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FarmBinisayaDirectX.Maps;
using FarmBinisayaDirectX.Entities;

namespace FarmBinisayaDirectX;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private TileMap _tileMap;
    private Texture2D _tileTexture;
    private MouseState _previousMouseState;

    // Asset loading
    private AssetLoader _assetLoader;
    private Texture2D _faceSprite;

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
        _tileMap = new TileMap(20, 15);
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
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Update player and entities
        // _player.Update(gameTime);
        // foreach (var entity in _entities)
        //     entity.Update(gameTime);

        // Handle mouse input for tile interaction
        MouseState mouseState = Mouse.GetState();
        
        if (mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
        {
            // Left click to till tile
            var tile = _tileMap.GetTileAtWorldPosition(new Vector2(mouseState.X, mouseState.Y));
            if (tile != null)
            {
                _tileMap.TillTile(tile.GridPosition.X, tile.GridPosition.Y);
            }
        }
        
        if (mouseState.RightButton == ButtonState.Pressed && _previousMouseState.RightButton == ButtonState.Released)
        {
            // Right click to water tile
            var tile = _tileMap.GetTileAtWorldPosition(new Vector2(mouseState.X, mouseState.Y));
            if (tile != null)
            {
                _tileMap.WaterTile(tile.GridPosition.X, tile.GridPosition.Y);
            }
        }

        _previousMouseState = mouseState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        // Draw all tiles
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
                    
                    _spriteBatch.Draw(_tileTexture, destination, tile.GetColor());
                    
                    // Draw tile border
                    DrawTileBorder(destination, Color.Black * 0.3f);
                }
            }
        }

        // Draw the face sprite at position (100, 100)
        if (_faceSprite != null)
        {
            _spriteBatch.Draw(_faceSprite, new Vector2(100, 100), Color.White);
        }

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
}
