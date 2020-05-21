using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class DebugService : IDebugService
{
  ContentManager _content;

  List<ScreenText> _textList;
  List<ScreenText> _textListToAdd;
  List<ScreenText> _textListToRemove;

  public DebugService(ContentManager content)
  {
    _content = content;
    _textList = new List<ScreenText>();
  }

  public void UpdateTextListPositions()
  {
    int y = 0;
    int increment = 20;
    foreach (ScreenText screenText in _textList)
    {
      screenText.position = new Vector2(0, y);
      y += increment;
    }
  }

  public void AddToTextList(ScreenText screenText)
  {
    //_screenTexts.Add(new ScreenText(text, pos, _font));
    _textList.Add(screenText);
    UpdateTextListPositions();
  }

  public void RemoveFromTextList(ScreenText screenText)
  {
    _textList.Remove(screenText);
  }

  // public void UpdateText(ScreenText screenText, string text)
  // {
  //   _screenTexts.Find(screenText).UpdateText(text);
  // }
}