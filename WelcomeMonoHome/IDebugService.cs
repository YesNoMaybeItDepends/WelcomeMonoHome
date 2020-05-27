using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WelcomeMonoHome.GUI;

public interface IDebugService
{
  void AddToTextList(TextBox screenText);
  void RemoveFromTextList(TextBox screenText);
  void UpdateTextListPositions();
}