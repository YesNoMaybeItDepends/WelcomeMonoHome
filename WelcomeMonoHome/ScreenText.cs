using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class ScreenText : IRenderable
{
  public string text { get; set; }
  SpriteFont font { get; set; }
  public Vector2 position { get; set; }

  public ScreenText(string Text, Vector2 Position, SpriteFont Font)
  {
    IrendererService renderer = ServiceLocator.GetService<IrendererService>();
    // TODO font = resouces.getfont() or whatever
    text = Text;
    position = Position;
    font = Font;
    renderer.AddRenderable(this);
  }

  // public ScreenText Instantiate(string Text, Vector2 Position)
  // {
  //
  // }

  public void Draw(SpriteBatch spriteBatch)
  {
    spriteBatch.DrawString(font, text, position, Color.White);
  }
}