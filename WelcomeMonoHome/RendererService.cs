using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class RendererService : IRendererService
{
  public SpriteBatch _spriteBatch { get; set; }

  public Camera _camera;
  GraphicsDeviceManager _graphicsDevice;

  public List<IRenderable> _renderableAddQueue { get; set; }
  public List<IRenderable> _renderableRenderQueue { get; set; }
  public List<IRenderable> _renderableRemoveQueue { get; set; }

  public RendererService(SpriteBatch SpriteBatch, Camera camera)
  {
    _spriteBatch = SpriteBatch;
    _camera = camera;
    _graphicsDevice = ServiceLocator.GetService<IGraphicsService>().graphics;
    _camera._pos = new Vector2(_graphicsDevice.PreferredBackBufferWidth / 2, _graphicsDevice.PreferredBackBufferHeight / 2);

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
        _renderableRenderQueue.Add(renderable);
      }
      _renderableAddQueue.Clear();
    }

    // draw
    _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, _camera.get_transformation(_graphicsDevice.GraphicsDevice));

    foreach (IRenderable renderable in _renderableRenderQueue)
    {
      renderable.Draw(_spriteBatch);
    }

    _spriteBatch.End();
  }

  public void AddRenderable(IRenderable renderable)
  {
    _renderableAddQueue.Add(renderable);
  }

  public void RemoveRenderable(IRenderable renderable)
  {
    _renderableRemoveQueue.Add(renderable);
  }


}