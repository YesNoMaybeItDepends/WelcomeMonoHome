using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

public interface IrendererService
{
  SpriteBatch _spriteBatch { get; set; }

  List<IRenderable> _renderableAddQueue { get; set; }
  List<IRenderable> _renderableRenderQueue { get; set; }
  List<IRenderable> _renderableRemoveQueue { get; set; }

  void Run();
  void AddRenderable(IRenderable renderable);
  void RemoveRenderable(IRenderable renderable);
}