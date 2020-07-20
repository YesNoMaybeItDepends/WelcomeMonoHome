using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public interface IInputService
{
  ButtonStateIS GetButtonStateIS(MouseButtons mouseButton);
  KeyStateIS GetKeyStateIS(Keys key);
  Vector2 GetMouseCameraPos();
  Vector2 GetMouseWorldPos();
  void Update(GameTime gameTime);
}