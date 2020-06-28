using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Button : Renderable
{
  public Texture2D _texture;
  public SpriteFont _font;

  public Vector2 position { get; set; }
  Vector2 centerPosition;
  Vector2 topLeftPosition;
  int width;
  int height;
  int borderSize = 5;

  Color borderColor;
  Color buttonColor;
  Color textColor = Color.Black;

  bool isHovered = false;

  public string text;

  public Button(Vector2 CenterPosition, int TotalWidth, int TotalHeight, Color ButtonColor, Color BorderColor)
  {
    _texture = ServiceLocator.GetService<IContentManagerService>().GetTexture("pixel");

    // temp @remove
    _font = ServiceLocator.GetService<IContentManagerService>().GetFont("MyFont");

    centerPosition = CenterPosition;
    width = TotalWidth;
    height = TotalHeight;
    topLeftPosition.X = centerPosition.X - width / 2;
    topLeftPosition.Y = centerPosition.Y - height / 2;

    buttonColor = ButtonColor;
    borderColor = BorderColor;

    IRendererService renderer = ServiceLocator.GetService<IRendererService>();
    renderer.AddRenderable(this);

    ServiceLocator.GetService<IInputService>().onmouseclick += HandleMouseInput;
  }

  public void HandleMouseInput(object sender, MouseState MouseState)
  {
    Rectangle rect = new Rectangle((int)topLeftPosition.X, (int)topLeftPosition.Y, width, height);
    if (!isHovered && rect.Contains(MouseState.X, MouseState.Y))
    {
      isHovered = true;
      OnEnterHover();
    }
    else if (isHovered && !rect.Contains(MouseState.X, MouseState.Y))
    {
      isHovered = false;
      OnExitHover();
    }
  }

  public virtual void OnMouseClick() { }

  public void OnEnterHover()
  {
    buttonColor = Color.LightGray;
    borderColor = Color.LightGray;
    textColor = Color.Black;
  }

  public void OnExitHover()
  {
    buttonColor = Color.White;
    borderColor = Color.White;
    textColor = Color.DarkSlateGray;
  }

  public override void Draw(SpriteBatch spriteBatch)
  {
    // draw black border
    spriteBatch.Draw(_texture, new Rectangle((int)topLeftPosition.X - borderSize, (int)topLeftPosition.Y - borderSize, width + borderSize * 2, height + borderSize * 2), borderColor);

    // draw button
    spriteBatch.Draw(_texture, new Rectangle((int)topLeftPosition.X, (int)topLeftPosition.Y, width, height), buttonColor);

    // draw text
    if (text != null)
    {
      spriteBatch.DrawString(_font, text, centerPosition, textColor);
    }
  }
}