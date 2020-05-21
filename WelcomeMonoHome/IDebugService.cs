using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IDebugService
{
  void AddToTextList(ScreenText screenText);
  void RemoveFromTextList(ScreenText screenText);
  void UpdateTextListPositions();
}