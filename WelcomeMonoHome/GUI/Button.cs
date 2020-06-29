using System;
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

  float transparency = 0.75f;

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

    // IRendererService renderer = ServiceLocator.GetService<IRendererService>();
    // renderer.AddRenderable(this);

    // TODO unsubscribe when we destroy
    ServiceLocator.GetService<IInputService>().onmouseclick += HandleMouseInput;
  }

  public void HandleMouseInput(object sender, MouseState MouseState)
  {
    Rectangle rect = new Rectangle((int)topLeftPosition.X, (int)topLeftPosition.Y, width, height);

    // Mouse inside button
    if (rect.Contains(MouseState.X, MouseState.Y))
    {
      if (!isHovered)
      {
        isHovered = true;
        OnEnterHover();
      }

      // Mouse click
      if (MouseState.LeftButton == ButtonState.Pressed)
      {
        OnMouseClick();
      }
    }

    // Mouse outside button
    else
    {
      if (isHovered)
      {
        isHovered = false;
        OnExitHover();
      }
    }
  }

  public Action dothingie { get; set; }
  public void OnMouseClick()
  {
    dothingie();
  }


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
    //spriteBatch.Draw(_texture, new Rectangle((int)topLeftPosition.X - borderSize, (int)topLeftPosition.Y - borderSize, width + borderSize * 2, height + borderSize * 2), borderColor * transparency);

    // draw button
    spriteBatch.Draw(_texture, new Rectangle((int)topLeftPosition.X, (int)topLeftPosition.Y, width, height), buttonColor * transparency);

    // draw text
    if (text != null)
    {
      Vector2 textSize = _font.MeasureString(text);
      float scale = 2f;
      Vector2 textPos = new Vector2(centerPosition.X - (textSize.X * scale) / 2, centerPosition.Y - (textSize.Y * scale) / 2);
      spriteBatch.DrawString(_font, text, textPos, textColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }
  }
}