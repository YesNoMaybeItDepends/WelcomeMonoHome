using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IRenderable
{
  Vector2 position { get; set; }

  //bool isVisible { get; set; }
  void Draw(SpriteBatch spriteBatch);
}