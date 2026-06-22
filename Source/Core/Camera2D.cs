using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FarmBinisayaDirectX.Core;

public class Camera2D
{
    public Vector2 Position { get; private set; }
    public float Zoom { get; set; } = 1f;

    public Matrix GetTransform()
    {
        return Matrix.CreateTranslation(new Vector3(-Position, 0f)) *
            Matrix.CreateScale(Zoom, Zoom, 1f);
    }

    public void Follow(Vector2 target, Viewport viewport, int worldWidth, int worldHeight)
    {
        Vector2 viewportCenter = new Vector2(viewport.Width, viewport.Height) / (2f * Zoom);
        Vector2 desiredPosition = target - viewportCenter;

        float maxX = MathHelper.Max(0, worldWidth - viewport.Width / Zoom);
        float maxY = MathHelper.Max(0, worldHeight - viewport.Height / Zoom);

        Position = new Vector2(
            MathHelper.Clamp(desiredPosition.X, 0, maxX),
            MathHelper.Clamp(desiredPosition.Y, 0, maxY)
        );
    }

    public Vector2 ScreenToWorld(Vector2 screenPosition)
    {
        return Vector2.Transform(screenPosition, Matrix.Invert(GetTransform()));
    }
}
