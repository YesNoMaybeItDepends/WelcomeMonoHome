using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class RectanglePrimitive : Renderable
{
  Texture2D _pixel;
  public Rectangle rectangle;
  Color _color;
  float _transparency;

  public RectanglePrimitive(Rectangle Rectangle, Color Color, float Transparency)
  {
    rectangle = Rectangle;
    _color = Color;
    _transparency = Transparency;
    _pixel = ServiceLocator.GetService<IContentManagerService>().GetTexture("pixel");
  }

  public override void Draw(SpriteBatch spriteBatch)
  {
    spriteBatch.Draw(_pixel, rectangle, _color * _transparency);
  }
}