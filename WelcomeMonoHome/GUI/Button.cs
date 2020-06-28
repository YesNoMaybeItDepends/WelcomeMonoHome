using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Button : Renderable
{
  public Texture2D _texture;
  public Vector2 position { get; set; }
  Vector2 centerPosition;
  Vector2 topLeftPosition;
  int width;
  int height;
  int borderSize = 5;
  Color borderColor;
  Color buttonColor;
  Color textColor;

  public Button(Vector2 CenterPosition, int TotalWidth, int TotalHeight, Color ButtonColor, Color BorderColor)
  {
    _texture = ServiceLocator.GetService<IContentManagerService>().GetTexture("pixel");

    centerPosition = CenterPosition;
    width = TotalWidth;
    height = TotalHeight;
    topLeftPosition.X = centerPosition.X - width / 2;
    topLeftPosition.Y = centerPosition.Y - height / 2;

    buttonColor = ButtonColor;
    borderColor = BorderColor;

    IRendererService renderer = ServiceLocator.GetService<IRendererService>();
    renderer.AddRenderable(this);
  }

  public override void Draw(SpriteBatch spriteBatch)
  {
    // draw black border
    spriteBatch.Draw(_texture, new Rectangle((int)topLeftPosition.X - borderSize, (int)topLeftPosition.Y - borderSize, width + borderSize * 2, height + borderSize * 2), borderColor);

    // draw button
    spriteBatch.Draw(_texture, new Rectangle((int)topLeftPosition.X, (int)topLeftPosition.Y, width, height), buttonColor);
  }
}