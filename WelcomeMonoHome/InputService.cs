using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

public class InputService : IInputService
{
  Camera _camera;
  GraphicsDeviceManager _graphics;

  // Mouse States
  public MouseState lastMouseState;
  public MouseState mouseState;

  // Keyboard States
  public KeyboardState lastKeyboardState;
  public KeyboardState keyboardState;

  // Current Mouse info
  public ButtonStateIS leftButton;
  public ButtonStateIS rightButton;
  public ButtonStateIS middleButton;
  public ScrollWheelState scrollWheel;

  // Current Keyboard info
  public Dictionary<Keys, KeyStateIS> keys;

  // Mouse World coordinates
  public int mouseWorldX;
  public int mouseWorldY;

  // Mouse Camera coordinates
  public int mouseCameraX;
  public int mouseCameraY;

  public InputService(Camera camera)
  {
    _camera = camera;
    keys = new Dictionary<Keys, KeyStateIS>();
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

    // TODO update mouse and keyboard info?

  }

  public KeyStateIS GetKeyStateIS(Keys key)
  {
    bool isDown;
    bool isRepeat;

    isDown = keyboardState.IsKeyDown(key) ? true : false;
    isRepeat = keyboardState.IsKeyDown(key) == lastKeyboardState.IsKeyDown(key) ? true : false;

    KeyStateIS keyStateIS = new KeyStateIS(key, isDown, isRepeat);
    return keyStateIS;
  }

  public ButtonStateIS GetButtonStateIS(MouseButtons mouseButton)
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

    ButtonStateIS buttonStateIS = new ButtonStateIS(mouseButton, isDown, isRepeat);
    return buttonStateIS;
  }
}

public enum MouseButtons
{
  Left,
  Right,
  Middle
}

public struct ButtonStateIS
{
  public MouseButtons button;
  public bool isDown;
  public bool isRepeat;

  public ButtonStateIS(MouseButtons Button, bool IsDown, bool IsRepeat)
  {
    button = Button;
    isDown = IsDown;
    isRepeat = IsRepeat;
  }
}

public struct KeyStateIS
{
  public Keys key;
  public bool isDown;
  public bool isRepeat;

  public KeyStateIS(Keys Key, bool IsDown, bool IsRepeat)
  {
    key = Key;
    isDown = IsDown;
    isRepeat = IsRepeat;
  }
}

public struct ScrollWheelState
{

}
