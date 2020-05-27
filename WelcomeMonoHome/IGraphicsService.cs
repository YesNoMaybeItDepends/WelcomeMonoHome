using Microsoft.Xna.Framework;

public interface IGraphicsService
{
  GraphicsDeviceManager graphics { get; set; }
  int GetScreenWidth();
  int GetScreenHeight();
}