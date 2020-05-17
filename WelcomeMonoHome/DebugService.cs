using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class DebugService : IDebugService
{
  SpriteFont _font;
  ContentManager _content;

  List<ScreenText> _screenTexts;

  public DebugService(ContentManager content)
  {
    _content = content;
    _screenTexts = new List<ScreenText>();
  }

  public void UpdateFont(SpriteFont font)
  {
    _font = font;
  }

  public ScreenText DrawText(string text, Vector2 pos)
  {
    //_screenTexts.Add(new ScreenText(text, pos, _font));
    return new ScreenText(text, pos);
  }

  // public void UpdateText(ScreenText screenText, string text)
  // {
  //   _screenTexts.Find(screenText).UpdateText(text);
  // }
}