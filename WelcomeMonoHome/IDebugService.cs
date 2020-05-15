using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IDebugService
{
  ScreenText DrawText(string text, Vector2 pos);
  void UpdateFont(SpriteFont font);
}