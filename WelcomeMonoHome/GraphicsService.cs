using Microsoft.Xna.Framework;

public class GraphicsService : IGraphicsService
{
  public GraphicsDeviceManager graphics { get; set; }

  public GraphicsService(GraphicsDeviceManager Graphics)
  {
    graphics = Graphics;
  }

  public int GetScreenWidth()
  {
    return graphics.PreferredBackBufferWidth;
  }
  public int GetScreenHeight()
  {
    return graphics.PreferredBackBufferHeight;
  }
}