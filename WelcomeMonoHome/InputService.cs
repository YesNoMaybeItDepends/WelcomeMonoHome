using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

public class InputService : IInputService
{
  Camera _camera;
  GraphicsDeviceManager graphics;

  MouseState lastMouseState;
  MouseState mouseState;

  KeyboardState lastKeyboardState;
  KeyboardState keyboardState;

  public InputService(Camera camera)
  {
    _camera = camera;
  }

  public void GetMouseClick()
  {
    MouseState mouse = Mouse.GetState();
    //_camera.WorldToScreen(ass.)
    // RAYCAST
    Rectangle mouserect = new Rectangle(mouse.X, mouse.Y, 1, 1);
  }

  public delegate void OnMouseClick(object sender, MouseState MouseState);

  public event OnMouseClick onmouseclick;

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

  public void Update(GameTime gameTime)
  {
    // If it's the first frame we don't have any input at all
    if (mouseState != null)
    {
      lastMouseState = mouseState;
      lastKeyboardState = keyboardState;
    }

    mouseState = Mouse.GetState();
    keyboardState = Keyboard.GetState();

    if (mouseState != lastMouseState)
    {
      onmouseclick(this, mouseState);
    }
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