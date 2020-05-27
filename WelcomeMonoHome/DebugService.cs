using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WelcomeMonoHome.GUI;

public class DebugService : IDebugService
{
  ContentManager _content;

  List<TextBox> _textList;
  List<TextBox> _textListToAdd;
  List<TextBox> _textListToRemove;

  List<RectanglePrimitive> _rectangles;
  List<RectanglePrimitive> _rectanglesToAdd;
  List<RectanglePrimitive> _rectanglesToRemove;

  public DebugService(ContentManager content)
  {
    _content = content;
    _textList = new List<TextBox>();
    _textListToAdd = new List<TextBox>();
    _textListToRemove = new List<TextBox>();

    _rectangles = new List<RectanglePrimitive>();
    _rectanglesToAdd = new List<RectanglePrimitive>();
    _rectanglesToRemove = new List<RectanglePrimitive>();
  }

  public void UpdateTextListPositions()
  {
    int y = 0;
    int increment = 20;
    foreach (TextBox textBox in _textList)
    {
      textBox.position = new Vector2(0, y);
      y += increment;
    }
  }

  public void AddToTextList(TextBox screenText)
  {
    //_screenTexts.Add(new ScreenText(text, pos, _font));
    _textList.Add(screenText);
    UpdateTextListPositions();
  }

  public void RemoveFromTextList(TextBox screenText)
  {
    _textList.Remove(screenText);
  }

  public void DrawRectangle(RectanglePrimitive rectangle)
  {
    _rectanglesToAdd.Add(rectangle);
    rectangle.Instantiate();
  }

  public void RemoveRectangle(RectanglePrimitive rectangle)
  {
    _rectanglesToRemove.Add(rectangle);
    rectangle.Destroy();
  }

  // public void UpdateText(ScreenText screenText, string text)
  // {
  //   _screenTexts.Find(screenText).UpdateText(text);
  // }
}