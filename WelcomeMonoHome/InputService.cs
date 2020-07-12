using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

public class InputService : IInputService
{
  Camera _camera;
  GraphicsDeviceManager _graphics;

  public MouseState lastMouseState;
  public MouseState mouseState;

  public KeyboardState lastKeyboardState;
  public KeyboardState keyboardState;

  public InputState leftButton;
  public InputState rightButton;
  public InputState middleButton;
  public ScrollWheelState scrollWheel;

  public Dictionary<Keys, InputState> keys;

  public int mouseWorldX;
  public int mouseWorldY;

  public int mouseCameraX;
  public int mouseCameraY;

  public InputService(Camera camera)
  {
    _camera = camera;
    keys = new Dictionary<Keys, InputState>();
  }

  public void GetMouseClick()
  {
    MouseState mouse = Mouse.GetState();
    //_camera.WorldToScreen(ass.)
    // RAYCAST
    Rectangle mouserect = new Rectangle(mouse.X, mouse.Y, 1, 1);
  }

  public Vector2 GetMouseWorldPos()
  {
    Point point = Mouse.GetState().Position;
    Vector2 pos = Vector2.Zero;
    pos.X = point.X;
    pos.Y = point.Y;
    return _camera.ScreenToWorld(pos);
  }

  public Vector2 GetMouseCameraPos()
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

    // update mouse buttans here?    
  }

  public InputState GetKeyState(Keys key)
  {
    bool isDown;
    bool isRepeat;

    isDown = keyboardState.IsKeyDown(key) ? true : false;
    isRepeat = keyboardState.IsKeyDown(key) == lastKeyboardState.IsKeyDown(key) ? true : false;

    InputState inputState = new InputState(isDown, isRepeat);
    return inputState;
  }

  public InputState GetButtonState(MouseButtons mouseButton)
  {

    // ButtonState is a struct and it can't be null
    // Switches are a meme, so we must assign it something
    // It will always be reassigned, so it's actually ok
    ButtonState button = default;
    ButtonState lastButton = default;

    switch (mouseButton)
    {
      case MouseButtons.Left:
        {
          button = mouseState.LeftButton;
          lastButton = lastMouseState.LeftButton;
          break;
        }
      case MouseButtons.Right:
        {
          button = mouseState.RightButton;
          lastButton = lastMouseState.RightButton;
          break;
        }
      case MouseButtons.Middle:
        {
          button = mouseState.MiddleButton;
          lastButton = lastMouseState.MiddleButton;
          break;
        }
    }

    bool isDown;
    bool isRepeat;

    isDown = (button == ButtonState.Pressed) ? true : false;
    isRepeat = (button == lastButton) ? true : false;

    InputState buttonState = new InputState(isDown, isRepeat);
    return buttonState;
  }
}

public enum MouseButtons
{
  Left,
  Right,
  Middle
}

public struct InputState
{
  public bool isDown;
  public bool isRepeat;

  public InputState(bool isdown, bool isrepeat)
  {
    isDown = isdown;
    isRepeat = isrepeat;
  }
}

public struct ScrollWheelState
{

}
