using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

public interface IrendererService
{
  SpriteBatch _spriteBatch { get; set; }

  List<IRenderable> _renderables { get; set; }

}