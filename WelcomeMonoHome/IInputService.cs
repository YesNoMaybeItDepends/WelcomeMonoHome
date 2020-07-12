using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public interface IInputService
{
  Vector2 GetWorldClick();
  event InputService.OnMouseClick OnMouseClickEvent;
}