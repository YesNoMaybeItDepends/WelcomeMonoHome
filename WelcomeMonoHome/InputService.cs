using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

public class InputService : IInputService
{
  Camera _camera;
  GraphicsDeviceManager graphics;

  public InputService(Camera camera)
  {
    _camera = camera;
  }

  public void GetMouseClick()
  {
    MouseState ass = Mouse.GetState();
    //_camera.WorldToScreen(ass.)
  }

  public Vector2 GetWorldClick()
  {
    Point point = Mouse.GetState().Position;
    Vector2 pos = Vector2.Zero;
    pos.X = point.X;
    pos.Y = point.Y;
    return _camera.ScreenToWorld(pos);
  }

  public Vector2 GetCameraClick()
  {
    Point point = Mouse.GetState().Position;
    Vector2 pos = Vector2.Zero;
    pos.X = point.X;
    pos.Y = point.Y;
    return pos;
  }
}

public class MuhMouseState
{
  int WorldX;
  int WorldY;

  int CameraX;
  int CameraY;

  bool isDown;
  bool isUp;

  bool isHeld;
}