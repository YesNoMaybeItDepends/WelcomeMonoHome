using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class RendererService : IrendererService
{
  public SpriteBatch _spriteBatch { get; set; }
  public List<IRenderable> _renderables { get; set; }

  RendererService(SpriteBatch SpriteBatch)
  {
    _spriteBatch = SpriteBatch;
    _renderables = new List<IRenderable>();
  }

  public void Draw()
  {
    foreach (IRenderable renderable in _renderables)
    {
      renderable.Draw();
    }
  }
}