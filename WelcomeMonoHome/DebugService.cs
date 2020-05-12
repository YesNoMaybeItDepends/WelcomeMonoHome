using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class DebugService : IDebugService
{
  SpriteFont font;
  ContentManager _content;

  List<string> text;

  public DebugService(ContentManager content)
  {
    _content = content;
    text = new List<string>();
  }

  public void DrawText()
  {

  }
}