using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class ScreenText : Renderable
{
  string text;
  SpriteFont font;
  Vector2 position;

  public ScreenText(string Text, Vector2 Position, SpriteFont font)
  {

  }

  public ScreenText Instantiate(string Text, Vector2 Position)
  {
    IrendererService renderer = ServiceLocator.GetService<IrendererService>();
    // TODO font = resouces.getfont() or whatever
    ScreenText txt = new ScreenText(Text, Position, font);
    renderer.AddRenderable(txt);
    return txt;
  }

  public override void Draw(SpriteBatch spriteBatch)
  {
    spriteBatch.DrawString(font, text, position, Color.White);
  }
}