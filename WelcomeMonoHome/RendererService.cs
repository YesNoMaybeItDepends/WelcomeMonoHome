using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class RendererService : IrendererService
{
  public SpriteBatch _spriteBatch { get; set; }

  private List<IRenderable> _renderableAddQueue { get; set; }
  private List<IRenderable> _renderableRenderQueue { get; set; }
  private List<IRenderable> _renderableRemoveQueue { get; set; }

  RendererService(SpriteBatch SpriteBatch)
  {
    _spriteBatch = SpriteBatch;

    _renderableAddQueue = new List<IRenderable>();
    _renderableRenderQueue = new List<IRenderable>();
    _renderableRemoveQueue = new List<IRenderable>();
  }

  public void Run()
  {
    // delete 
    if (_renderableRemoveQueue.Count > 0)
    {
      foreach (IRenderable renderable in _renderableRemoveQueue)
      {
        _renderableRenderQueue.Remove(renderable);
      }
      _renderableRemoveQueue.Clear();
    }

    // add
    if (_renderableAddQueue.Count > 0)
    {
      foreach (IRenderable renderable in _renderableAddQueue)
      {
        _renderableAddQueue.Add(renderable);
      }
      _renderableAddQueue.Clear();
    }

    // draw
    foreach (IRenderable renderable in _renderableRenderQueue)
    {
      renderable.Draw(_spriteBatch);
    }
  }

  public void AddRenderable(IRenderable renderable)
  {
    _renderableRenderQueue.Add(renderable);
  }

  public void RemoveRenderable(IRenderable renderable)
  {
    _renderableRemoveQueue.Add(renderable);
  }


}